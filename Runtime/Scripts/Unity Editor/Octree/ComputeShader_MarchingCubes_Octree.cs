using System.Collections;
using System.Collections.Generic;

using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

namespace TerrainSystem
{
    public class ComputeShader_MarchingCubes_Octree
    {
        // Variables
        private ComputeShader _computeShader;
        private ComputeBuffer _triangleTableBuffer;
        private ComputeBuffer _edgeIndexBuffer;
        private ComputeBuffer _cornerTableBuffer;
        private ComputeBuffer _terrainDataBuffer;
        private ComputeBuffer _verticesDataBuffer;
        private ComputeBuffer _verticesCounterBuffer;
        private int _kernel;
        private int[] verticesLength = new int[1];

        private Vector3Int _chunkSize;
        private float _surfaceThreshold;
        private float[] _sampleData;
        private Vector3Int _octreeRes;
        private float _octreeLOD;

        private Vector3[] _vertices;
        private int[] _triangles;

        // Constructor
        public ComputeShader_MarchingCubes_Octree()
        {
            // Instantiate Compute Shader
            //ComputeShader _computeShaderAsset = (ComputeShader)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Terrain/ComputeShader/ComputeShader_MarchingCubes_Octree.compute", typeof(ComputeShader));
            //_computeShader = GameObject.Instantiate(_computeShaderAsset);

            // Kernel
            _kernel = _computeShader.FindKernel("CSMain");
            // Triangle Table Buffer
            _triangleTableBuffer = new ComputeBuffer(Tables.TriangleTable.Length, sizeof(int));
            _triangleTableBuffer.SetData(Tables.TriangleTable);
            _computeShader.SetBuffer(_kernel, "triangleTableBuffer", _triangleTableBuffer);
            // Edge Index Buffer
            _edgeIndexBuffer = new ComputeBuffer(Tables.EdgeIndexes.Length, sizeof(int) * 2);
            _edgeIndexBuffer.SetData(Tables.EdgeIndexes);
            _computeShader.SetBuffer(_kernel, "edgeIndexBuffer", _edgeIndexBuffer);
            // Corner Table Buffer
            _cornerTableBuffer = new ComputeBuffer(Tables.CornerTable.Length, sizeof(float) * 3);
            _cornerTableBuffer.SetData(Tables.CornerTable);
            _computeShader.SetBuffer(_kernel, "cornerTableBuffer", _cornerTableBuffer);
            // Vertices Counter Buffer
            _verticesCounterBuffer = new ComputeBuffer(1, sizeof(int), ComputeBufferType.Raw);
            _computeShader.SetBuffer(_kernel, "verticesCounterBuffer", _verticesCounterBuffer);
        }

        // Methods
        public ComputeShader computeShader { get { return _computeShader; } }
        public ComputeBuffer triangleTableBuffer { get { return _triangleTableBuffer; } }
        public ComputeBuffer edgeIndexBuffer { get { return _edgeIndexBuffer; } }
        public ComputeBuffer cornerTableBuffer { get { return _cornerTableBuffer; } }
        public ComputeBuffer terrainDataBuffer { get { return _terrainDataBuffer; } }
        public ComputeBuffer verticesDataBuffer { get { return _verticesDataBuffer; } }
        public ComputeBuffer verticesCounterBuffer { get { return _verticesCounterBuffer; } }
        public int kernel { get { return _kernel; } }

        public Vector3[] vertices { get { return _vertices; } }
        public int[] triangles { get { return _triangles; } }

        public float[] sampleData { get { return _sampleData; } set { _sampleData = value; } }
        public Vector3Int octreeRes { get { return _octreeRes; } set { _octreeRes = value; SetDynamicBuffers(); } }
        public float octreeLOD { get { return _octreeLOD; } set { _octreeLOD = value; } }
        public float surfaceThreshold { get { return _surfaceThreshold; } set { _surfaceThreshold = value; } }

        private void SetDynamicBuffers()
        {
            // Terrain Data Buffer
            _terrainDataBuffer = new ComputeBuffer((_octreeRes.x + 1) * (_octreeRes.y + 1) * (_octreeRes.z + 1), sizeof(float));
            _computeShader.SetBuffer(_kernel, "terrainDataBuffer", _terrainDataBuffer);
            // Vertices Data Buffer
            _verticesDataBuffer = new ComputeBuffer(_octreeRes.x * _octreeRes.y * _octreeRes.z * 15, sizeof(float) * 3, ComputeBufferType.Append);
            _computeShader.SetBuffer(_kernel, "verticesDataBuffer", _verticesDataBuffer);
        }

        public void Dispatch()
        {
            // Pass SampleData
            terrainDataBuffer.SetData(_sampleData);
            // Reset Vertices Buffer
            verticesDataBuffer.SetCounterValue(0);
            // Pass Terrain Data Info
            _computeShader.SetFloat("surfaceThreshold", _surfaceThreshold);
            _computeShader.SetInts("sampleResolution", new int[] { _octreeRes.x, _octreeRes.y, _octreeRes.z} );
            _computeShader.SetFloat("lodScale", _octreeLOD);

            // Dispatch
            int threadGroupsX = _octreeRes.x;
            int threadGroupsY = _octreeRes.y;
            int threadGroupsZ = _octreeRes.z;
            computeShader.Dispatch(_kernel, threadGroupsX, threadGroupsY, threadGroupsZ);

            // Get Amount of Vertices
            ComputeBuffer.CopyCount(verticesDataBuffer, verticesCounterBuffer, 0);
            verticesCounterBuffer.GetData(verticesLength);
            verticesLength[0] *= 3;

            // Build Mesh if it has at least one triangle
            if (verticesLength[0] >= 3)
            {
                // Vertices
                _vertices = new Vector3[verticesLength[0]];
                verticesDataBuffer.GetData(_vertices);
                // Triangles
                _triangles = new int[_vertices.Length];
                for (int i = 0; i < _vertices.Length; i++) { _triangles[i] = i; }
            }
        }

        public IEnumerator IEDispatch()
        {
            // Set Data
            terrainDataBuffer.SetData(_sampleData);
            // Reset Vertices Buffer
            verticesDataBuffer.SetCounterValue(0);
            // Pass Terrain Data Info
            computeShader.SetFloat("surfaceThreshold", _surfaceThreshold);
            computeShader.SetInt("sizeX", _chunkSize.x);
            computeShader.SetInt("sizeY", _chunkSize.y);
            computeShader.SetInt("sizeZ", _chunkSize.z);

            // Dispatch
            computeShader.Dispatch(kernel, _chunkSize.x, _chunkSize.y, _chunkSize.z);
            AsyncGPUReadbackRequest requestVertices = AsyncGPUReadback.Request(verticesDataBuffer);
            yield return new WaitUntil(() => (requestVertices.done));

            // Get Amount of Vertices
            ComputeBuffer.CopyCount(verticesDataBuffer, verticesCounterBuffer, 0);
            verticesCounterBuffer.GetData(verticesLength);
            verticesLength[0] *= 3;

            // Build Mesh if it has at least one triangle
            if (verticesLength[0] >= 3)
            {
                _vertices = new Vector3[verticesLength[0]];
                verticesDataBuffer.GetData(_vertices);

                // Build Triangles
                _triangles = new int[_vertices.Length];
                for (int i = 0; i < _vertices.Length; i++) { _triangles[i] = i; }
            }
        }

        public void Clear()
        {
            _sampleData = null;
            _vertices = null;
            _triangles = null;
        }
        public void Destroy()
        {
            if (_triangleTableBuffer != null)
            {
                _triangleTableBuffer.Release();
                _triangleTableBuffer = null;
            }
            if (_edgeIndexBuffer != null)
            {
                _edgeIndexBuffer.Release();
                _edgeIndexBuffer = null;
            }
            if (_cornerTableBuffer != null)
            {
                _cornerTableBuffer.Release();
                _cornerTableBuffer = null;
            }
            if (_terrainDataBuffer != null)
            {
                _terrainDataBuffer.Release();
                _terrainDataBuffer = null;
            }
            if (_verticesDataBuffer != null)
            {
                _verticesDataBuffer.Release();
                _verticesDataBuffer = null;
            }
            if (_verticesCounterBuffer != null)
            {
                _verticesCounterBuffer.Release();
                _verticesCounterBuffer = null;
            }
#if (UNITY_EDITOR)
            UnityEngine.Object.DestroyImmediate(_computeShader);
#else
        UnityEngine.Object.Destroy(_computeShader);
#endif
        }

#if (UNITY_EDITOR)
        private IEnumerator _iEnumerator;
        public void EditorApplicationUpdate()
        {
            _iEnumerator = IEDispatch();
            EditorApplication.update += UpdateIESmoothMesh;
        }
        private void UpdateIESmoothMesh()
        {
            if (_iEnumerator.MoveNext()) { }
            else
            {
                EditorApplication.update -= UpdateIESmoothMesh;
            }
        }
#endif

    }

}
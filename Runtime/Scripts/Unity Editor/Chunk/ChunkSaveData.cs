
using System;
using UnityEngine;

namespace TerrainSystem
{
    [Serializable]
    public class ChunkSaveData
    {
        // Variables
        [SerializeField] private float[] _localPosition;
        [SerializeField] private float[] _sampleData;
        [SerializeField] private float[] _vertices;
        [SerializeField] private int[] _triangles;
        [SerializeField] private int[] _index;

        // Constructors
        public ChunkSaveData(ChunkData chunkData)
        {
            _localPosition = SerializeVector3(chunkData.localPosition);
            //_sampleData = chunkData.sampleData;
            _vertices = SerializeVertices(chunkData.vertices);
            _triangles = chunkData.triangles;
            _index = SerializeVector3Int(chunkData.index);
        }

        // Getter / Setter
        public Vector3 localPosition { get { return DeSerializeVector3(_localPosition); } }
        public float[] sampleData { get { return _sampleData; } }
        public Vector3[] vertices { get { return DeSerializeVertices(); } }
        public int[] trianlges { get { return _triangles; } }
        public Vector3Int index { get { return DeSerializeVector3Int(_index); } }

        // Methods
        private float[] SerializeVertices(Vector3[] vertices)
        {
            if (vertices == null)
            {
                return new float[0];
            }
            float[] serializedVertices = new float[vertices.Length * 3];
            int i = 0;
            for (int v = 0; v < vertices.Length; v++)
            {
                serializedVertices[i] = vertices[v].x;
                serializedVertices[i + 1] = vertices[v].y;
                serializedVertices[i + 2] = vertices[v].z;
                i += 3;
            }
            return serializedVertices;
        }
        private float[] SerializeVector3(Vector3 vector3)
        {
            float[] vector3Array = new float[3];
            vector3Array[0] = vector3.x;
            vector3Array[1] = vector3.y;
            vector3Array[2] = vector3.z;
            return vector3Array;
        }
        private int[] SerializeVector3Int(Vector3Int vector3Int)
        {
            int[] vector3IntArray = new int[3];
            vector3IntArray[0] = vector3Int.x;
            vector3IntArray[1] = vector3Int.y;
            vector3IntArray[2] = vector3Int.z;
            return vector3IntArray;
        }

        private Vector3 DeSerializeVector3(float[] serializedVector3Array)
        {
            if (serializedVector3Array.Length != 3)
            {
                throw new ArgumentException("The Parameter 'serializedVector3Array' must have Length of 3");
            }
            Vector3 deSerializedVector3 = new Vector3(
                serializedVector3Array[0],
                serializedVector3Array[1],
                serializedVector3Array[2]
            );
            return deSerializedVector3;
        }
        private Vector3Int DeSerializeVector3Int(int[] serializedVector3IntArray)
        {
            if (serializedVector3IntArray.Length != 3)
            {
                throw new ArgumentException("The Parameter 'serializedVector3Array' must have Length of 3");
            }
            Vector3Int deSerializedVector3Int = new Vector3Int(
                serializedVector3IntArray[0],
                serializedVector3IntArray[1],
                serializedVector3IntArray[2]
            );
            return deSerializedVector3Int;
        }
        private Vector3[] DeSerializeVertices()
        {
            if (_vertices.Length % 3 != 0)
            {
                throw new ArgumentException("The Array Parameter 'serializedVertices' Length must be multiple of 3");
            }
            Vector3[] vertices = new Vector3[(int)_vertices.Length / 3];
            int vertID = 0;
            for (int i = 0; i < _vertices.Length; i += 3)
            {
                vertices[vertID] = new Vector3(
                    _vertices[i],
                    _vertices[i + 1],
                    _vertices[i + 2]
                );
                vertID++;
            }
            return vertices;
        }
        private Vector3[] DeSerializeVertices(float[] serializedVertices)
        {
            if (serializedVertices.Length % 3 != 0)
            {
                throw new ArgumentException("The Array Parameter 'serializedVertices' Length must be multiple of 3");
            }
            Vector3[] vertices = new Vector3[(int)serializedVertices.Length / 3];
            int vertID = 0;
            for (int i = 0; i < serializedVertices.Length; i += 3)
            {
                vertices[vertID] = new Vector3(
                    serializedVertices[i],
                    serializedVertices[i + 1],
                    serializedVertices[i + 2]
                );
            }
            return vertices;
        }
    }
}

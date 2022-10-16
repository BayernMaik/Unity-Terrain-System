using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.Rendering;
using System.Collections.Generic;

namespace TerrainSystem
{
    [ExecuteInEditMode]
    public class TerrainComponent : MonoBehaviour
    {
        #region Variables
        [SerializeField] private TerrainDataAsset _terrainDataAsset;
        private TerrainData _terrainData;
        private Terrain _terrain;

        //private ComputeShader_SmoothMesh _computeShader_SmoothMesh;
        //private MarchingCubes _marchingCubes;
        // Build Data Variables
        private bool _isBuilding = true;
        private float _buildProgress = 0f;
        private string _buildProgressHint = "";
        private Chunk _currentBuildChunk;

        private Dictionary<Vector3Int, Chunk> _currentChunkIDs = new Dictionary<Vector3Int, Chunk>();
        #endregion

        #region GetterSetter
        public TerrainData terrainData { get { return _terrainData; } set { _terrainData = value; } }
        public TerrainDataAsset terrainDataAsset { get { return _terrainDataAsset; } set { _terrainDataAsset = value; } }
        public Terrain terrain { get { return _terrain; } set { _terrain = value; } }
        public float buildProgress { get { return _buildProgress; } }
        public bool isBuilding { get { return _isBuilding; } }
        public string buildProgressHint { get { return _buildProgressHint; } }
        #endregion

        #region MonoBehaviors
        private void OnEnable()
        {
#if (UNITY_EDITOR)
            EditorApplication.update += IELod;
#endif
        }
        private void OnDisable()
        {
#if (UNITY_EDITOR)
            EditorApplication.update -= IELod;
            ClearCurrentChunks();
#endif
        }
        private void Update()
        {
            if (!_isBuilding)
            UpdateVisibleChunks(Vector3.zero, 100f);
        }
        #endregion

        #region Methods
        public void ClearCurrentChunks()
        {
            foreach (Vector3Int key in _currentChunkIDs.Keys)
            {
                Chunk deleteChunk;
                _currentChunkIDs.TryGetValue(key, out deleteChunk);
                DestroyImmediate(deleteChunk.gameObject);
            }
            _currentChunkIDs.Clear();
        }
        public void Build()
        {
            StartCoroutine(IE_GenerateTerrainData());
        }
        public IEnumerator IE_GenerateTerrainData()
        {
            yield return null;
            /*
            _isBuilding = true;
            _marchingCubes = new MarchingCubes(_terrainData.voxelResolution);

            // Ensure Terrain Data Fully Initialized
            while (_terrainData.iNoise[0].material == null)
            {
                yield return null;
            }
            while (_marchingCubes.computeShaderComponent == null)
            {
                yield return null;
            }

            int abs = _terrainData.chunkBounds.x * _terrainData.chunkBounds.y * _terrainData.chunkBounds.z;
            int progress = 0;

            ChunkData chunkData = new ChunkData();
            for (int x = 0; x < _terrainData.chunkBounds.x; x++)
            {
                for (int y = 0; y < _terrainData.chunkBounds.y; y++)
                {
                    for (int z = 0; z < _terrainData.chunkBounds.z; z++)
                    {
                        // Chunk Index
                        chunkData.index = new Vector3Int(x, y, z);
                        // Chunk Position
                        chunkData.localPosition = new Vector3(
                            x * _terrainData.chunkSize.x,
                            y * _terrainData.chunkSize.y,
                            z * _terrainData.chunkSize.z
                        );
                        // Generate Data
                        chunkData.sampleData = _terrainData.GenerateChunkVoxelData(chunkData);
                        _marchingCubes.computeShaderComponent.Dispatch(chunkData.sampleData, _terrainData.surfaceThreshold, _terrainData.voxelResolution, _terrainData.chunkSize);
                        chunkData.vertices = _marchingCubes.computeShaderComponent.vertices;
                        chunkData.triangles = _marchingCubes.computeShaderComponent.triangles;

                        chunkData.Save(_terrainData);
                        _marchingCubes.computeShaderComponent.Clear();


                        /*
                        // Smooth Mesh
                        if (_computeShader_SmoothMesh == null)
                        {
                            _computeShader_SmoothMesh = new ComputeShader_SmoothMesh(chunkData.vertices, chunkData.triangles);
                        }
                        else
                        {
                            _computeShader_SmoothMesh.vertices = chunkData.vertices;
                            _computeShader_SmoothMesh.triangles = chunkData.triangles;
                            _computeShader_SmoothMesh.Dispatch();
                        }
                        chunkData.vertices = _computeShader_SmoothMesh.vertices;
                        chunkData.triangles = _computeShader_SmoothMesh.triangles;
                        */
                        //chunkData.Save(_terrainData);

            /*
                        progress++;
                        _buildProgress = (float)progress / abs;
                        _buildProgressHint = (_buildProgress * 100).ToString("f2") + "%";
                        
                        if (progress % 5 == 0)
                        {
                            yield return null;
                        }
                    }
                }
            }

            _buildProgress = 0;
            _buildProgressHint = "";
            _isBuilding = false;
            */
        }

        public void UpdateVisibleChunks(Vector3 position, float radius)
        {
            Vector3 _camPos = Camera.main.transform.position;

            List<Vector3Int> _activeChunksIDs = _terrain.ActiveJunkIDs(_camPos, 50f);

            // Check for non visible chunks
            foreach (Vector3Int key in _currentChunkIDs.Keys)
            {
                if (!_activeChunksIDs.Contains(key))
                {
                    // Despawn Chunk
                    Chunk destroyChunk;
                    _currentChunkIDs.TryGetValue(key, out destroyChunk);
                    Destroy(destroyChunk.gameObject);
                    _currentChunkIDs.Remove(key);
                }
            }
            // Check for new visible Chunks
            foreach (Vector3Int id in _activeChunksIDs)
            {
                if (!_currentChunkIDs.ContainsKey(id))
                {
                    // Add new chunk                    
                    ChunkData chunkData = TerrainSystemUtility.LoadChunk(_terrainData, id);
                    if (chunkData != null)
                    {
                        Chunk chunk = new Chunk(chunkData);

                        chunk.gameObject.transform.SetParent(gameObject.transform);
                        _currentChunkIDs.Add(id, chunk);
                    }                    
                }
            }
        }
       
        public void EditAt(Vector3Int pos)
        {

        }
        #endregion

#if (UNITY_EDITOR)
        private Vector3Int onChunk;
        private void IELod()
        {
            if (Application.isEditor && !Application.isPlaying)
            {
                Vector3 position = SceneView.lastActiveSceneView.camera.transform.position;

                Vector3Int ci = _terrainDataAsset.GetChunkIndexAt(position);
                if (ci != onChunk)
                {
                    onChunk = ci;
                    List<Vector3Int> _activeChunks = _terrainDataAsset.ActiveJunkIDs(position, 50f);
                    // Despawn Old Chunks
                    List<Vector3Int> despawnChunkIds = new List<Vector3Int>();
                    foreach (Vector3Int key in _currentChunkIDs.Keys)
                    {
                        if (!_activeChunks.Contains(key))
                        {
                            despawnChunkIds.Add(key);
                        }
                    }
                    foreach (Vector3Int key in despawnChunkIds)
                    {
                        Chunk deleteChunk;
                        _currentChunkIDs.TryGetValue(key, out deleteChunk);
                        DestroyImmediate(deleteChunk.gameObject);
                        _currentChunkIDs.Remove(key);
                    }
                    
                    for (int i = 0; i < _activeChunks.Count; i++)
                    {
                        if (!_currentChunkIDs.ContainsKey(_activeChunks[i]))
                        {
                            // Add new chunk
                            ChunkData chunkData = TerrainSystemUtility.LoadChunk(_terrainData, _activeChunks[i]);
                            if (chunkData != null)
                            {
                                Chunk chunk = new Chunk(chunkData);


                                chunk.gameObject.transform.SetParent(gameObject.transform);
                                _currentChunkIDs.Add(_activeChunks[i], chunk);
                            }
                        }
                    }
                }
            }
        }
#endif
    }
}
using UnityEngine;
using System.Collections.Generic;

namespace TerrainSystem
{
    [CreateAssetMenu(fileName = "New Terrain", menuName = "Tools/Terrain System/New Terrain", order = 1)]
    public class TerrainDataAsset : ScriptableObject
    {
        #region Variables
        #region Terrain Data
        [SerializeField] private TerrainData terrainData;
        public TerrainData TerrainData
        {
            get { return terrainData; }
            set { terrainData = value; }
        }
        #endregion
        #region Terrain Name
        public string DisplayName
        {
            get { return terrainData.DisplayName; }
            set { terrainData.DisplayName = value; }
        }
        #endregion
        #region Terrain Size
        public float Width
        {
            get { return this.terrainData.Width; }
            set { this.terrainData.Width = value; }
        }
        public float Length
        {
            get { return this.terrainData.Length; }
            set { this.terrainData.Length = value; }
        }
        public float Height
        {
            get { return this.terrainData.Height; }
            set { this.terrainData.Height = value; }
        }
        #endregion
        #region Terrain Surface
        public float SurfaceMin
        {
            get { return this.terrainData.SurfaceMin; }
            set { this.terrainData.SurfaceMin = value; }
        }
        public float SurfaceMax
        {
            get { return this.terrainData.SurfaceMax; }
            set { this.terrainData.SurfaceMax = value; }
        }
        #endregion
        #region Terrain HeightMap
        public Texture2D HeightMap
        {
            get { return terrainData.HeightMap; }
            set { terrainData.HeightMap = value; }
        }
        #endregion
        #endregion

        #region Methods
        /// <summary>
        /// Method to be called from UnityEditor_TerrainEditor when TerrainData Scriptable Object is dropped into SceneView
        /// </summary>
        public void DropToSceneView()
        {
            new Terrain(this.terrainData);
        }
        #endregion


        [HideInInspector] [SerializeField] private string _name = "New Terrain";
        [HideInInspector] [SerializeField] private bool _smoothShade = true;
        [HideInInspector] [SerializeField] private float _surfaceThreshold = 0.5f;
        [HideInInspector] [SerializeField] private Vector2 _surfaceRange = new Vector2(0f, 16f);
        [HideInInspector] [SerializeField] private Vector3 _position = Vector3.zero;

        [HideInInspector] [SerializeField] private Vector3Int _sampleResolution = new Vector3Int(64, 64, 64);
        [HideInInspector] [SerializeField] private Vector3Int _chunkSize = new Vector3Int(16, 16, 16);
        [HideInInspector] [SerializeField] private Vector3 _chunkDetail = new Vector3(1f, 1f, 1f);
        [HideInInspector] [SerializeField] private Vector3Int _chunkBounds = new Vector3Int(4, 4, 4);
        //[HideInInspector] [SerializeReference] private List<INoise> _iNoise;

        [HideInInspector] [SerializeField] private Vector3Int _octreeSize = new Vector3Int(24, 24, 24);
        [HideInInspector] [SerializeField] private Vector3Int _octreeRes = new Vector3Int(16, 16, 16);
        [HideInInspector] [SerializeField] private Vector3Int _octreeBounds = new Vector3Int(1, 1, 1);
        [HideInInspector] [SerializeField] private int _octreeLOD = 0;
        [HideInInspector] [SerializeField] private int _subDivisions = 2;
        public Vector3Int octreeSize { get { return _octreeSize; } }
        public Vector3Int octreeRes { get { return _octreeRes; } }
        public int octreeLOD { get { return _octreeLOD; } }
        public int subDivisions { get { return _subDivisions; } set { _subDivisions = value; } }
        public Vector3Int octreeBounds { get { return _octreeBounds; } }


        
        // Constructors
        public TerrainDataAsset(){}

        // Methods
        public new string name { get { return _name; } set { _name = value; } }
        public bool smoothShade { get { return _smoothShade; } set { _smoothShade = value; } }
        public float surfaceThreshold { get { return _surfaceThreshold; } set { _surfaceThreshold = value; } }
        public Vector2 surfaceRange { get { return _surfaceRange; } set { _surfaceRange = value; } }
        public Vector3 size { get { return (_chunkSize * _chunkBounds); } }
        public Vector3 chunkDetail { get { return _chunkDetail; } }
        public Vector3Int chunkSize { get { return _chunkSize; } set { SetChunkSize(value); } }
        public Vector3Int chunkBounds { get { return _chunkBounds; } set { _chunkBounds = value; } }
        // public List<INoise> iNoise { get { if (_iNoise == null) { _iNoise = new List<INoise>(); } return _iNoise; } set { _iNoise = value; } }
        public Vector3Int sampleSize { get {
                return new Vector3Int(
                    Mathf.RoundToInt(_chunkSize.x / _chunkDetail.x),
                    Mathf.RoundToInt(_chunkSize.y / _chunkDetail.y),
                    Mathf.RoundToInt(_chunkSize.z / _chunkDetail.z)
                );
        } }
        public Vector3Int sampleResolution { get { return _sampleResolution; } set { SetSampleResolution(value); } }

        public void SetSampleResolution(Vector3Int sampleResolution)
        {
            // Chunk Size has to be at least 8 and multiple of 8 (min. Threadsize)
            _sampleResolution.x = Mathf.Max(Mathf.RoundToInt(sampleResolution.x / 8) * 8, 8);
            _sampleResolution.y = Mathf.Max(Mathf.RoundToInt(sampleResolution.y / 8) * 8, 8);
            _sampleResolution.z = Mathf.Max(Mathf.RoundToInt(sampleResolution.z / 8) * 8, 8);
            //if (_chunkSize != chunkSize) { Debug.LogWarning("ChunkSize must be multiple of 8 graeter of equal to 8"); }
        }
        public void SetChunkSize(Vector3Int chunkSize)
        {
            // Chunk Size has to be at least 8 and multiple of 8 (min. Threadsize)
            _chunkSize.x = Mathf.Max(Mathf.RoundToInt(chunkSize.x / 8) * 8, 8);
            _chunkSize.y = Mathf.Max(Mathf.RoundToInt(chunkSize.y / 8) * 8, 8);
            _chunkSize.z = Mathf.Max(Mathf.RoundToInt(chunkSize.z / 8) * 8, 8);
            //if (_chunkSize != chunkSize) { Debug.LogWarning("ChunkSize must be multiple of 8 graeter of equal to 8"); }
        }




        /// <summary>
        /// Sample Chunks Data
        /// </summary>
        /// <param name="chunkData"></param>
        /// <returns></returns>
        /*
        public float[] SampleChunkData(ChunkData chunkData)
        {
            float[] sampleData = new float[(_chunkSize.x + 1) * (_chunkSize.y + 1) * (_chunkSize.z + 1)];
            float terrainScope = (_surfaceRange.y - _surfaceRange.x);

            iNoise[0].width = (_chunkSize.x + 1);
            iNoise[0].height = (_chunkSize.z + 1);
            iNoise[0].offset = new Vector2(
                (float)_chunkSize.x * chunkData.index.x / (_chunkSize.x + 1), 
                (float)_chunkSize.z * chunkData.index.z / (_chunkSize.z + 1)
            );
            iNoise[0].Apply();

            int i = 0;
            for (int x = 0; x <= _chunkSize.x; x++)
            {
                for (int y = 0; y <= _chunkSize.y; y++)
                {
                    for (int z = 0; z <= _chunkSize.z; z++)
                    {
                        float n = iNoise[0].texture2D.GetPixel(x, z).r;
                        sampleData[i] = (_surfaceRange.x + terrainScope * n) - (chunkData.localPosition.y + y);
                        i++;
                    }
                }
            }

            return sampleData;
        }
        */
        /*
        public float[]SampleC (ChunkData chunkData)
        {
            float[] sampleData = new float[(_sampleResolution.x + 1) * (_sampleResolution.y + 1) * (_sampleResolution.z + 1)];
            float terrainScope = (_surfaceRange.y - _surfaceRange.x) * .25f;

            iNoise[0].width = (_sampleResolution.x + 1);
            iNoise[0].height = (_sampleResolution.z + 1);
            iNoise[0].offset = new Vector2(
                (float)_sampleResolution.x * chunkData.index.x / (_sampleResolution.x + 1),
                (float)_sampleResolution.z * chunkData.index.z / (_sampleResolution.z + 1)
            );
            iNoise[0].Apply();

            int i = 0;
            for (int x = 0; x <= _sampleResolution.x; x++)
            {
                for (int y = 0; y <= _sampleResolution.y; y++)
                {
                    for (int z = 0; z <= _sampleResolution.z; z++)
                    {
                        float n = iNoise[0].texture2D.GetPixel(x, z).r;
                        sampleData[i] = (_surfaceRange.x + terrainScope * n) - (chunkData.localPosition.y + y);
                        i++;
                    }
                }
            }

            return sampleData;
        }
        */
        /*
        public float[] SampleOctree()
        {
            float[] sampleData = new float[(_octreeRes.x + 1) * (_octreeRes.y + 1) * (_octreeRes.z + 1)];
            float terrainScope = (_surfaceRange.y - _surfaceRange.x);

            iNoise[0].width = (_octreeRes.x + 1);
            iNoise[0].height = (_octreeRes.z + 1);

            iNoise[0].tiling = new Vector2(1 / Mathf.Pow(2, _octreeLOD), 1 / Mathf.Pow(2, _octreeLOD));
            iNoise[0].offset = new Vector2(
                _octreeLOD == 0 ? 0 : 1 / Mathf.Pow(2, _octreeLOD),
                _octreeLOD == 0 ? 0 : 1 / Mathf.Pow(2, _octreeLOD)
            );

            iNoise[0].Apply();

            int i = 0;
            for (int x = 0; x <= _octreeRes.x; x++)
            {
                for (int y = 0; y <= _octreeRes.y; y++)
                {
                    for (int z = 0; z <= _octreeRes.z; z++)
                    {
                        float n = iNoise[0].texture2D.GetPixel(x, z).r;
                        sampleData[i] = (_surfaceRange.x + terrainScope * n) - (0 + y);
                        i++;
                    }
                }
            }

            return sampleData;
        }
        */
        /// <summary>
        /// Calculate the Chunk Index at this position (There may be no chunk at this position)
        /// </summary>
        /// <param name="position">The position to calculate the Chunk Index</param>
        /// <returns></returns>
        public Vector3Int GetChunkIndexAt(Vector3 position)
        {
            position = position - _position;
            Vector3Int chunkIndex = new Vector3Int(
                Mathf.FloorToInt(position.x / _chunkSize.x),
                Mathf.FloorToInt(position.y / _chunkSize.y),
                Mathf.FloorToInt(position.z / _chunkSize.z)
            );
            return chunkIndex;
        }
        /// <summary>
        /// Get the ChunkData from the Chunk at position
        /// </summary>
        /// <param name="position">The position to get the Chunk</param>
        /// <returns>Returns Chunk ChunkData on given Position, null if there is no Chunk at given Position</returns>
        public ChunkData GetChunkDataAt(Vector3 position)
        {
            Vector3Int chunkIndex = GetChunkIndexAt(position);

            if ((chunkIndex.x < 0) || (chunkIndex.x > _chunkBounds.x)) { return null; }
            if ((chunkIndex.y < 0) || (chunkIndex.y > _chunkBounds.y)) { return null; }
            if ((chunkIndex.z < 0) || (chunkIndex.z > _chunkBounds.z)) { return null; }

            return TerrainSystemUtility.LoadChunk(terrainData, chunkIndex);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="renderDistance"></param>
        /// <returns></returns>
        public Vector3Int[] ActiveJunks(Vector3 position, float renderDistance)
        {
            Vector3Int centerChunkIndex = GetChunkIndexAt(position);

            Vector3Int renderDistanceInChunks = new Vector3Int(
                Mathf.RoundToInt(renderDistance / _chunkSize.x),
                Mathf.RoundToInt(renderDistance / _chunkSize.y),
                Mathf.RoundToInt(renderDistance / _chunkSize.z)
            );

            int minX = Mathf.Max(centerChunkIndex.x - renderDistanceInChunks.x, 0);
            int maxX = Mathf.Min(centerChunkIndex.x + renderDistanceInChunks.x, _chunkBounds.x);

            int minY = Mathf.Max(centerChunkIndex.y - renderDistanceInChunks.y, 0);
            int maxY = Mathf.Min(centerChunkIndex.y + renderDistanceInChunks.y, _chunkBounds.y);

            int minZ = Mathf.Max(centerChunkIndex.z - renderDistanceInChunks.z, 0);
            int maxZ = Mathf.Min(centerChunkIndex.z + renderDistanceInChunks.z, _chunkBounds.z);

            Vector3Int[] activeChunksIndices = new Vector3Int[(maxX - minX) * (maxY - minY) * (maxZ - minZ)];
            Debug.Log((maxX - minX) + " " + (maxY - minY) + " " + (maxZ - minZ));
            int i = 0;

            for (int x = minX; x < maxX; x++)
            {
                for (int y = minY; y < maxY; y++)
                {
                    for (int z = minZ; z < maxZ; z++)
                    {
                        activeChunksIndices[i] = new Vector3Int(x, y, z);
                        i++;
                    }
                }
            }
            return activeChunksIndices;
        }
        public List<Vector3Int> ActiveJunkIDs(Vector3 position, float renderDistance)
        {
            List<Vector3Int> chunkIDs = new List<Vector3Int>();

            Vector3Int centerChunkIndex = GetChunkIndexAt(position);

            Vector3Int renderDistanceInChunks = new Vector3Int(
                Mathf.RoundToInt(renderDistance / _chunkSize.x),
                Mathf.RoundToInt(renderDistance / _chunkSize.y),
                Mathf.RoundToInt(renderDistance / _chunkSize.z)
            );

            int minX = Mathf.Max(centerChunkIndex.x - renderDistanceInChunks.x, 0);
            int maxX = Mathf.Min(centerChunkIndex.x + renderDistanceInChunks.x, _chunkBounds.x);

            int minY = Mathf.Max(centerChunkIndex.y - renderDistanceInChunks.y, 0);
            int maxY = Mathf.Min(centerChunkIndex.y + renderDistanceInChunks.y, _chunkBounds.y);

            int minZ = Mathf.Max(centerChunkIndex.z - renderDistanceInChunks.z, 0);
            int maxZ = Mathf.Min(centerChunkIndex.z + renderDistanceInChunks.z, _chunkBounds.z);

            for (int x = minX; x < maxX; x++)
            {
                for (int y = minY; y < maxY; y++)
                {
                    for (int z = minZ; z < maxZ; z++)
                    {
                        chunkIDs.Add(new Vector3Int(x, y, z));
                    }
                }
            }

            return chunkIDs;
        }
    }
}

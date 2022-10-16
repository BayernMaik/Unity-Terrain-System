using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TerrainSystem
{
    public class Terrain
    {
        // Variables
        #region Variables
        #region Terrain Data
        private TerrainData data;
        public TerrainData Data
        {
            get { return data; }
            set { data = value; }
        }
        #endregion
        #region Terrain Component
        private TerrainComponent component;
        public TerrainComponent Component
        { 
            get { return component; } 
            set { component = value; }
        }
        public GameObject gameObject
        {
            get { return component.gameObject; }
        }
        #endregion


        // Terrain Position
        #region Terrain Position
        private Vector3 _position = Vector3.zero;
        #endregion
        // Terrain Size
        #region Terrain Size
        private Vector3 _size = new Vector3(1024.0f, 512.0f, 1024.0f);
        public Vector3 size { get { return _size; } }
        public float width { get { return _size.x; } }
        public float height { get { return _size.y; } }
        public float length { get { return _size.z; } }
        #endregion
        // Terrain Surface
        #region Terrain Surface Range
        private float _minSurface = 0.0f;
        private float _maxSurface = 512.0f;
        #endregion
        // Chunk Resolution
        #region Chunk Resolution
        private Vector3Int _chunkGrid = new Vector3Int(32, 16, 32);
        public Vector3Int chunkGrid { get { return _chunkGrid; } }
        #endregion
        // Chunk Size
        #region Chunk Size
        private Vector3 _chunkSize;
        public Vector3 chunkSize { get { return _chunkSize; } }
        #endregion

        #endregion

        #region Constructors
        #region Default Constructor
        public Terrain(){}
        #endregion
        #region TerrainDataConstructor
        public Terrain(TerrainData terrainData)
        {
            this.data = terrainData;
            this.component = new GameObject(terrainData.DisplayName).AddComponent<TerrainComponent>();
            component.terrain = this;

            component.terrainData = terrainData;
            component.Build();
        }
        #endregion
        // HeightMap Constructor
        #region HeightMap Constructor
        public Terrain(Texture2D heightMap, Vector3 size)
        {
            // Set Size
            _size = size;
            // Recalculate Chunk Size
            ApplySize();
        }
        #endregion

        public Terrain(TerrainDataAsset terrainData)
        {

        }
        #endregion

        // Methods
        #region Methods

        // Set Terrain Size
        #region Set Terrain Size
        public void SetSize(Vector3 size)
        {
            _size = size;
        }
        public void SetSize(Vector3 size, bool applySize = false)
        {
            _size = size;
            if (applySize)
            {
                ApplySize();
            }
        }
        public void SetSize(float width, float height, float length)
        {
            _size.x = width;
            _size.y = height;
            _size.z = length;
        }
        public void SetSize(float width, float height, float length, bool applySize = false)
        {
            _size.x = width;
            _size.y = height;
            _size.z = length;
            if (applySize)
            {
                ApplySize();
            }
        }

        public void SetWidth(float width)
        {
            _size.x = width;
        }
        public void SetWidth(float width, bool applySize = false)
        {
            _size.x = width;
            if (applySize)
            {
                ApplySize();
            }
        }

        public void SetHeight(float height)
        {
            _size.y = height;
        }
        public void SetHeight(float height, bool applySize = false)
        {
            _size.y = height;
            if (applySize)
            {
                ApplySize();
            }
        }

        public void SetLength(float length)
        {
            _size.z = length;
        }
        public void SetLength(float length, bool applySize = false)
        {
            _size.z = length;
            if (applySize)
            {
                ApplySize();
            }
        }
        #endregion
        // Set Chunk Resolution
        #region Set Chunk Resolution
        public void SetChunkResolution(Vector3Int chunkResolution)
        {
            _chunkGrid = chunkResolution;
        }
        public void SetChunkResolution(Vector3Int chunkResolution, bool applySize = false)
        {
            _chunkGrid = chunkResolution;
            if (applySize)
            {
                ApplySize();
            }
        }
        public void SetChunkResolution(int x, int y, int z)
        {
            _chunkGrid.x = x;
            _chunkGrid.y = y;
            _chunkGrid.z = z;
        }
        public void SetChunkResolution(int x, int y, int z, bool applySize = false)
        {
            _chunkGrid.x = x;
            _chunkGrid.y = y;
            _chunkGrid.z = z;
            if (applySize)
            {
                ApplySize();
            }
        }
        #endregion
        // Apply Size
        #region Apply Size
        public void ApplySize()
        {
            _chunkSize.x = _size.x / _chunkGrid.x;
            _chunkSize.y = _size.y / _chunkGrid.y;
            _chunkSize.z = _size.z / _chunkGrid.z;
        }
        #endregion

        // SetHeightMap
        #region SetHeightMap
        #endregion

        // Local Position
        #region Local Position
        public Vector3 LocalPosition(Vector3 worldSpacePosition)
        {
            return gameObject.transform.InverseTransformPoint(worldSpacePosition);
        }
        #endregion

        // Get Chunk At Position
        #region Get Chunk At Position
        private Vector3 _localPosition;
        private Vector3Int _chunkID;
        public Vector3Int ChunkAtPosition(Vector3 position)
        {
            // Position in Terrains Local Space
            _localPosition = gameObject.transform.InverseTransformPoint(position);
            // Find Chunk Index
            _chunkID.x = Mathf.FloorToInt(_localPosition.x / _chunkSize.x);
            _chunkID.y = Mathf.FloorToInt(_localPosition.y / _chunkSize.y);
            _chunkID.z = Mathf.FloorToInt(_localPosition.z / _chunkSize.z);

            return _chunkID;
        }
        #endregion

        // Octree
        private int _startDepth = 2;
        private Vector3 _octreeTargetPosition;
        public void Octree(Vector3 position)
        {
            Vector3 _localPosition = gameObject.transform.InverseTransformPoint(position);
            _octreeTargetPosition = _localPosition;

            Vector3Int _chunkID = Vector3Int.zero;
            _chunkID.x = Mathf.FloorToInt(_localPosition.x / _chunkSize.x);
            _chunkID.y = Mathf.FloorToInt(_localPosition.y / _chunkSize.y);
            _chunkID.z = Mathf.FloorToInt(_localPosition.z / _chunkSize.z);
            Vector3 _chunkPosition = Vector3Utility.multiply(_chunkID, _chunkSize);

            Vector3 _halfSize = _chunkSize * 0.5f;
            OctreeDepth(_startDepth, OctreeIndex._0, _chunkPosition + Vector3Utility.multiply(OctreeIndex._0, _halfSize), _halfSize); // (0, 0, 0) // 0
            OctreeDepth(_startDepth, OctreeIndex._1, _chunkPosition + Vector3Utility.multiply(OctreeIndex._1, _halfSize), _halfSize); // (1, 0, 0) // 1
            OctreeDepth(_startDepth, OctreeIndex._2, _chunkPosition + Vector3Utility.multiply(OctreeIndex._2, _halfSize), _halfSize); // (0, 0, 1) // 2
            OctreeDepth(_startDepth, OctreeIndex._3, _chunkPosition + Vector3Utility.multiply(OctreeIndex._3, _halfSize), _halfSize); // (1, 0, 1) // 3
            OctreeDepth(_startDepth, OctreeIndex._4, _chunkPosition + Vector3Utility.multiply(OctreeIndex._4, _halfSize), _halfSize); // (0, 1, 0) // 4
            OctreeDepth(_startDepth, OctreeIndex._5, _chunkPosition + Vector3Utility.multiply(OctreeIndex._5, _halfSize), _halfSize); // (1, 1, 0) // 5
            OctreeDepth(_startDepth, OctreeIndex._6, _chunkPosition + Vector3Utility.multiply(OctreeIndex._6, _halfSize), _halfSize); // (0, 1, 1) // 6
            OctreeDepth(_startDepth, OctreeIndex._7, _chunkPosition + Vector3Utility.multiply(OctreeIndex._7, _halfSize), _halfSize); // (1, 1, 1) // 7
        }
        private void OctreeDepth(int depth, Vector3Int _index, Vector3 _octreePosition, Vector3 _octreeSize)
        {
            // This Octree Position
            Vector3 _localPosition = _octreeTargetPosition - _octreePosition;
            bool _isTarget = _localPosition.InBounds(Vector3.zero, _octreeSize);

            // Continue with Subdivision if this Octree is Target
            if (_isTarget)
            {
                // Break Condition
                if (depth > 0)
                {
                    // OctreeDepth(depth - 1, )
                }
            }
            else // Create this Octree
            {

            }
        }


        /// <summary>
        /// Calculate the Chunk Index at this position (There may be no chunk at this position)
        /// </summary>
        /// <param name="position">The position to calculate the Chunk Index</param>
        /// <returns></returns>
        public Vector3Int GetChunkIndexAt(Vector3 position)
        {
            position = position - component.gameObject.transform.position;
            Vector3Int chunkIndex = new Vector3Int(
                Mathf.FloorToInt(position.x / data.chunkSize.x),
                Mathf.FloorToInt(position.y / data.chunkSize.y),
                Mathf.FloorToInt(position.z / data.chunkSize.z)
            );
            return chunkIndex;
        }

        public void RenderAtPosition(Vector3 position)
        {
            // Chunk Index at Position
            position = position - component.gameObject.transform.position;
            Vector3Int chunkIndex = new Vector3Int(
                Mathf.FloorToInt(position.x / data.chunkSize.x),
                Mathf.FloorToInt(position.y / data.chunkSize.y),
                Mathf.FloorToInt(position.z / data.chunkSize.z)
            );

            // Active Chunks in Range
        }
        
        /// <summary>
        /// Get the ChunkData from the Chunk at position
        /// </summary>
        /// <param name="position">The position to get the Chunk</param>
        /// <returns>Returns Chunk ChunkData on given Position, null if there is no Chunk at given Position</returns>
        public ChunkData GetChunkDataAt(Vector3 position)
        {
            Vector3Int chunkIndex = GetChunkIndexAt(position);

            if ((chunkIndex.x < 0) || (chunkIndex.x > data.chunkBounds.x)) { return null; }
            if ((chunkIndex.y < 0) || (chunkIndex.y > data.chunkBounds.y)) { return null; }
            if ((chunkIndex.z < 0) || (chunkIndex.z > data.chunkBounds.z)) { return null; }

            return TerrainSystemUtility.LoadChunk(data, chunkIndex);
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
                Mathf.RoundToInt(renderDistance / data.chunkSize.x),
                Mathf.RoundToInt(renderDistance / data.chunkSize.y),
                Mathf.RoundToInt(renderDistance / data.chunkSize.z)
            );

            int minX = Mathf.Max(centerChunkIndex.x - renderDistanceInChunks.x, 0);
            int maxX = Mathf.Min(centerChunkIndex.x + renderDistanceInChunks.x, data.chunkBounds.x);

            int minY = Mathf.Max(centerChunkIndex.y - renderDistanceInChunks.y, 0);
            int maxY = Mathf.Min(centerChunkIndex.y + renderDistanceInChunks.y, data.chunkBounds.y);

            int minZ = Mathf.Max(centerChunkIndex.z - renderDistanceInChunks.z, 0);
            int maxZ = Mathf.Min(centerChunkIndex.z + renderDistanceInChunks.z, data.chunkBounds.z);

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
                Mathf.RoundToInt(renderDistance / data.chunkSize.x),
                Mathf.RoundToInt(renderDistance / data.chunkSize.y),
                Mathf.RoundToInt(renderDistance / data.chunkSize.z)
            );

            int minX = Mathf.Max(centerChunkIndex.x - renderDistanceInChunks.x, 0);
            int maxX = Mathf.Min(centerChunkIndex.x + renderDistanceInChunks.x, data.chunkBounds.x);

            int minY = Mathf.Max(centerChunkIndex.y - renderDistanceInChunks.y, 0);
            int maxY = Mathf.Min(centerChunkIndex.y + renderDistanceInChunks.y, data.chunkBounds.y);

            int minZ = Mathf.Max(centerChunkIndex.z - renderDistanceInChunks.z, 0);
            int maxZ = Mathf.Min(centerChunkIndex.z + renderDistanceInChunks.z, data.chunkBounds.z);

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

        public void SetSize(Vector3Int size)
        {
        }
        #endregion
    }

    public static class OctreeIndex
    {
        public static readonly Vector3Int _0 = new Vector3Int(0, 0, 0);
        public static readonly Vector3Int _1 = new Vector3Int(1, 0, 0);
        public static readonly Vector3Int _2 = new Vector3Int(0, 0, 1);
        public static readonly Vector3Int _3 = new Vector3Int(1, 0, 1);
        public static readonly Vector3Int _4 = new Vector3Int(0, 1, 0);
        public static readonly Vector3Int _5 = new Vector3Int(1, 1, 0);
        public static readonly Vector3Int _6 = new Vector3Int(0, 1, 1);
        public static readonly Vector3Int _7 = new Vector3Int(1, 1, 1);
    }
}
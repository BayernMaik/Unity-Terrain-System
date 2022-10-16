using System;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainSystem
{
    [Serializable]
    public class TerrainData
    {
        #region Variables
        #region Terrain Name
        private string displayName = "New Terrain";
        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }
        #endregion
        #region Terrain Size
        private Vector3 size = new Vector3(1000, 512, 1000);
        public Vector3 Size
        {
            get { return size; }
            set { size = value; }
        }
        public float Width
        {
            get { return size.x; }
            set { this.SetWidth(value); }
        }
        public float Length
        {
            get { return size.z; }
            set { this.SetLength(value); }
        }
        public float Height
        {
            get { return size.y; }
            set { this.SetHeight(value); }
        }
        public float GetWidth()
        {
            return this.size.x;
        }
        public float GetLength()
        {
            return this.size.z;
        }
        public float GetHeight()
        {
            return this.size.y;
        }
        public void SetSize(Vector3 size)
        {
            this.size = size;
        }
        public void SetWidth(float width)
        {
            this.size.x = width;
        }
        public void SetLength(float length)
        {
            this.size.z = length;
        }
        public void SetHeight(float height)
        {
            this.size.y = height;
        }
        #endregion
        #region Terrain Surface
        private float surfaceMin = 0.0f;
        public float SurfaceMin
        {
            get { return this.surfaceMin; }
            set { this.surfaceMin = value; }
        }
        private float surfaceMax = 512.0f;
        public float SurfaceMax
        {
            get { return this.surfaceMax; }
            set { this.surfaceMax = value; }
        }
        #endregion
        #region Terrain HeightMap
        private Texture2D heightMap;
        public Texture2D HeightMap
        {
            get { return heightMap; }
            set { heightMap = value; }
        }
        #endregion



        private bool _smoothShade = true;
        private float _surfaceThreshold = 0.5f;
        private bool _tryKeepRange = true;
        private Vector2Int _surfaceRange = new Vector2Int(0, 16); // X - lowest Point, Y - Surface Range above lowest point
        private Vector3 _position = Vector3.zero;


        private Vector3Int _minTerrainSize = new Vector3Int(1, 1, 1);
        private Vector3Int _maxTerrainSize = new Vector3Int(32, 32, 32);
        private Vector3Int _defaultSize = new Vector3Int(1, 1, 1);
        public Vector3Int minTerrainSize { get { return _minTerrainSize; } }
        public Vector3Int maxTerrainSize { get { return _maxTerrainSize; } }
        public Vector3Int defaultSize { get { return _defaultSize; } }


        private Vector3Int _chunkBounds = new Vector3Int(4, 4, 4); // Chunks in Terrain
        private Vector3Int _chunkSize = new Vector3Int(32, 32, 32); // Size of a Chunk in Units
        private Vector3Int _voxelResolution = new Vector3Int(64, 64, 64); // Chunks Voxel Density

        #endregion

        #region Constructors
        public TerrainData()
        {
            //_noiseData = new List<Elapsed.Noise.INoise>();
            //_noiseData.Add(new Elapsed.Noise.Voronoi2D());
        }
        #endregion

        #region GetterSetter
        public float surfaceThreshold { get { return _surfaceThreshold; } }
        public bool smoothShade { get { return _smoothShade; } set { _smoothShade = value; } }
        public Vector3Int voxelResolution { get { return _voxelResolution; } set { _voxelResolution = value; } }
        public Vector3Int chunkSize { get { return _chunkSize; } }
        public Vector3Int chunkBounds { get { return _chunkBounds; } set { _chunkBounds = value; RecalculateSurfaceRange(); } }

        public Vector2Int surfaceRange { get { return _surfaceRange; } set { _surfaceRange = value; } }
        public int height { get { return _chunkBounds.y * chunkSize.y; } }
        #endregion

        #region Methods
        /// <summary>
        /// Recalculate Surface if Terrain is resized and surfaceRange exceeds Terrain bounds
        /// </summary>
        private void RecalculateSurfaceRange()
        {
            if (_tryKeepRange)
            {
                if (_surfaceRange.x + _surfaceRange.y > this.height)
                {
                    int _diff = _surfaceRange.x + _surfaceRange.y - this.height;
                    _surfaceRange.x = _surfaceRange.x - _diff;
                    if (_surfaceRange.x < 0)
                    {
                        _surfaceRange.y += _surfaceRange.x;
                        _surfaceRange.x = 0;
                    }
                }
            }
            else
            {
                if (_surfaceRange.x + _surfaceRange.y > this.height)
                {
                    // Collapse Range first, then lower min
                    int _diff = _surfaceRange.x + _surfaceRange.y - this.height;
                    _surfaceRange.y = _surfaceRange.y - _diff;
                    if (_surfaceRange.y < 0)
                    {
                        _surfaceRange.x += _surfaceRange.y;
                        _surfaceRange.y = 0;
                    }
                }
            }
        }
        /*
        public float[] GenerateChunkVoxelData(ChunkData chunkData)
        {
            float[] voxelData = new float[(_voxelResolution.x + 1) * (_voxelResolution.y + 1) * (_voxelResolution.z + 1)];
            float terrainScope = (_surfaceRange.y - _surfaceRange.x) * .5f;

            // Calculate Noise Tile for this Chunk
            _noiseData[0].width = (_voxelResolution.x + 1);
            _noiseData[0].height = (_voxelResolution.z + 1);
            _noiseData[0].offset = new Vector2(
                (float)_voxelResolution.x * chunkData.index.x / (_voxelResolution.x + 1),
                (float)_voxelResolution.z * chunkData.index.z / (_voxelResolution.z + 1)
            );
            _noiseData[0].Apply();

            // Build Voxel Data Values
            float noisePixel;
            int i = 0;
            for (int x = 0; x <= _voxelResolution.x; x++)
            {
                for (int y = 0; y <= _voxelResolution.y; y++)
                {
                    for (int z = 0; z <= _voxelResolution.z; z++)
                    {
                        noisePixel = _noiseData[0].texture2D.GetPixel(x, z).r;
                        
                        voxelData[i] = (_surfaceRange.x + terrainScope * noisePixel) - (chunkData.localPosition.y + y);
                        // Fill Ground Holes
                        if (Mathf.Approximately(chunkData.index.y + y, _surfaceRange.x) && (noisePixel <= _surfaceThreshold))
                        {
                            voxelData[i] = 1;
                        }

                        i++;
                    }
                }
            }

            return voxelData;
        }
        */
        #endregion
    }
}
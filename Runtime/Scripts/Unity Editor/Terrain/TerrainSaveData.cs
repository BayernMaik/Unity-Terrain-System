using System;
using UnityEngine;

namespace TerrainSystem
{
    /// <summary>
    /// Converter Class to make TerrainData Serializable
    /// </summary>
    [Serializable]
    public class TerrainSaveData
    {
        // Variables
        [SerializeField] private string _name;
        [SerializeField] private int[] _chunkSize;
        [SerializeField] private int[] _chunkBounds;
        [SerializeField] private float[] _surfaceRange;
        [SerializeField] private float _surfaceThreshold;

        // Constructors
        public TerrainSaveData(TerrainDataAsset terrainData)
        {
            _name = terrainData.name;
            _chunkSize = SerializeChunkSize(terrainData.chunkSize);
            _chunkBounds = SerializeChunkBounds(terrainData.chunkBounds);
            _surfaceRange = SerializeSurfaceRange(terrainData.surfaceRange);
            _surfaceThreshold = terrainData.surfaceThreshold;
        }

        // Methods
        private int[] SerializeChunkSize(Vector3Int chunkSize)
        {
            int[] returnArray = new int[3];
            returnArray[0] = chunkSize.x;
            returnArray[1] = chunkSize.y;
            returnArray[2] = chunkSize.z;
            return returnArray;
        }
        private int[] SerializeChunkBounds(Vector3Int chunkBounds)
        {
            int[] returnArray = new int[3];
            returnArray[0] = chunkBounds.x;
            returnArray[1] = chunkBounds.y;
            returnArray[2] = chunkBounds.z;
            return returnArray;
        }
        private float[] SerializeSurfaceRange(Vector2 surfaceRange)
        {
            float[] returnArray = new float[2];
            returnArray[0] = surfaceRange.x;
            returnArray[1] = surfaceRange.y;
            return returnArray;
        }
    }
}
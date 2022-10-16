using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainSystem
{
    [Serializable]
    public class MeshData
    {
        [SerializeField] private byte[] _voxelGrid;
        public byte[] sampleData { get { return _voxelGrid; } set { _voxelGrid = value; } }
    }
}
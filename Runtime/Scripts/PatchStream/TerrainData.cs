using System;
using UnityEngine;

namespace Elapsed.Terrain.PatchStream.Component
{
    [Serializable]
    public class TerrainData
    {
        #region Variables
        #region Terrain Name
        private string name = "New Terrain";
        public string Name
        {
            get { return name; }
            set { name = value; }
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
        #region Resolution
        public Vector2Int Resolution
        {
            get { return new Vector2Int(this.heightMap.width, this.heightMap.height); }
        }
        #endregion


        #region Patch Resolution
        private Vector2Int patchResolution = new Vector2Int(128, 128);
        public Vector2Int PatchResolution
        {
            get { return patchResolution; }
        }
        #endregion
        #region Terrain Patch Dimensions
        private Vector2Int patchDimensions = new Vector2Int(8, 8);
        /// <summary>
        /// 
        /// </summary>
        public Vector2Int PatchDimensions
        {
            get { return this.patchDimensions; }
            set { this.patchDimensions = value; }
        }
        #endregion
        private Vector3 patchSize = new Vector3(125f, 64f, 125f);
        public Vector3 PatchSize
        {
            get { return this.patchSize; }
            set { this.patchSize = value; }
        }
        #endregion
    }
}
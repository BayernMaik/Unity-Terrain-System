using UnityEngine;

namespace Elapsed.Terrain.PatchStream.Component
{
    [CreateAssetMenu(fileName = "New Terrain", menuName = "Terrain System/Patch Stream Terrain/New Terrain", order = 1)]
    public class TerrainDataAsset : ScriptableObject
    {
        #region Variables
        #region Terrain Data
        [SerializeField] private TerrainData terrainData;
        public TerrainData TerrainData
        { 
            get { return this.terrainData; }
            set { this.terrainData = value; }
        }
        #endregion
        #region Terrain Name
        public string DisplayName
        {
            get { return this.terrainData.Name; }
            set { this.terrainData.Name = value; }
        }
        #endregion
        #region Terrain Size
        [SerializeField] private Vector3 _size = new Vector3(1000f, 600f, 1000f);
        public Vector3 Size
        {
            get { return this.terrainData.Size; }
            set { this.terrainData.Size = value; }
        }
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
        #region Patch Resolution
        public Vector2Int PatchResolution
        {
            get { return this.terrainData.PatchDimensions; }
        }
        #endregion
        #region Terrain HeightMap
        public Texture2D HeightMap
        {
            get { return this.terrainData.HeightMap; }
            set { this.terrainData.HeightMap = value; }
        }
        #endregion
        #endregion

        public Terrain DropToSceneView()
        {
            return new Terrain(this.terrainData);
        }
        // Detail Resolution
        #region Detail Resolution
        [SerializeField] private Vector2Int _detailResolution = new Vector2Int(1024, 1024);
        private Vector2Int _minDetailResolution = new Vector2Int(32, 32);
        private Vector2Int _maxDetailResolution = new Vector2Int(8192, 8192);
        public Vector2Int detailResolution
        {
            get { return _detailResolution; }
            set { SetDetailResolution(value); }
        }
        public void SetDetailResolution(Vector2Int value)
        {
            _detailResolution.x = Mathf.Clamp(value.x, _minDetailResolution.x, _maxDetailResolution.x);
            _detailResolution.y = Mathf.Clamp(value.y, _minDetailResolution.y, _maxDetailResolution.y);

            // _patchDimension.x = Mathf.Max(1, Mathf.FloorToInt((float)_detailResolution.x / _detailResolutionPerPatch.x));
            // _patchDimension.y = Mathf.Max(1, Mathf.FloorToInt((float)_detailResolution.y / _detailResolutionPerPatch.y));

            // _detailResolution.x = _patchDimension.x * _detailResolutionPerPatch.x;
            // _detailResolution.y = _patchDimension.y * _detailResolutionPerPatch.y;

            this.PatchSize();
        }
        #endregion
        // Detail Resolution Per Patch
        #region Detail Resolution Per Patch
        [SerializeField] private Vector2Int _detailResolutionPerPatch = new Vector2Int(128, 128);
        public Vector2Int detailResolutionPerPatch
        {
            get { return _detailResolutionPerPatch; }
            set { SetDetailResolutionPerPatch(value); }
        }
        private Vector2Int _minDetailResolutionPerPatch = new Vector2Int(8, 8);
        private Vector2Int _maxDetailResolutionPerPatch = new Vector2Int(255, 255);
        public void SetDetailResolutionPerPatch(Vector2Int value)
        {
            // Clamp Detail Resolution Per Patch
            _detailResolutionPerPatch.x = Mathf.Clamp(value.x, _minDetailResolutionPerPatch.x, _maxDetailResolutionPerPatch.x);
            _detailResolutionPerPatch.y = Mathf.Clamp(value.y, _minDetailResolutionPerPatch.y, _maxDetailResolutionPerPatch.y);

            // Multiple of two
            _detailResolutionPerPatch.x = Mathf.FloorToInt(_detailResolutionPerPatch.x / 2f) * 2;
            _detailResolutionPerPatch.y = Mathf.FloorToInt(_detailResolutionPerPatch.y / 2f) * 2;

            // _patchDimension.x = Mathf.FloorToInt((float)_detailResolution.x / _detailResolutionPerPatch.x);
            // _patchDimension.y = Mathf.FloorToInt((float)_detailResolution.y / _detailResolutionPerPatch.y);
            /*
            this.SetDetailResolution(
                new Vector2Int(
                    _detailResolutionPerPatch.x * _patchDimension.x,
                    _detailResolutionPerPatch.y * _patchDimension.y
                )
            );
            */
        }
        #endregion
        // Patch Size
        #region Patch Size
        private Vector3 _patchSize = new Vector3(125.0f, 600.0f, 125.0f);
        public Vector3 patchSize
        {
            get { return _patchSize; }
        }
        public float patchWidth
        {
            get { return _patchSize.x; }
        }
        public float patchHeight
        {
            get { return _patchSize.y; }
        }
        public float patchLength
        {
            get { return _patchSize.z; }
        }
        private void PatchSize()
        {
            // _patchSize.x = _size.x / _patchDimension.x;
            _patchSize.y = _size.y;
            // _patchSize.z = _size.z / _patchDimension.y;
        }
        #endregion   
    }
}
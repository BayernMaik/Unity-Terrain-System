using System.Collections.Generic;
using UnityEngine;

namespace Elapsed.Terrain.PatchStream.Component
{
    [ExecuteAlways]
    [ExecuteInEditMode]
    public class TerrainComponent : MonoBehaviour
    {
        // Variables
        #region Variables
        #region Terrain
        public Terrain terrain { get; set; }
        #endregion
        /*
        #region Display Name
        [SerializeField] private string _displayName = "New Terrain";
        public string displayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }
        #endregion
        // Terrain Size
        #region Terrain Size
        [SerializeField] private Vector3 _size = new Vector3(1000f, 600f, 1000f);
        public Vector3 size
        {
            get { return _size; }
            set { 
                _size = value;
                this.PatchSize();
                this.SurfaceRange(); 
            }
        }
        public float width
        {
            get { return _size.x; }
            set { 
                _size.x = value;
                this.PatchSize();
            }
        }
        public float height
        {
            get { return _size.y; }
            set { 
                _size.y = value;
                this.PatchSize();
                this.SurfaceRange();
            }
        }
        public float length
        {
            get { return _size.z; }
            set { 
                _size.z = value;
                this.PatchSize();
            }
        }
        #endregion
        // Surface Range
        #region Surface Range
        private float _minSurfaceRange = 0.0f;
        public float minSurfaceRange
        {
            get { return _minSurfaceRange; }
            set { _minSurfaceRange = value; SurfaceRange(); }
        }
        private float _maxSurfaceRange = 600.0f;
        public float maxSurfaceRange
        {
            get { return _maxSurfaceRange; }
            set { _maxSurfaceRange = value; SurfaceRange(); }
        }
        private void SurfaceRange()
        {
            _maxSurfaceRange = Mathf.Clamp(_maxSurfaceRange, 0.0f, _size.y);
            _minSurfaceRange = Mathf.Clamp(_minSurfaceRange, 0.0f, _maxSurfaceRange);
        }
        #endregion
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

            _patchDimension.x = Mathf.Max(1, Mathf.FloorToInt((float)_detailResolution.x / _detailResolutionPerPatch.x));
            _patchDimension.y = Mathf.Max(1, Mathf.FloorToInt((float)_detailResolution.y / _detailResolutionPerPatch.y));

            _detailResolution.x = _patchDimension.x * _detailResolutionPerPatch.x;
            _detailResolution.y = _patchDimension.y * _detailResolutionPerPatch.y;

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

            _patchDimension.x = Mathf.FloorToInt((float)_detailResolution.x / _detailResolutionPerPatch.x);
            _patchDimension.y = Mathf.FloorToInt((float)_detailResolution.y / _detailResolutionPerPatch.y);

            this.SetDetailResolution(
                new Vector2Int(
                    _detailResolutionPerPatch.x * _patchDimension.x,
                    _detailResolutionPerPatch.y * _patchDimension.y
                )
            );
        }
        #endregion
        // Patch Dimension
        #region Patch Dimension
        [SerializeField] private Vector2Int _patchDimension = new Vector2Int(8, 8);
        public Vector2Int patchDimension
        {
            get { return _patchDimension; }
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
            _patchSize.x = _size.x / _patchDimension.x;
            _patchSize.y = _size.y;
            _patchSize.z = _size.z / _patchDimension.y;
        }
        #endregion
        // Height Map
        #region Height Map
        [SerializeField] private Texture2D _heightMap;
        public Texture2D heightMap
        {
            get { return _heightMap; }
            set {
                _heightMap = value;
                this.ResampleHeightMap();
            }
        }
        public void ResampleHeightMap()
        {
            //_heightMap = Texture2DUtility.ResampleHorizontalLerp(_heightMap, _detailResolution + Vector2Int.one);
        }
        #endregion
        */
        // Patch Array
        #region Patch Array
        private Patch[,] _patchArray = new Patch[8, 8];
        public void ClearPatchArray()
        {
            for (int _y = 0; _y < _patchArray.GetLength(0); _y++)
            {
                for (int _x = 0; _x < _patchArray.GetLength(1); _x++)
                {
                    _patchArray[_y, _x].Destroy();
                    _patchArray[_y, _x] = null;
                }
            }
        }
        public Patch GetPatch(Vector2Int index)
        {
            return _patchArray[index.x, index.y];
        }
        #endregion
        // Patch Hide Flags
        #region Patch Hide Flags
        private HideFlags _patchHhideFlags = HideFlags.None;
        public HideFlags patchHideFlags
        {
            get { return hideFlags; }
        }
        #endregion
        #endregion

        #region MonoBehaviors
        #region OnEnable
        private void OnEnable()
        {
#if (UNITY_EDITOR)
            #region UnityEditor
            UnityEditor.EditorApplication.update += this.UnityEditor_EditorApplication_Update;
            #endregion
#endif
        }
        #endregion
        #region OnDisable
        private void OnDisable()
        {
#if (UNITY_EDITOR)
            #region UnityEditor            
            UnityEditor.EditorApplication.update -= this.UnityEditor_EditorApplication_Update;
            #endregion
#endif
        }
        #endregion
        #endregion

        #region Methods
        /*
        private Vector2Int _lastIndex;
        private int _minLod = 0, _maxLod = 4;
        public void BuildFromPosition(Vector3 position, Space space = Space.World)
        {
            // LOD 0 Patch
            Vector2Int index = this.terrain.GetPatchIndexAtPosition(position);
            _lastIndex = index;
            Vector2Int offset = index * _detailResolutionPerPatch;
            Vector2Int dimension = _detailResolutionPerPatch;

            // Center Patch
            Patch _patch = new Patch(this.terrain, offset, dimension);
            _patch.meshFilter.mesh = GenerateMesh(
                ref _heightMap,
                -new Vector3(_patchSize.x * 0.5f, _patchSize.y * 0.5f, _patchSize.z * 0.5f),
                _patchSize,
                _minSurfaceRange,
                _maxSurfaceRange,
                offset,
                dimension,
                new Vector2Int(_minLod, _minLod),
                new TransitionCellData()
            );

            int _r = 1;
            for (int _lod = _minLod; _lod <= _maxLod; _lod++)
            {
                for (int _i = 0; _i < 8; _i++)
                {
                    Vector2Int _thisOffset = offset + Patch.res[_i] * dimension * _r;
                    _patch = new Patch(this.terrain, _thisOffset, dimension * _r);
                    _patch.meshFilter.mesh = GenerateMesh(
                        ref _heightMap,
                        -new Vector3(_patchSize.x * _r * 0.5f, _patchSize.y * 0.5f, _patchSize.z * _r * 0.5f),
                        _patchSize * _r,
                        _minSurfaceRange,
                        _maxSurfaceRange,
                        _thisOffset,
                        dimension * _r,
                        new Vector2Int(_lod, _lod),
                        TransitionCellData.patch[_i]
                    );
                }
                offset += Patch.res[5] * dimension * _r;
                _r *= 3;
                //dimension = _detailResolutionPerPatch * _r;
            }
        }

        public void ReBuildPatches()
        {
            for (int _y = 0; _y < _patchArray.GetLength(0); _y++)
            {
                for (int _x = 0; _x < _patchArray.GetLength(1); _x++)
                {
                    if (_patchArray[_y, _x] == null)
                    {
                        _patchArray[_y, _x] = new Patch(this, new Vector2Int(_y, _x));
                    }

                    Mesh _mesh = GenerateMesh(
                        ref _heightMap,
                        -_patchSize * 0.5f,
                        new Vector2Int(_x, _y) * _detailResolutionPerPatch,
                        Vector2Int.zero,
                        new TransitionCellData(true, true, true, true)
                   );
                    _patchArray[_y, _x].meshFilter.mesh = _mesh;
                }
            }
        }
        private Mesh GenerateMesh(ref Texture2D texture2D, Vector3 minPos, Vector2Int resolutionOffset, Vector2Int lod, TransitionCellData _transition)
        {   
            // Level Of Detail
            lod.x = (int)Mathf.Pow(2, lod.x); // LOD Step
            lod.y = (int)Mathf.Pow(2, lod.y); // LOD Step

            // Vertex Dimension
            Vector2Int _vertexDimension = Vector2Int.zero;
            _vertexDimension.x = Mathf.CeilToInt((float)(_detailResolutionPerPatch.x + 1) / lod.x);
            _vertexDimension.y = Mathf.CeilToInt((float)(_detailResolutionPerPatch.y + 1) / lod.y);

            // Cell Dimension
            Vector2Int _cellDimension = _vertexDimension - Vector2Int.one;

            Vector2 _uvStep = Vector2.zero;
            _uvStep.x = 1f / _cellDimension.x;
            _uvStep.y = 1f / _cellDimension.y;

            // Calculate Vertex Length
            Vector2Int _meshCoreDimension = _vertexDimension;
            _meshCoreDimension.x -= _transition.Left ? 1 : 0;
            _meshCoreDimension.x -= _transition.Right ? 1 : 0;
            _meshCoreDimension.y -= _transition.Forward ? 1 : 0;
            _meshCoreDimension.y -= _transition.Back ? 1 : 0;
            Vector2Int _transitionDimension = new Vector2Int(
                _cellDimension.x / 2,
                _cellDimension.y / 2
            );
            int _verticesLength = _meshCoreDimension.x * _meshCoreDimension.y; // Mesh Core            
            if (_transition.Left)
            {
                _verticesLength += _transitionDimension.y;
                if (!_transition.Forward)
                {
                    _verticesLength += 1;
                }
            }
            if (_transition.Forward)
            {
                _verticesLength += _transitionDimension.x;
                if (!_transition.Right)
                {
                    _verticesLength += 1;
                }
            }
            if (_transition.Right)
            {
                _verticesLength += _transitionDimension.y;
                if (!_transition.Back)
                {
                    _verticesLength += 1;
                }
            }
            if (_transition.Back)
            {
                _verticesLength += _transitionDimension.x;
                if (!_transition.Left)
                {
                    _verticesLength += 1;
                }
            }

            Vector2Int _pixelOffset = new Vector2Int(
                _transition.Left ? 1 : 0,
                _transition.Back ? 1 : 0
            );

            float _xOffset = _transition.Left ? _uvStep.x * _patchSize.x : 0;
            float _yOffset = _transition.Back ? _uvStep.y * _patchSize.z : 0;

            // Mesh Core Vertices
            #region Mesh Core Vertices
            int _v = 0;
            Vector3[] _vertices = new Vector3[_verticesLength];
            for (int _z = 0; _z < _meshCoreDimension.y; _z++)
            {
                for (int _x = 0; _x < _meshCoreDimension.x; _x++)
                {
                    // Add Vertex
                    _vertices[_v] = minPos;
                    _vertices[_v].x += _xOffset + _x * _uvStep.x * _patchSize.x;    // Vertex X-Position
                    _vertices[_v].y += _minSurfaceRange + texture2D.GetPixel(       // Vertex Y-Position
                            resolutionOffset.x + _pixelOffset.x + _x * lod.x,       // Texture2D X-Coordinate
                            resolutionOffset.y + _pixelOffset.y + _z * lod.y        // Texture2D Y-Coordinate
                        ).r * _maxSurfaceRange;
                    _vertices[_v].z += _yOffset + _z * _uvStep.y * _patchSize.z;    // Vertex Z-Position
                    _v++;
                }
            }
            #endregion
            // Left Transition Mesh Vertices
            #region Left Transition Mesh Vertices
            int _leftOffset = _v;
            if (_transition.Left)
            {
                // Add Left Transition Cell Vertex
                for (int _z = 0; _z < _cellDimension.y - 1; _z += 2)
                {
                    _vertices[_v] = minPos;
                    //_vertices[_v].x += 0f;                                        // Vertex X-Position
                    _vertices[_v].y += _minSurfaceRange + texture2D.GetPixel(       // Y-Position
                            resolutionOffset.x,                                     // X-Coordinate
                            resolutionOffset.y + _z * lod.y                         // Y-Coordinate
                        ).r * _maxSurfaceRange;
                    _vertices[_v].z += _z * _uvStep.y * _patchSize.z;               // Z-Position
                    _v++;
                }
                // Add Last Left Transition Cell Vertex
                if (!_transition.Forward)
                {
                    _vertices[_v] = minPos;
                    //_vertices[_v].x += 0f;                                        // Vertex X-Position
                    _vertices[_v].y += _minSurfaceRange + texture2D.GetPixel(       // Vertex Y-Position
                            resolutionOffset.x,                                     // Texture2D X-Coordinate
                            resolutionOffset.y + _detailResolutionPerPatch.y        // Texture2D Y-Coordinate
                        ).r * _maxSurfaceRange;
                    _vertices[_v].z += _patchSize.z;                                // Vertex Z-Position
                    _v++;
                }
            }
            #endregion
            // Forward Transition Mesh Vertices
            #region Forward Transition Mesh Vertices
            int _topOffset = _v;
            if (_transition.Forward)
            {
                // Add Forward Transition Cell Vertex
                for (int _x = 0; _x < _cellDimension.x - 1; _x += 2)
                {
                    _vertices[_v] = minPos;
                    _vertices[_v].x += _x * _uvStep.x * _patchSize.x;               // Vertex X-Position
                    _vertices[_v].y += _minSurfaceRange + texture2D.GetPixel(       // Vertex Y-Position
                        resolutionOffset.x + _x * lod.x,                            // Texture2D X-Coordinate
                        resolutionOffset.y + _detailResolutionPerPatch.y            // Texture2D Y-Coordinate
                    ).r * _maxSurfaceRange;
                    _vertices[_v].z += _patchSize.z;                                // Vertex Z-Position
                    _v++;
                }
                // Add Last Transition Cell Vertex
                if (!_transition.Right)
                {
                    _vertices[_v] = minPos;
                    _vertices[_v].x += _patchSize.x;                                // Vertex X-Position
                    _vertices[_v].y += _minSurfaceRange + texture2D.GetPixel(       // Vertex Y-Position
                        resolutionOffset.x + _detailResolutionPerPatch.x,           // Texture2D X-Coordinate
                        resolutionOffset.y + _detailResolutionPerPatch.y            // Texture2D Y-Coordinate
                    ).r * _maxSurfaceRange;
                    _vertices[_v].z += _patchSize.z;                                // Vertex Z-Position
                    _v++;
                }
            }
            #endregion
            // Right Transition Mesh Vertices
            #region Right Transition Mesh Vertices
            int _rightOffset = _v;
            if (_transition.Right)
            {
                // Add Right Transition Cell Vertex
                for (int _y = 0; _y < _cellDimension.y; _y += 2)
                {
                    _vertices[_v] = minPos;
                    _vertices[_v].x += _patchSize.x;                                        // Vertex X-Position
                    _vertices[_v].y += _minSurfaceRange + texture2D.GetPixel(               // Vertex Y-Position
                            resolutionOffset.x + _detailResolutionPerPatch.x,
                            resolutionOffset.y + _detailResolutionPerPatch.y - _y * lod.y
                        ).r * _maxSurfaceRange;
                    _vertices[_v].z += _patchSize.z - _y * _uvStep.y * _patchSize.z;        // Vertex Z-Position
                    _v++;
                }
                // Add Last Right Transition Cell Vertex
                if (!_transition.Back)
                {
                    _vertices[_v] = minPos;
                    _vertices[_v].x += _patchSize.x;                                        // Vertex X-Position
                    _vertices[_v].y += _minSurfaceRange + texture2D.GetPixel(               // Vertex Y-Position
                            resolutionOffset.x + _detailResolutionPerPatch.x,
                            resolutionOffset.y
                        ).r * _maxSurfaceRange;
                    // _vertices[_v].z += 0f;                                               // Vertex Z-Position
                    _v++;
                }
            }
            #endregion
            // Back Transition Mesh Vertices
            #region Back Transition Mesh Vertices
            int _bottomOffset = _v;
            if (_transition.Back)
            {
                // Add Back Transition Cell Vertex
                for (int _x = 0; _x < _cellDimension.x - 1; _x += 2)
                {
                    _vertices[_v] = minPos;
                    _vertices[_v].x += _patchSize.x - _x * _uvStep.x * _patchSize.x;    // Vertex X-Position
                    _vertices[_v].y += _minSurfaceRange + texture2D.GetPixel(
                            resolutionOffset.x + _detailResolutionPerPatch.x - _x * lod.x,
                            resolutionOffset.y
                        ).r * _maxSurfaceRange;
                    // _vertices[_v].z += 0f;                                               // Vertex Z-Position
                    _v++;
                }
                // Add Last Back Transition Cell Vertex
                if (!_transition.Left)
                {
                    _vertices[_v] = minPos;
                    // _vertices[_v].x += 0f;                                               // Vertex X-Position 
                    _vertices[_v].y += _minSurfaceRange + texture2D.GetPixel(               // Vertex Y-Position
                            resolutionOffset.x,
                            resolutionOffset.y
                        ).r * _maxSurfaceRange;
                    // _vertices[_v].z += 0f;                                               // Vertex Z-Position
                    _v++;
                }
            }
            #endregion

            // Build Triangles
            #region Mesh Core Triangles
            int _t = 0;
            List<int> _triangles = new List<int>();
            for (int _y = 0; _y < _meshCoreDimension.y - 1; _y++)
            {
                for (int _x = 0; _x < _meshCoreDimension.x - 1; _x++)
                {
                    // First Triangle
                    _triangles.Add(_x + _meshCoreDimension.x * _y);                             // Bottom Left Vertex
                    _triangles.Add(_x + _meshCoreDimension.x * _y + _meshCoreDimension.x);      // Top Left Vertex
                    _triangles.Add(_x + _meshCoreDimension.x * _y + 1);                         // Bottom Right Vertex

                    // Second Triangle
                    _triangles.Add(_x + _meshCoreDimension.x * _y + _meshCoreDimension.x + 1);  // Top Right Vertex
                    _triangles.Add(_x + _meshCoreDimension.x * _y + 1);                         // Bottom Right Vertex
                    _triangles.Add(_x + _meshCoreDimension.x * _y + _meshCoreDimension.x);      // Top Left Vertex

                    _t += 6;
                }
            }
            #endregion
            // Left Transition Mesh Triangles
            #region Left Transition Mesh Triangles
            if (_transition.Left)
            {
                // Cells
                if (!_transition.Back)
                {
                    // If Mesh has no Top Transition build full row, else leave last cell free
                    int _length = _cellDimension.y / 2 - (_transition.Forward ? 1 : 0);
                    for (int _y = 0; _y < _length; _y++)
                    {
                        // First Triangle
                        _triangles.Add(_meshCoreDimension.x + _meshCoreDimension.x * _y * 2);
                        _triangles.Add(_meshCoreDimension.x * _y * 2);
                        _triangles.Add(_leftOffset + _y);
                        // Second Triangle
                        _triangles.Add(_meshCoreDimension.x + _meshCoreDimension.x * _y * 2);
                        _triangles.Add(_leftOffset + _y);
                        _triangles.Add(_leftOffset + _y + 1);
                        // Third Triangle
                        _triangles.Add(_meshCoreDimension.x + _meshCoreDimension.x * _y * 2);
                        _triangles.Add(_leftOffset + _y + 1);
                        _triangles.Add(_meshCoreDimension.x * 2 + _meshCoreDimension.x * _y * 2);
                    }
                }
                else
                {
                    // Skip First Triangle
                    // Second Triangle
                    _triangles.Add(0);
                    _triangles.Add(_leftOffset);
                    _triangles.Add(_leftOffset + 1);
                    // Third Triangle
                    _triangles.Add(0);
                    _triangles.Add(_leftOffset + 1);
                    _triangles.Add(_meshCoreDimension.x);

                    // Fill Cells
                    int _length = _cellDimension.y / 2 - (_transition.Forward ? 2 : 1);
                    for (int _y = 0; _y < _length; _y++)
                    {
                        // First Triangle                       
                        _triangles.Add(_meshCoreDimension.x * 2 + _meshCoreDimension.x * 2 * _y);
                        _triangles.Add(_meshCoreDimension.x + _meshCoreDimension.x * 2 * _y);
                        _triangles.Add(_leftOffset + _y + 1);
                        // Second Triangle
                        _triangles.Add(_meshCoreDimension.x * 2 + _meshCoreDimension.x * 2 * _y);
                        _triangles.Add(_leftOffset + _y + 1);
                        _triangles.Add(_leftOffset + _y + 2);
                        // Third Triangle
                        _triangles.Add(_meshCoreDimension.x * 2 + _meshCoreDimension.x * 2 * _y);
                        _triangles.Add(_leftOffset + _y + 2);
                        _triangles.Add(_meshCoreDimension.x * 3 + _meshCoreDimension.x * 2 * _y);
                    }
                }
                // Last Cell
                if (_transition.Forward)
                {
                    // First Triangle
                    _triangles.Add(_meshCoreDimension.x * (_meshCoreDimension.y - 1));
                    _triangles.Add(_meshCoreDimension.x * (_meshCoreDimension.y - 2));
                    _triangles.Add(_leftOffset + _cellDimension.y / 2 - 1);
                    // Second Triangle
                    _triangles.Add(_meshCoreDimension.x * (_meshCoreDimension.y - 1));
                    _triangles.Add(_leftOffset + _cellDimension.y / 2 - 1);
                    _triangles.Add(_leftOffset + _cellDimension.y / 2);
                }
            }
            #endregion
            // Forward Transition Mesh Triangles
            #region  Forward Transition Mesh Triangles
            if (_transition.Forward)
            {
                if (!_transition.Left)
                {
                    int _length = _cellDimension.x / 2 - (_transition.Right ? 1 : 0);
                    for (int _x = 0; _x < _length; _x++)
                    {
                        // First Triangle
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x + 1 + _x * 2);
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x + _x * 2);
                        _triangles.Add(_topOffset + _x);
                        // Second Triangle
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x + 1 + _x * 2);
                        _triangles.Add(_topOffset + _x);
                        _triangles.Add(_topOffset + _x + 1);
                        // Third Triangle
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x + 1 + _x * 2);
                        _triangles.Add(_topOffset + _x + 1);
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x + 2 + _x * 2);
                    }
                }
                else
                {
                    // First Cell
                    // Second Triangle
                    _triangles.Add(_meshCoreDimension.x * (_meshCoreDimension.y - 1));
                    _triangles.Add(_topOffset);
                    _triangles.Add(_topOffset + 1);
                    // Third Triangle
                    _triangles.Add(_meshCoreDimension.x * (_meshCoreDimension.y - 1));
                    _triangles.Add(_topOffset + 1);
                    _triangles.Add(_meshCoreDimension.x * (_meshCoreDimension.y - 1) + 1);

                    int _length = _cellDimension.x / 2 - (_transition.Right ? 2 : 1);
                    for (int _x = 0; _x < _length; _x++)
                    {
                        // First Triangle
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x + 2 + _x * 2);
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x + 1 + _x * 2);
                        _triangles.Add(_topOffset + 1 + _x);
                        // Second Triangle
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x + 2 + _x * 2);
                        _triangles.Add(_topOffset + 1 + _x);
                        _triangles.Add(_topOffset + 2 + _x);
                        // Third Triangle
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x + 2 + _x * 2);
                        _triangles.Add(_topOffset + 2 + _x);
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x + 3 + _x * 2);
                    }
                }
                // Last Cell
                if (_transition.Right)
                {
                    // First Triangle
                    _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - 1);
                    _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - 2);
                    _triangles.Add(_topOffset + _cellDimension.x / 2 - 1);
                    // Second Triangle
                    _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - 1);
                    _triangles.Add(_topOffset + _cellDimension.x / 2 - 1);
                    _triangles.Add(_topOffset + _cellDimension.x / 2);
                }
            }
            #endregion
            // Right Transition Mesh Triangles
            #region Right Transition Mesh Triangles
            if (_transition.Right)
            {
                if (!_transition.Forward)
                {
                    int _length = _cellDimension.y / 2 - (_transition.Back ? 1 : 0);
                    for (int _y = 0; _y < _length; _y++)
                    {
                        // First Triangle
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x - 1 - _meshCoreDimension.x * _y * 2);
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - 1 - _meshCoreDimension.x * _y * 2);
                        _triangles.Add(_rightOffset + _y);
                        // Second Triangle
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x - 1 - _meshCoreDimension.x * _y * 2);
                        _triangles.Add(_rightOffset + _y);
                        _triangles.Add(_rightOffset + _y + 1);
                        // Third Triangle
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x - 1 - _meshCoreDimension.x * _y * 2);
                        _triangles.Add(_rightOffset + _y + 1);
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x * 2 - 1 - _meshCoreDimension.x * _y * 2);
                    }
                }
                else
                {
                    // First Cell
                    // Skip First Triangle
                    // Second Triangle
                    _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - 1);
                    _triangles.Add(_rightOffset);
                    _triangles.Add(_rightOffset + 1);
                    // Third Triangle
                    _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - 1);
                    _triangles.Add(_rightOffset + 1);
                    _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x - 1);

                    int _length = _cellDimension.y / 2 - (_transition.Back ? 2 : 1);
                    for (int _y = 0; _y < _length; _y++)
                    {
                        // First Triangle
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x * 2 - 1 - _meshCoreDimension.x * _y * 2);
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x - 1 - _meshCoreDimension.x * _y * 2);
                        _triangles.Add(_rightOffset + _y + 1);
                        // Second Triangle
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x * 2 - 1 - _meshCoreDimension.x * _y * 2);
                        _triangles.Add(_rightOffset + _y + 1);
                        _triangles.Add(_rightOffset + _y + 2);
                        // Third Triangle
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x * 2 - 1 - _meshCoreDimension.x * _y * 2);
                        _triangles.Add(_rightOffset + _y + 2);
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x * 3 - 1 - _meshCoreDimension.x * _y * 2);
                    }
                }
                // Last Cell
                if (_transition.Back)
                {
                    // First Triangle
                    _triangles.Add(_meshCoreDimension.x - 1);
                    _triangles.Add(_meshCoreDimension.x * 2 - 1);
                    _triangles.Add(_rightOffset + _cellDimension.y / 2 - 1);
                    // Second Triangle
                    _triangles.Add(_meshCoreDimension.x - 1);
                    _triangles.Add(_rightOffset + _cellDimension.y / 2 - 1);
                    _triangles.Add(_rightOffset + _cellDimension.y / 2);
                }
            }
            #endregion
            // Bottom Transition Mesh Triangles
            #region Bottom Transition Mesh Triangles
            if (_transition.Back)
            {
                if (!_transition.Right)
                {
                    int _length = _cellDimension.x / 2 - (_transition.Left ? 1 : 0);
                    for (int _x = 0; _x < _length; _x++)
                    {
                        // First Triangle
                        _triangles.Add(_meshCoreDimension.x - 2 - _x * 2);
                        _triangles.Add(_meshCoreDimension.x - 1 - _x * 2);
                        _triangles.Add(_bottomOffset + _x);
                        // Second Triangle
                        _triangles.Add(_meshCoreDimension.x - 2 - _x * 2);
                        _triangles.Add(_bottomOffset + _x);
                        _triangles.Add(_bottomOffset + 1 + _x);
                        // Third Triangle
                        _triangles.Add(_meshCoreDimension.x - 2 - _x * 2);
                        _triangles.Add(_bottomOffset + 1 + _x);
                        _triangles.Add(_meshCoreDimension.x - 3 - _x * 2);
                    }
                }
                else
                {
                    // Skip First Triangle
                    // Second Triangle
                    _triangles.Add(_meshCoreDimension.x - 1);
                    _triangles.Add(_bottomOffset);
                    _triangles.Add(_bottomOffset + 1);
                    // Third Triangle
                    _triangles.Add(_meshCoreDimension.x - 1);
                    _triangles.Add(_bottomOffset + 1);
                    _triangles.Add(_meshCoreDimension.x - 2);

                    int _length = _cellDimension.x / 2 - (_transition.Left ? 2 : 1);
                    for (int _x = 0; _x < _length; _x++)
                    {
                        // First Triangle
                        _triangles.Add(_meshCoreDimension.x - 3 - (_x * 2));
                        _triangles.Add(_meshCoreDimension.x - 2 - _x * 2);
                        _triangles.Add(_bottomOffset + 1 + _x);
                        // Second Triangle
                        _triangles.Add(_meshCoreDimension.x - 3 - (_x * 2));
                        _triangles.Add(_bottomOffset + 1 + _x);
                        _triangles.Add(_bottomOffset + 2 + _x);
                        // Third Triangle
                        _triangles.Add(_meshCoreDimension.x - 3 - (_x * 2));
                        _triangles.Add(_bottomOffset + 2 + _x);
                        _triangles.Add(_meshCoreDimension.x - 4 - (_x * 2));
                    }
                }
                // Last Cell
                if (_transition.Left)
                {
                    // First Triangle
                    _triangles.Add(0);
                    _triangles.Add(1);
                    _triangles.Add(_bottomOffset + _cellDimension.x / 2 - 1);
                    // Second Triangles
                    _triangles.Add(0);
                    _triangles.Add(_bottomOffset + _cellDimension.x / 2 - 1);
                    _triangles.Add(_leftOffset);
                }
            }
            #endregion

            // Mesh
            Mesh _mesh = new Mesh();
            _mesh.vertices = _vertices;
            _mesh.triangles = _triangles.ToArray();
            _mesh.RecalculateNormals();

            //_meshFilter.mesh = _mesh;
            //_meshCollider.sharedMesh = _mesh;
            return _mesh;
        }
        private Mesh GenerateMesh(ref Texture2D texture2D, Vector3 minPos, Vector3 patchSize, float minSurfaceRange, float maxSurfaceRange, Vector2Int resolutionOffset, Vector2Int resolutionDimension, Vector2Int lod, TransitionCellData _transition)
        {
            // Level Of Detail
            lod.x = (int)Mathf.Pow(2, lod.x); // LOD Step
            lod.y = (int)Mathf.Pow(2, lod.y); // LOD Step

            // Vertex Dimension
            Vector2Int _vertexDimension = Vector2Int.zero;
            _vertexDimension.x = Mathf.CeilToInt((float)(resolutionDimension.x + 1) / lod.x);
            _vertexDimension.y = Mathf.CeilToInt((float)(resolutionDimension.y + 1) / lod.y);

            // Cell Dimension
            Vector2Int _cellDimension = _vertexDimension - Vector2Int.one;

            Vector2 _uvStep = Vector2.zero;
            _uvStep.x = 1f / _cellDimension.x;
            _uvStep.y = 1f / _cellDimension.y;

            // Calculate Vertex Length
            Vector2Int _meshCoreDimension = _vertexDimension;
            _meshCoreDimension.x -= _transition.Left ? 1 : 0;
            _meshCoreDimension.x -= _transition.Right ? 1 : 0;
            _meshCoreDimension.y -= _transition.Forward ? 1 : 0;
            _meshCoreDimension.y -= _transition.Back ? 1 : 0;
            Vector2Int _transitionDimension = new Vector2Int(
                _cellDimension.x / 2,
                _cellDimension.y / 2
            );
            int _verticesLength = _meshCoreDimension.x * _meshCoreDimension.y; // Mesh Core            
            if (_transition.Left)
            {
                _verticesLength += _transitionDimension.y;
                if (!_transition.Forward)
                {
                    _verticesLength += 1;
                }
            }
            if (_transition.Forward)
            {
                _verticesLength += _transitionDimension.x;
                if (!_transition.Right)
                {
                    _verticesLength += 1;
                }
            }
            if (_transition.Right)
            {
                _verticesLength += _transitionDimension.y;
                if (!_transition.Back)
                {
                    _verticesLength += 1;
                }
            }
            if (_transition.Back)
            {
                _verticesLength += _transitionDimension.x;
                if (!_transition.Left)
                {
                    _verticesLength += 1;
                }
            }

            Vector2Int _pixelOffset = new Vector2Int(
                _transition.Left ? 1 * lod.x : 0,
                _transition.Back ? 1 * lod.y : 0
            );

            float _xOffset = _transition.Left ? _uvStep.x * patchSize.x : 0;
            float _yOffset = _transition.Back ? _uvStep.y * patchSize.z : 0;

            // Mesh Core Vertices
            #region Mesh Core Vertices
            int _v = 0;
            Vector3[] _vertices = new Vector3[_verticesLength];
            for (int _z = 0; _z < _meshCoreDimension.y; _z++)
            {
                for (int _x = 0; _x < _meshCoreDimension.x; _x++)
                {
                    // Add Vertex
                    _vertices[_v] = minPos;
                    _vertices[_v].x += _xOffset + _x * _uvStep.x * patchSize.x;    // Vertex X-Position
                    _vertices[_v].y += minSurfaceRange + texture2D.GetPixel(       // Vertex Y-Position
                            resolutionOffset.x + _pixelOffset.x + _x * lod.x,       // Texture2D X-Coordinate
                            resolutionOffset.y + _pixelOffset.y + _z * lod.y        // Texture2D Y-Coordinate
                        ).r * maxSurfaceRange;
                    _vertices[_v].z += _yOffset + _z * _uvStep.y * patchSize.z;    // Vertex Z-Position
                    _v++;
                }
            }
            #endregion
            // Left Transition Mesh Vertices
            #region Left Transition Mesh Vertices
            int _leftOffset = _v;
            if (_transition.Left)
            {
                // Add Left Transition Cell Vertex
                for (int _z = 0; _z < _cellDimension.y - 1; _z += 2)
                {
                    _vertices[_v] = minPos;
                    //_vertices[_v].x += 0f;                                    // Vertex X-Position
                    _vertices[_v].y += minSurfaceRange + texture2D.GetPixel(    // Y-Position
                            resolutionOffset.x,                                 // X-Coordinate
                            resolutionOffset.y + _z * lod.y                     // Y-Coordinate
                        ).r * maxSurfaceRange;
                    _vertices[_v].z += _z * _uvStep.y * patchSize.z;            // Z-Position
                    _v++;
                }
                // Add Last Left Transition Cell Vertex
                if (!_transition.Forward)
                {
                    _vertices[_v] = minPos;
                    //_vertices[_v].x += 0f;                                    // Vertex X-Position
                    _vertices[_v].y += minSurfaceRange + texture2D.GetPixel(    // Vertex Y-Position
                            resolutionOffset.x,                                 // Texture2D X-Coordinate
                            resolutionOffset.y + resolutionDimension.y          // Texture2D Y-Coordinate
                        ).r * maxSurfaceRange;
                    _vertices[_v].z += patchSize.z;                             // Vertex Z-Position
                    _v++;
                }
            }
            #endregion
            // Forward Transition Mesh Vertices
            #region Forward Transition Mesh Vertices
            int _topOffset = _v;
            if (_transition.Forward)
            {
                // Add Forward Transition Cell Vertex
                for (int _x = 0; _x < _cellDimension.x - 1; _x += 2)
                {
                    _vertices[_v] = minPos;
                    _vertices[_v].x += _x * _uvStep.x * patchSize.x;            // Vertex X-Position
                    _vertices[_v].y += minSurfaceRange + texture2D.GetPixel(    // Vertex Y-Position
                        resolutionOffset.x + _x * lod.x,                        // Texture2D X-Coordinate
                        resolutionOffset.y + resolutionDimension.y          // Texture2D Y-Coordinate
                    ).r * maxSurfaceRange;
                    _vertices[_v].z += patchSize.z;                             // Vertex Z-Position
                    _v++;
                }
                // Add Last Transition Cell Vertex
                if (!_transition.Right)
                {
                    _vertices[_v] = minPos;
                    _vertices[_v].x += patchSize.x;                             // Vertex X-Position
                    _vertices[_v].y += minSurfaceRange + texture2D.GetPixel(    // Vertex Y-Position
                        resolutionOffset.x + resolutionDimension.x,         // Texture2D X-Coordinate
                        resolutionOffset.y + resolutionDimension.y          // Texture2D Y-Coordinate
                    ).r * maxSurfaceRange;
                    _vertices[_v].z += patchSize.z;                             // Vertex Z-Position
                    _v++;
                }
            }
            #endregion
            // Right Transition Mesh Vertices
            #region Right Transition Mesh Vertices
            int _rightOffset = _v;
            if (_transition.Right)
            {
                // Add Right Transition Cell Vertex
                for (int _y = 0; _y < _cellDimension.y - 1; _y += 2)
                {
                    _vertices[_v] = minPos;
                    _vertices[_v].x += patchSize.x;                                        // Vertex X-Position
                    _vertices[_v].y += minSurfaceRange + texture2D.GetPixel(               // Vertex Y-Position
                            resolutionOffset.x + resolutionDimension.x,
                            resolutionOffset.y + resolutionDimension.y - _y * lod.y
                        ).r * maxSurfaceRange;
                    _vertices[_v].z += patchSize.z - _y * _uvStep.y * patchSize.z;        // Vertex Z-Position
                    _v++;
                }
                // Add Last Right Transition Cell Vertex
                if (!_transition.Back)
                {
                    _vertices[_v] = minPos;
                    _vertices[_v].x += patchSize.x;                                        // Vertex X-Position
                    _vertices[_v].y += minSurfaceRange + texture2D.GetPixel(               // Vertex Y-Position
                            resolutionOffset.x + resolutionDimension.x,
                            resolutionOffset.y
                        ).r * maxSurfaceRange;
                    // _vertices[_v].z += 0f;                                               // Vertex Z-Position
                    _v++;
                }
            }
            #endregion
            // Back Transition Mesh Vertices
            #region Back Transition Mesh Vertices
            int _bottomOffset = _v;
            if (_transition.Back)
            {
                // Add Back Transition Cell Vertex
                for (int _x = 0; _x < _cellDimension.x - 1; _x += 2)
                {
                    _vertices[_v] = minPos;
                    _vertices[_v].x += patchSize.x - _x * _uvStep.x * patchSize.x;    // Vertex X-Position
                    _vertices[_v].y += minSurfaceRange + texture2D.GetPixel(
                            resolutionOffset.x + resolutionDimension.x - _x * lod.x,
                            resolutionOffset.y
                        ).r * maxSurfaceRange;
                    // _vertices[_v].z += 0f;                                               // Vertex Z-Position
                    _v++;
                }
                // Add Last Back Transition Cell Vertex
                if (!_transition.Left)
                {
                    _vertices[_v] = minPos;
                    // _vertices[_v].x += 0f;                                               // Vertex X-Position 
                    _vertices[_v].y += minSurfaceRange + texture2D.GetPixel(               // Vertex Y-Position
                            resolutionOffset.x,
                            resolutionOffset.y
                        ).r * maxSurfaceRange;
                    // _vertices[_v].z += 0f;                                               // Vertex Z-Position
                    _v++;
                }
            }
            #endregion

            // Build Triangles
            #region Mesh Core Triangles
            int _t = 0;
            List<int> _triangles = new List<int>();
            for (int _y = 0; _y < _meshCoreDimension.y - 1; _y++)
            {
                for (int _x = 0; _x < _meshCoreDimension.x - 1; _x++)
                {
                    // First Triangle
                    _triangles.Add(_x + _meshCoreDimension.x * _y);                             // Bottom Left Vertex
                    _triangles.Add(_x + _meshCoreDimension.x * _y + _meshCoreDimension.x);      // Top Left Vertex
                    _triangles.Add(_x + _meshCoreDimension.x * _y + 1);                         // Bottom Right Vertex

                    // Second Triangle
                    _triangles.Add(_x + _meshCoreDimension.x * _y + _meshCoreDimension.x + 1);  // Top Right Vertex
                    _triangles.Add(_x + _meshCoreDimension.x * _y + 1);                         // Bottom Right Vertex
                    _triangles.Add(_x + _meshCoreDimension.x * _y + _meshCoreDimension.x);      // Top Left Vertex

                    _t += 6;
                }
            }
            #endregion
            // Left Transition Mesh Triangles
            #region Left Transition Mesh Triangles
            if (_transition.Left)
            {
                // Cells
                if (!_transition.Back)
                {
                    // If Mesh has no Top Transition build full row, else leave last cell free
                    int _length = _cellDimension.y / 2 - (_transition.Forward ? 1 : 0);
                    for (int _y = 0; _y < _length; _y++)
                    {
                        // First Triangle
                        _triangles.Add(_meshCoreDimension.x + _meshCoreDimension.x * _y * 2);
                        _triangles.Add(_meshCoreDimension.x * _y * 2);
                        _triangles.Add(_leftOffset + _y);
                        // Second Triangle
                        _triangles.Add(_meshCoreDimension.x + _meshCoreDimension.x * _y * 2);
                        _triangles.Add(_leftOffset + _y);
                        _triangles.Add(_leftOffset + _y + 1);
                        // Third Triangle
                        _triangles.Add(_meshCoreDimension.x + _meshCoreDimension.x * _y * 2);
                        _triangles.Add(_leftOffset + _y + 1);
                        _triangles.Add(_meshCoreDimension.x * 2 + _meshCoreDimension.x * _y * 2);
                    }
                }
                else
                {
                    // Skip First Triangle
                    // Second Triangle
                    _triangles.Add(0);
                    _triangles.Add(_leftOffset);
                    _triangles.Add(_leftOffset + 1);
                    // Third Triangle
                    _triangles.Add(0);
                    _triangles.Add(_leftOffset + 1);
                    _triangles.Add(_meshCoreDimension.x);

                    // Fill Cells
                    int _length = _cellDimension.y / 2 - (_transition.Forward ? 2 : 1);
                    for (int _y = 0; _y < _length; _y++)
                    {
                        // First Triangle                       
                        _triangles.Add(_meshCoreDimension.x * 2 + _meshCoreDimension.x * 2 * _y);
                        _triangles.Add(_meshCoreDimension.x + _meshCoreDimension.x * 2 * _y);
                        _triangles.Add(_leftOffset + _y + 1);
                        // Second Triangle
                        _triangles.Add(_meshCoreDimension.x * 2 + _meshCoreDimension.x * 2 * _y);
                        _triangles.Add(_leftOffset + _y + 1);
                        _triangles.Add(_leftOffset + _y + 2);
                        // Third Triangle
                        _triangles.Add(_meshCoreDimension.x * 2 + _meshCoreDimension.x * 2 * _y);
                        _triangles.Add(_leftOffset + _y + 2);
                        _triangles.Add(_meshCoreDimension.x * 3 + _meshCoreDimension.x * 2 * _y);
                    }
                }
                // Last Cell
                if (_transition.Forward)
                {
                    // First Triangle
                    _triangles.Add(_meshCoreDimension.x * (_meshCoreDimension.y - 1));
                    _triangles.Add(_meshCoreDimension.x * (_meshCoreDimension.y - 2));
                    _triangles.Add(_leftOffset + _cellDimension.y / 2 - 1);
                    // Second Triangle
                    _triangles.Add(_meshCoreDimension.x * (_meshCoreDimension.y - 1));
                    _triangles.Add(_leftOffset + _cellDimension.y / 2 - 1);
                    _triangles.Add(_leftOffset + _cellDimension.y / 2);
                }
            }
            #endregion
            // Forward Transition Mesh Triangles
            #region  Forward Transition Mesh Triangles
            if (_transition.Forward)
            {
                if (!_transition.Left)
                {
                    int _length = _cellDimension.x / 2 - (_transition.Right ? 1 : 0);
                    for (int _x = 0; _x < _length; _x++)
                    {
                        // First Triangle
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x + 1 + _x * 2);
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x + _x * 2);
                        _triangles.Add(_topOffset + _x);
                        // Second Triangle
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x + 1 + _x * 2);
                        _triangles.Add(_topOffset + _x);
                        _triangles.Add(_topOffset + _x + 1);
                        // Third Triangle
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x + 1 + _x * 2);
                        _triangles.Add(_topOffset + _x + 1);
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x + 2 + _x * 2);
                    }
                }
                else
                {
                    // First Cell
                    // Second Triangle
                    _triangles.Add(_meshCoreDimension.x * (_meshCoreDimension.y - 1));
                    _triangles.Add(_topOffset);
                    _triangles.Add(_topOffset + 1);
                    // Third Triangle
                    _triangles.Add(_meshCoreDimension.x * (_meshCoreDimension.y - 1));
                    _triangles.Add(_topOffset + 1);
                    _triangles.Add(_meshCoreDimension.x * (_meshCoreDimension.y - 1) + 1);

                    int _length = _cellDimension.x / 2 - (_transition.Right ? 2 : 1);
                    for (int _x = 0; _x < _length; _x++)
                    {
                        // First Triangle
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x + 2 + _x * 2);
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x + 1 + _x * 2);
                        _triangles.Add(_topOffset + 1 + _x);
                        // Second Triangle
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x + 2 + _x * 2);
                        _triangles.Add(_topOffset + 1 + _x);
                        _triangles.Add(_topOffset + 2 + _x);
                        // Third Triangle
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x + 2 + _x * 2);
                        _triangles.Add(_topOffset + 2 + _x);
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x + 3 + _x * 2);
                    }
                }
                // Last Cell
                if (_transition.Right)
                {
                    // First Triangle
                    _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - 1);
                    _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - 2);
                    _triangles.Add(_topOffset + _cellDimension.x / 2 - 1);
                    // Second Triangle
                    _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - 1);
                    _triangles.Add(_topOffset + _cellDimension.x / 2 - 1);
                    _triangles.Add(_topOffset + _cellDimension.x / 2);
                }
            }
            #endregion
            // Right Transition Mesh Triangles
            #region Right Transition Mesh Triangles
            if (_transition.Right)
            {
                if (!_transition.Forward)
                {
                    int _length = _cellDimension.y / 2 - (_transition.Back ? 1 : 0);
                    for (int _y = 0; _y < _length; _y++)
                    {
                        // First Triangle
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x - 1 - _meshCoreDimension.x * _y * 2);
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - 1 - _meshCoreDimension.x * _y * 2);
                        _triangles.Add(_rightOffset + _y);
                        // Second Triangle
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x - 1 - _meshCoreDimension.x * _y * 2);
                        _triangles.Add(_rightOffset + _y);
                        _triangles.Add(_rightOffset + _y + 1);
                        // Third Triangle
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x - 1 - _meshCoreDimension.x * _y * 2);
                        _triangles.Add(_rightOffset + _y + 1);
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x * 2 - 1 - _meshCoreDimension.x * _y * 2);
                    }
                }
                else
                {
                    // First Cell
                    // Skip First Triangle
                    // Second Triangle
                    _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - 1);
                    _triangles.Add(_rightOffset);
                    _triangles.Add(_rightOffset + 1);
                    // Third Triangle
                    _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - 1);
                    _triangles.Add(_rightOffset + 1);
                    _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x - 1);

                    int _length = _cellDimension.y / 2 - (_transition.Back ? 2 : 1);
                    for (int _y = 0; _y < _length; _y++)
                    {
                        // First Triangle
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x * 2 - 1 - _meshCoreDimension.x * _y * 2);
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x - 1 - _meshCoreDimension.x * _y * 2);
                        _triangles.Add(_rightOffset + _y + 1);
                        // Second Triangle
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x * 2 - 1 - _meshCoreDimension.x * _y * 2);
                        _triangles.Add(_rightOffset + _y + 1);
                        _triangles.Add(_rightOffset + _y + 2);
                        // Third Triangle
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x * 2 - 1 - _meshCoreDimension.x * _y * 2);
                        _triangles.Add(_rightOffset + _y + 2);
                        _triangles.Add(_meshCoreDimension.x * _meshCoreDimension.y - _meshCoreDimension.x * 3 - 1 - _meshCoreDimension.x * _y * 2);
                    }
                }
                // Last Cell
                if (_transition.Back)
                {
                    // First Triangle
                    _triangles.Add(_meshCoreDimension.x - 1);
                    _triangles.Add(_meshCoreDimension.x * 2 - 1);
                    _triangles.Add(_rightOffset + _cellDimension.y / 2 - 1);
                    // Second Triangle
                    _triangles.Add(_meshCoreDimension.x - 1);
                    _triangles.Add(_rightOffset + _cellDimension.y / 2 - 1);
                    _triangles.Add(_rightOffset + _cellDimension.y / 2);
                }
            }
            #endregion
            // Bottom Transition Mesh Triangles
            #region Bottom Transition Mesh Triangles
            if (_transition.Back)
            {
                if (!_transition.Right)
                {
                    int _length = _cellDimension.x / 2 - (_transition.Left ? 1 : 0);
                    for (int _x = 0; _x < _length; _x++)
                    {
                        // First Triangle
                        _triangles.Add(_meshCoreDimension.x - 2 - _x * 2);
                        _triangles.Add(_meshCoreDimension.x - 1 - _x * 2);
                        _triangles.Add(_bottomOffset + _x);
                        // Second Triangle
                        _triangles.Add(_meshCoreDimension.x - 2 - _x * 2);
                        _triangles.Add(_bottomOffset + _x);
                        _triangles.Add(_bottomOffset + 1 + _x);
                        // Third Triangle
                        _triangles.Add(_meshCoreDimension.x - 2 - _x * 2);
                        _triangles.Add(_bottomOffset + 1 + _x);
                        _triangles.Add(_meshCoreDimension.x - 3 - _x * 2);
                    }
                }
                else
                {
                    // Skip First Triangle
                    // Second Triangle
                    _triangles.Add(_meshCoreDimension.x - 1);
                    _triangles.Add(_bottomOffset);
                    _triangles.Add(_bottomOffset + 1);
                    // Third Triangle
                    _triangles.Add(_meshCoreDimension.x - 1);
                    _triangles.Add(_bottomOffset + 1);
                    _triangles.Add(_meshCoreDimension.x - 2);

                    int _length = _cellDimension.x / 2 - (_transition.Left ? 2 : 1);
                    for (int _x = 0; _x < _length; _x++)
                    {
                        // First Triangle
                        _triangles.Add(_meshCoreDimension.x - 3 - (_x * 2));
                        _triangles.Add(_meshCoreDimension.x - 2 - _x * 2);
                        _triangles.Add(_bottomOffset + 1 + _x);
                        // Second Triangle
                        _triangles.Add(_meshCoreDimension.x - 3 - (_x * 2));
                        _triangles.Add(_bottomOffset + 1 + _x);
                        _triangles.Add(_bottomOffset + 2 + _x);
                        // Third Triangle
                        _triangles.Add(_meshCoreDimension.x - 3 - (_x * 2));
                        _triangles.Add(_bottomOffset + 2 + _x);
                        _triangles.Add(_meshCoreDimension.x - 4 - (_x * 2));
                    }
                }
                // Last Cell
                if (_transition.Left)
                {
                    // First Triangle
                    _triangles.Add(0);
                    _triangles.Add(1);
                    _triangles.Add(_bottomOffset + _cellDimension.x / 2 - 1);
                    // Second Triangles
                    _triangles.Add(0);
                    _triangles.Add(_bottomOffset + _cellDimension.x / 2 - 1);
                    _triangles.Add(_leftOffset);
                }
            }
            #endregion

            // Mesh
            Mesh _mesh = new Mesh();
            _mesh.vertices = _vertices;
            _mesh.triangles = _triangles.ToArray();
            _mesh.RecalculateNormals();

            //_meshFilter.mesh = _mesh;
            //_meshCollider.sharedMesh = _mesh;
            return _mesh;
        }
        */
        #endregion

#if (UNITY_EDITOR)
        // UnityEditor MonoBehaviors
        #region UnityEditor MonoBehaviors
        // OnGUI
        #region OnGUI
        private void OnGUI()
        {
            
        }
        #endregion
        // OnDrawGizmos
        #region OnDrawGizmos
        private void OnDrawGizmos()
        {

        }
        #endregion
        #endregion
        private void UnityEditor_EditorApplication_Update()
        {
            if (this.terrain != null)
            {
                this.terrain.UnityEditor_EditorApplication_Update();
            }
        }
#endif
    }
}
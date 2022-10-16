using UnityEngine;
using System.Collections.Generic;

namespace Elapsed.Terrain.PatchStream.Component
{
    public class Terrain
    {
        #region Variables
        #region Terrain Data
        private TerrainData terrainData;
        public TerrainData Data
        {
            get { return this.terrainData; }
            set { this.SetTerrainData(value); }
        }
        public void SetTerrainData(TerrainData terrainData)
        {
            this.terrainData = terrainData;
        }
        #endregion
        #region Terrain Component
        private TerrainComponent component;
        public TerrainComponent Component
        {
            get { return this.component; }
            set { this.component = value; }
        }
        #endregion
        #region Terrain GameObject
        public GameObject gameObject
        {
            get { return this.component.gameObject; }
        }
        #endregion

        private Vector2Int lastIndex;
        private int minLod = 0, maxLod = 4;
        #endregion

        #region Constructors
        public Terrain(TerrainData terrainData)
        {
            this.terrainData = terrainData;
            this.component = new GameObject(terrainData.Name).AddComponent<TerrainComponent>();
            this.component.terrain = this;


        }
        #endregion

        #region Methods
        #region GetPatchIndexAtPosition
        /// <summary>
        /// Fin Patch Index from position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector2Int GetPatchIndexAtPosition(Vector3 position)
        {
            Vector3 localPosition = component.gameObject.transform.InverseTransformPoint(position);
            return new Vector2Int(
                Mathf.FloorToInt(localPosition.x / this.terrainData.PatchSize.x),
                Mathf.FloorToInt(localPosition.z / this.terrainData.PatchSize.z)
            );
        }
        #endregion

        public void GenerateFromPosition(Vector3 position)
        {
            Vector2Int rootIndex = this.GetPatchIndexAtPosition(position);
            lastIndex = rootIndex;
            Vector2Int resolutionOffset = rootIndex * this.terrainData.PatchResolution;
            Vector2Int resolution = this.terrainData.PatchResolution;

            Patch patch = new Patch(
                this,
                resolutionOffset,
                resolution
            );
            
            patch.meshFilter.mesh = this.GenerateMesh(
                this.terrainData.HeightMap,
                -new Vector3(
                    this.terrainData.PatchSize.x * 0.5f,
                    this.terrainData.PatchSize.y * 0.5f,
                    this.terrainData.PatchSize.z * 0.5f
                ),
                this.terrainData.PatchSize,
                this.terrainData.SurfaceMin,
                this.terrainData.SurfaceMax,
                resolutionOffset,
                resolution,
                new Vector2Int(minLod, minLod),
                new TransitionCellData(true)
            );

            int r = 1;
            for (int lod = 1; lod < 4; lod++)
            {
                for (int i = 0; i < 8; i++)                                                                                     // Each Patch has 8 Neighbours
                {
                    Vector2Int thisOffset = resolutionOffset + Patch.res[i] * resolution * r;
                    patch = new Patch(this, thisOffset, resolution * r);
                    patch.meshFilter.mesh = this.GenerateMesh(
                        this.terrainData.HeightMap,
                        -new Vector3(
                            this.terrainData.PatchSize.x * r * 0.5f,
                            this.terrainData.PatchSize.y * 0.5f,
                            this.terrainData.PatchSize.z * r * 0.5f
                        ),
                        this.terrainData.PatchSize * r,
                        this.terrainData.SurfaceMin,
                        this.terrainData.SurfaceMax,
                        thisOffset,
                        resolution * r,
                        new Vector2Int(lod, lod),
                        TransitionCellData.patch[i]
                        );
                }
                resolutionOffset += Patch.res[5] * resolution * r;
                r *= 3;
            }
            
        }


        private Mesh GenerateMesh(Texture2D texture2D, Vector3 minPos, Vector3 patchSize, float minSurfaceRange, float maxSurfaceRange, Vector2Int resolutionOffset, Vector2Int resolutionDimension, Vector2Int lod, TransitionCellData _transition)
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

            return _mesh;
        }
        public void Clear()
        {
            while (component.transform.childCount != 0)
            {
                GameObject.DestroyImmediate(component.transform.GetChild(0).gameObject);
            }
        }

#if (UNITY_EDITOR)
        #region UnityEditor MonoBehaviors
        #region OnGUI
        private void OnGUI()
        {

        }
        #endregion
        #region OnDrawGizmos
        private void OnDrawGizmos()
        {

        }
        #endregion
        #endregion
        #region EditorApplication_Update
        /// <summary>
        /// 
        /// </summary>
        public void UnityEditor_EditorApplication_Update()
        {
            if (Application.isEditor && !Application.isPlaying)
            {
                Vector2Int index = this.GetPatchIndexAtPosition(
                    UnityEditor.SceneView.lastActiveSceneView.camera.transform.position
                );
                if (lastIndex != index)
                {
                    this.Clear();
                    this.GenerateFromPosition(UnityEditor.SceneView.lastActiveSceneView.camera.transform.position);
                }
            }
        }
        #endregion
#endif
        #endregion
    }
}
using UnityEngine;


namespace Elapsed.Terrain.PatchStream.Component
{
    public class Patch
    {
        // Statics
        #region Statics
        public static readonly Vector2Int[] index = new Vector2Int[8]
        {
            new Vector2Int(-1, -1),  // 0 - Forward Left
            new Vector2Int(-1,  0),  // 1 - Forward
            new Vector2Int(-1,  1),  // 2 - Forward Right
            new Vector2Int( 0, -1),  // 3 - Left
            new Vector2Int( 0,  1),  // 4 - Right
            new Vector2Int( 1, -1),  // 5 - Back Left
            new Vector2Int( 1,  0),  // 6 - Back
            new Vector2Int( 1,  1)   // 7 - Back Right
        };
        public static readonly Vector2Int[] res = new Vector2Int[8]
        {
            new Vector2Int(-1,  1),  // 0 - Forward Left
            new Vector2Int( 0,  1),  // 1 - Forward
            new Vector2Int( 1,  1),  // 2 - Forward Right
            new Vector2Int(-1,  0),  // 3 - Left
            new Vector2Int( 1,  0),  // 4 - Right
            new Vector2Int(-1, -1),  // 5 - Back Left
            new Vector2Int( 0, -1),  // 6 - Back
            new Vector2Int( 1, -1)   // 7 - Back Right
        };
        #endregion

        // Variables
        #region Variables
        // GameObject
        #region GameObject
        private GameObject gameObject;
        public GameObject GameObject
        {
            get { return gameObject; }
            set { gameObject = value; }
        }
        #endregion
        // MeshFilter
        #region MeshFilter
        private MeshFilter _meshFilter;
        public MeshFilter meshFilter
        {
            get { return _meshFilter; }
            set { _meshFilter = value; }
        }
        #endregion
        // MeshRenderer
        #region MeshRenderer
        private MeshRenderer _meshRenderer;
        public MeshRenderer meshRenderer
        {
            get { return _meshRenderer; }
            set { _meshRenderer = value; }
        }
        #endregion
        // MeshCollider
        #region MeshCollider
        private MeshCollider _meshCollider;
        public MeshCollider meshCollider
        {
            get { return _meshCollider; }
            set { _meshCollider = value; }
        }
        #endregion
        #endregion

        // Constructors
        #region Constructors
        // Default Constructor
        #region Default Constructor
        public Patch() { }
        #endregion
        // Standard Construtor
        #region Standard Construtor
        public Patch(Terrain terrain, Vector2Int resolutionOffset, Vector2Int resolutionSize)
        {
            // GameObject
            gameObject = new GameObject();
            gameObject.transform.SetParent(terrain.gameObject.transform);
            gameObject.transform.localPosition = new Vector3(
                ((float)resolutionOffset.x + resolutionSize.x * 0.5f) / terrain.Data.Resolution.x * terrain.Data.Size.x,
                terrain.Data.PatchSize.y * 0.5f,
                ((float)resolutionOffset.y + resolutionSize.y * 0.5f) / terrain.Data.Resolution.y * terrain.Data.Size.z
            );
            _meshFilter = gameObject.AddComponent<MeshFilter>();
            _meshRenderer = gameObject.AddComponent<MeshRenderer>();
        }
        #endregion
        #endregion

        public void Destroy()
        {

        }

        public void Generate()
        {

        }
    }
}
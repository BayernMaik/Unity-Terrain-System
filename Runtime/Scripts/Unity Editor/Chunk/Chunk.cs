using UnityEditor;
using UnityEngine;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TerrainSystem
{
    public class Chunk
    {
        // Variables
        private ChunkData _chunkData;
        private GameObject _gameObject;
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private MeshCollider _meshCollider;

        AssetReference _assetReference;
        AsyncOperationHandle _asyncOperationHandle;

        // Constructors
        public Chunk(ChunkData chunkData)
        {
            _gameObject = new GameObject();
            _gameObject.name = "Chunk" + chunkData.index.ToString();
            _gameObject.AddComponent<MeshFilter>();
            _gameObject.AddComponent<MeshRenderer>();
            _gameObject.AddComponent<MeshCollider>();

            _gameObject.transform.position = chunkData.localPosition;

            _meshFilter = _gameObject.GetComponent<MeshFilter>();
            _meshRenderer = _gameObject.GetComponent<MeshRenderer>();
            _meshCollider = _gameObject.GetComponent<MeshCollider>();

            // Material
            _assetReference = new AssetReference("Assets/Materials/Terrain/Material_Terrain_Holder.prefab");
            _asyncOperationHandle = Addressables.LoadAssetAsync<GameObject>(_assetReference);
            _asyncOperationHandle.Completed += (operation) =>
            {
                _assetReference.InstantiateAsync().Completed += (asyncOperationHandle) =>
                {
                    // Apply Holders Material to Chunks Mesh Renderer
                    _meshRenderer.material = asyncOperationHandle.Result.GetComponent<MeshRenderer>().material;
                    // Delete Material Holder
                    Addressables.ReleaseInstance(asyncOperationHandle.Result);
                    GameObject.Destroy(asyncOperationHandle.Result);
                    Addressables.Release(_asyncOperationHandle);
                };
            };

            if (chunkData.vertices.Length >= 3)
            {
                Mesh mesh = new Mesh();
                mesh.vertices = chunkData.vertices;
                mesh.triangles = chunkData.triangles;
                mesh.RecalculateNormals();

                _gameObject.GetComponent<MeshFilter>().mesh = mesh;
                _gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
            }
        }

        // Getter / Setter
        public GameObject gameObject { get { return _gameObject; } set { _gameObject = value; } }
        public ChunkData chunkData { get { return _chunkData; } set { _chunkData = value; } }

        // Methods
        public void Destroy()
        {
            GameObject.Destroy(gameObject);
        }
    }
}

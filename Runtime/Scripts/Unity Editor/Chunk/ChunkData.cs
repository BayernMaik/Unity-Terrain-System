
using UnityEditor;
using UnityEngine;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TerrainSystem
{
    public class ChunkData
    {
        // Variables
        private Vector3Int _index;

        private Vector3 _localPosition;

        private float[] _sampleData;

        private Vector3[] _vertices;
        private Vector3[] _verticesLOD0;
        private Vector3[] _verticesLOD1;

        private int[] _triangles;
        private int[] _trianglesLOD0;
        private int[] _trianglesLOD1;



        // Constructors

        // Default Constructor
        #region Default Constructor
        public ChunkData() { }
        #endregion
        public ChunkData(ChunkSaveData chunkSaveData)
        {
            _localPosition = chunkSaveData.localPosition;
            _sampleData = chunkSaveData.sampleData;
            _vertices = chunkSaveData.vertices;
            _triangles = chunkSaveData.trianlges;
            _index = chunkSaveData.index;
        }

        // Getter / Setter 
        public Vector3Int index { get { return _index; } set { _index = value; } }
        public Vector3 localPosition { get { return _localPosition; } set { _localPosition = value; } }
        public Vector3[] vertices { get { return _vertices; } set { _vertices = value; } }
        public float[] sampleData { get { return _sampleData; } set { _sampleData = value; } }
        public int[] triangles { get { return _triangles; } set { _triangles = value; } }
       
        
        // Methods
        public void Save(TerrainData terrainData)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            string directoryPath = Application.persistentDataPath + "/Terrain/" + terrainData.DisplayName + "/";
            string fileName = terrainData.DisplayName + "_" + _index.x + "_" + _index.y + "_" + _index.z + ".chunkdata";

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string savePath = directoryPath + fileName;

            FileStream fileStream = new FileStream(savePath, FileMode.OpenOrCreate);

            ChunkSaveData chunkSaveData = new ChunkSaveData(this);

            binaryFormatter.Serialize(fileStream, chunkSaveData);

            fileStream.Close();
        }
    
    
    }
}

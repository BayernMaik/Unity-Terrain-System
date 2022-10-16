using System;
using System.IO;
using UnityEngine;

namespace TerrainSystem
{
    [Serializable]
    public class OctreeData
    {
        // Variables
        [SerializeField] private int[] _index;
        [SerializeField] private MeshData[] octs;

        [SerializeField] private string _id;
        [SerializeField] private string[] _ids = new string[8];

        // Constructors
        public OctreeData(string id)
        {
            _id = id;
        }

        // Methods
        public string id { get { return _id; } }
        public string[] ids { get { return _ids; } }
        public void SaveJSON()
        {
            string jsonString = JsonUtility.ToJson(this);
            Debug.Log(jsonString);
            File.WriteAllText(Application.persistentDataPath + "write.json", jsonString);
        }
    }
}
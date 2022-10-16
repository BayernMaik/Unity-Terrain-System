using UnityEngine;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TerrainSystem
{
    public static class TerrainSystemUtility
    {
        public static string terrainDataPath = "/Terrain/";

        public static TerrainFileInfo[] LoadSavedTerrainAssetsAtPath(string path)
        {
            string directoryPath = Application.persistentDataPath + "/Terrain/";
            string[] directories = Directory.GetDirectories(directoryPath);
            TerrainFileInfo[] tf = new TerrainFileInfo[directories.Length];
            for (int i = 0; i < tf.Length; i++)
            {
                DirectoryInfo di = new DirectoryInfo(directories[i]);

                tf[i] = new TerrainFileInfo();
                tf[i].name = di.Name;
            }
            return tf;
        }

        public static void Save(TerrainDataAsset terrainData)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            string directoryPath = Application.persistentDataPath + "/Terrain/" + terrainData.name + "/";
            string fileName = terrainData.name + ".terraindata";

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string savePath = directoryPath + fileName;

            FileStream fileStream = new FileStream(savePath, FileMode.OpenOrCreate);

            TerrainSaveData terrainSaveData = new TerrainSaveData(terrainData);

            binaryFormatter.Serialize(fileStream, terrainSaveData);

            fileStream.Close();
        }
        public static ChunkData LoadChunk(TerrainData terrainData, Vector3Int chunkIndex)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            string directoryPath = Application.persistentDataPath + "/Terrain/" + terrainData.DisplayName + "/";
            string fileName = terrainData.DisplayName + "_" + chunkIndex.x + "_" + chunkIndex.y + "_" + chunkIndex.z + ".chunkdata";

            string loadPath = directoryPath + fileName;

            FileStream saveFile = File.Open(loadPath, FileMode.Open);

            ChunkSaveData chunkSaveData = (ChunkSaveData)binaryFormatter.Deserialize(saveFile);

            saveFile.Close();

            return new ChunkData(chunkSaveData);
        }
    }
    public class TerrainFileInfo
    {
        public string name;
        public Texture2D thumbnail;
        public string description;
    }
}
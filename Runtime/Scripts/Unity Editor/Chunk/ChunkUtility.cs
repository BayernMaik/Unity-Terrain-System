using UnityEngine;

namespace TerrainSystem
{
    public static class ChunkUtility
    {
        public static void SampleData(TerrainDataAsset terrainData, ChunkData chunkData, Texture2D texture2D)
        {
            chunkData.sampleData = new float[(terrainData.chunkSize.x + 1) * (terrainData.chunkSize.y + 1) * (terrainData.chunkSize.z + 1)];
            float terrainScope = (terrainData.surfaceRange.y - terrainData.surfaceRange.x);
            int i = 0;
            for (int x = 0; x <= terrainData.chunkSize.x; x++)
            {
                for (int y = 0; y <= terrainData.chunkSize.y; y++)
                {
                    for (int z = 0; z <= terrainData.chunkSize.z; z++)
                    {
                        float n = texture2D.GetPixel(x, z).r;
                        chunkData.sampleData[i] = (terrainData.surfaceRange.x + terrainScope * n) - (chunkData.localPosition.y + y);
                        i++;
                    }
                }
            }
        }


        public static void SampleData(TerrainDataAsset terrainData, ChunkData chunkData)
        {
            chunkData.sampleData = new float[(terrainData.chunkSize.x + 1) * (terrainData.chunkSize.y + 1) * (terrainData.chunkSize.z + 1)];
            float terrainScope = (terrainData.surfaceRange.y - terrainData.surfaceRange.x);
            float scale = 10f;
            int i = 0;
            for (int x = 0; x <= terrainData.chunkSize.x; x++)
            {
                for (int y = 0; y <= terrainData.chunkSize.y; y++)
                {
                    for (int z = 0; z <= terrainData.chunkSize.z; z++)
                    {
                        float xSample = (chunkData.index.x * terrainData.chunkSize.x + (float)x) / scale;
                        float ySample = (chunkData.index.z * terrainData.chunkSize.z + (float)z) / scale;
                        float noise2D = Mathf.PerlinNoise(xSample, ySample);

                        chunkData.sampleData[i] = (terrainData.surfaceRange.x + terrainScope * noise2D) - (chunkData.localPosition.y + y);
                        i++;
                    }
                }
            }
        }
        public static float[] SamplePerlinNoise(TerrainDataAsset terrainData, ChunkData chunkData)
        {
            float[] sampleData = new float[(terrainData.chunkSize.x + 1) * (terrainData.chunkSize.y + 1) * (terrainData.chunkSize.z + 1)];
            float terrainScope = (terrainData.surfaceRange.y - terrainData.surfaceRange.x);

            Vector3 scale = new Vector3(10f, 10f, 10f);
            int i = 0;
            for (int x = 0; x <= terrainData.chunkSize.x; x++)
            {
                for (int y = 0; y <= terrainData.chunkSize.y; y++)
                {
                    for (int z = 0; z <= terrainData.chunkSize.z; z++)
                    {
                        float xCoord = (chunkData.index.x * terrainData.chunkSize.x + (float)x) / 10f;
                        float yCoord = (chunkData.index.y * terrainData.chunkSize.y + (float)y) / 10f;
                        float zCoord = (chunkData.index.z * terrainData.chunkSize.z + (float)z) / 10f;
                        //sampleData[i] = Noise.PerlinNoise.PerlinNoise3D(xCoord, yCoord, zCoord);
                        i++;
                    }
                }
            }
            return sampleData;
        }
        public static float[] SampleDataSimplexNoise(TerrainDataAsset terrainData, ChunkData chunkData)
        {
            float[] sampleData = new float[(terrainData.chunkSize.x + 1) * (terrainData.chunkSize.y + 1) * (terrainData.chunkSize.z + 1)];
            int i = 0;
            for (int x = 0; x <= terrainData.chunkSize.x; x++)
            {
                for (int y = 0; y <= terrainData.chunkSize.y; y++)
                {
                    for (int z = 0; z <= terrainData.chunkSize.z; z++)
                    {
                        int xOffset = chunkData.index.x * terrainData.chunkSize.x + x;
                        int yOffset = chunkData.index.y * terrainData.chunkSize.y + y;
                        int zOffset = chunkData.index.z * terrainData.chunkSize.z + z;

                        //sampleData[i] = SimplexNoise.CalcPixel3D(xOffset, yOffset, zOffset, 0.05f) / 255.0f;
                        i++;
                    }
                }
            }
            return sampleData;
        }
    }
}
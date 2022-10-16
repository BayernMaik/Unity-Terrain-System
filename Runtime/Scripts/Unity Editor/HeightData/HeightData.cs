using System;
using UnityEngine;

namespace TerrainSystem
{
    [Serializable]
    public class HeightData
    {
        private string _name = "New Layer";
        private float[] octaves = new float[] { 0.0f };
        private Vector2 _seed = new Vector2(0, 0);
        private Vector2 _scale = new Vector2(1, 1);
        private byte[] _rawTextureData;
        public string name { get { return _name; } set { _name = value; } }
        public Vector2 seed { get { return _seed; } set { _seed = value; } }
        public Vector2 scale { get { return _scale; } set { _scale = value; } }
        public byte[] texture2DRaw { get { return _rawTextureData; } }
        
        public Texture2D texture2D { get { return LoadRawTexture2D(); } set { _rawTextureData = Texture2DToRaw(value); } }
       
        private Texture2D LoadRawTexture2D()
        {
            Texture2D _texture2D = new Texture2D(32, 32);
            if (_rawTextureData != null)
            {
                _texture2D.LoadRawTextureData(_rawTextureData);
                _texture2D.Apply();
            }
            return _texture2D;
        }
        private byte[] Texture2DToRaw(Texture2D texture2D)
        {
            return texture2D.GetRawTextureData();
        }
    }
}
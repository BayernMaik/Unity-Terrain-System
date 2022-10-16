using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elapsed.Utility
{
    public static class MaterialUtility
    {
        // Get Material Texture
        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="textureWidth"></param>
        /// <param name="textureHeight"></param>
        /// <returns></returns>
        public static Texture2D GetTexture(this Material material, int textureWidth, int textureHeight)
        {
            Texture2D texture2D = new Texture2D(
                textureWidth,
                textureHeight,
                TextureFormat.ARGB32,
                true
            );
            RenderTexture renderTexture = new RenderTexture(
                textureWidth,
                textureHeight,
                0,
                RenderTextureFormat.ARGB32,
                RenderTextureReadWrite.sRGB
            );
            Graphics.Blit(null, renderTexture, material, 0);
            RenderTexture.active = renderTexture;
            texture2D.ReadPixels(
                new Rect(0, 0, textureWidth, textureHeight),
                0,
                0,
                false
            );
            texture2D.Apply();
            return texture2D;
        }
        public static void GetTexture(this Material material, ref Texture2D texture2, ref RenderTexture renderTexture)
        {
        }
    }
}
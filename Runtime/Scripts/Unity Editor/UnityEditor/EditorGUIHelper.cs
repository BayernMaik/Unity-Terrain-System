#if (UNITY_EDITOR)

using UnityEngine;

namespace TerrainSystem
{
    /// <summary>
    /// Custom Helper class to create proper Inspector Windows
    /// </summary>
    public static class EditorGUIHelper
    {
        /// <summary>
        /// The Space a EditorGUILayout.Space() creates
        /// </summary>
        public static float SpaceHeight = 0.3025f * Screen.height;
    }

    public static class ExtensionMethods_Rect
    {
        public static Rect CenterHorizontal(this Rect rect)
        {
            rect.x = (Screen.width - rect.width) / 2;
            return rect;
        }
        public static Rect ScreenWidth(this Rect rect)
        {
            rect.width = Screen.width;
            return rect;
        }
    }
}

#endif
#if(UNITY_EDITOR)
using UnityEngine;
using UnityEditor;

namespace TerrainSystem
{
    [CustomEditor(typeof(TerrainDataAsset))]
    public class CustomEditor_TerrainDataAsset : Editor
    {
        private TerrainDataAsset terrainDataAsset;
        //private new SerializedObject serializedObject;

        void OnEnable()
        {
            terrainDataAsset = (TerrainDataAsset)target;
            //serializedObject = new SerializedObject(target);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            #region Terrain Name
            terrainDataAsset.DisplayName = EditorGUILayout.TextField(
                "Name",
                terrainDataAsset.DisplayName
            );
            #endregion
            #region Terrain Size
            terrainDataAsset.Width = EditorGUILayout.FloatField(
                "Terrain Width",
                terrainDataAsset.Width
            );
            terrainDataAsset.Length = EditorGUILayout.FloatField(
                "Terrain Length",
                terrainDataAsset.Length
            );
            terrainDataAsset.Height = EditorGUILayout.FloatField(
                "Terrain Height",
                terrainDataAsset.Height
            );
            #endregion
            #region Terrain Surface
            this.terrainDataAsset.SurfaceMin = EditorGUILayout.Slider(
                "Surface Min",
                this.terrainDataAsset.SurfaceMin,
                0,
                this.terrainDataAsset.SurfaceMax
            );
            this.terrainDataAsset.SurfaceMax = EditorGUILayout.Slider(
                "Surface Max",
                this.terrainDataAsset.SurfaceMax,
                this.terrainDataAsset.SurfaceMin,
                this.terrainDataAsset.Height
            );
            #endregion
            #region Terrain HeightMap
            terrainDataAsset.HeightMap = (Texture2D)EditorGUILayout.ObjectField(
                terrainDataAsset.HeightMap,
                typeof(Texture2D),
                false
            );
            #endregion

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
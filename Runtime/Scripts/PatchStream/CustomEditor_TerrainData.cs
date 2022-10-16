#if (UNITY_EDITOR)

using UnityEditor;
using UnityEngine;

namespace Elapsed.Terrain.PatchStream.Component
{
    [CustomEditor(typeof(TerrainDataAsset))]
    public class CustomEditor_TerrainDataAsset : Editor
    {
        private TerrainDataAsset terrainDataAsset;
        // private SerializedObject serializedObject;
     
        // OnEnable
        #region OnEnable
        private void OnEnable()
        {
            terrainDataAsset = (TerrainDataAsset)target;
            // serializedObject = new SerializedObject(target);
        }
        #endregion

        // OnInspectorGUI
        #region OnInspectorGUI
        public override void OnInspectorGUI()
        {
            // Only Expose EditorGUI if enabled in Settings
            if (EditorPrefs.GetBool(Editor_Terrain.EditorPrefsIDs.isEditAssetEnabled))
            {
                serializedObject.Update();

                #region Terrain Name
                terrainDataAsset.DisplayName = EditorGUILayout.TextField(
                    "Terrain Name",
                    terrainDataAsset.DisplayName
                );
                #endregion
                #region Terrain Size
                EditorGUILayout.LabelField("Terrain Size");
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
                EditorGUILayout.Vector2IntField(
                    "Patch Resolution",
                    terrainDataAsset.PatchResolution
                );

                serializedObject.ApplyModifiedProperties();
            }
        }
        #endregion
    }
}
#endif
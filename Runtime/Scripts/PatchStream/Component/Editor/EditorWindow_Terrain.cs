#if (UNITY_EDITOR)

using UnityEditor;

namespace Elapsed.Terrain.PatchStream.Component
{
    public class EditorWindow_Terrain : EditorWindow
    {
        // Menu Item
        #region Menu Item
        [MenuItem("Terrain/Terrain(PatchStream) Settings")]
        private static void MenuItem()
        {
            // Get existing open window or if none, make a new one:
            EditorWindow_Terrain _editorWindow = (EditorWindow_Terrain)EditorWindow.GetWindow(typeof(EditorWindow_Terrain));
            _editorWindow.Show();
        }
        #endregion

        // OnGUI
        #region OnGUI
        private void OnGUI()
        {
            // Drag And Drop
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.Toggle("Enable Drag and Drop", EditorPrefs.GetBool(Editor_Terrain.EditorPrefsIDs.isDragAndDropEnabled));
            if (EditorGUI.EndChangeCheck())
            {
                EditorPrefs.SetBool(
                    Editor_Terrain.EditorPrefsIDs.isDragAndDropEnabled,
                    !EditorPrefs.GetBool(Editor_Terrain.EditorPrefsIDs.isDragAndDropEnabled)
                );
                Editor_Terrain.UpdateDuringSceneGUI();
            }

            // Edit Asset
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.Toggle("Edit Terrain Data Asset Directly", EditorPrefs.GetBool(Editor_Terrain.EditorPrefsIDs.isEditAssetEnabled));
            if (EditorGUI.EndChangeCheck())
            {
                EditorPrefs.SetBool(
                    Editor_Terrain.EditorPrefsIDs.isEditAssetEnabled,
                    !EditorPrefs.GetBool(Editor_Terrain.EditorPrefsIDs.isEditAssetEnabled)
                );
            }    
        }
        #endregion
    }
}
#endif
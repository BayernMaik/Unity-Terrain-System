#if (UNITY_EDITOR)

using UnityEngine;
using UnityEditor;

namespace Elapsed.Terrain.PatchStream.Component
{
    [InitializeOnLoad]
    public static class Editor_Terrain
    {
        // Editor Preferences
        #region Editor Preferences
        public static class EditorPrefsIDs
        {
            private static string _isEditAssetEnabled = "Editor_Terrain_PatchStream_Edit_Asset";
            public static string isEditAssetEnabled
            {
                get { return _isEditAssetEnabled; }
            }

            private static string _isDragAndDropEnabled = "Editor_Terrain_PatchStream_Drag_And_Drop";
            public static string isDragAndDropEnabled
            {
                get { return _isDragAndDropEnabled; }
            }
        }
        #endregion

        // Constructor
        #region Constructor
        // Default Constructor
        #region Default Constructor
        static Editor_Terrain()
        {
            UpdateDuringSceneGUI();
        }
        #endregion
        #endregion

        // Methods
        #region Methods
        // Update During Scene GUI
        #region Update During Scene GUI
        public static void UpdateDuringSceneGUI()
        {
            if (EditorPrefs.GetBool(EditorPrefsIDs.isDragAndDropEnabled))
            {
                SceneView.duringSceneGui += DuringSceneGUI;
            }
            else
            {
                SceneView.duringSceneGui -= DuringSceneGUI;
            }
        }
        #endregion
        // DuringSceneGUI
        #region DuringSceneGUI
        private static void DuringSceneGUI(SceneView sceneView)
        {
            if (SceneView.mouseOverWindow)
            {
                switch (Event.current.type)
                {
                    // Change Cursor Icon when dragging Terrain Data Asset over Scene View
                    case EventType.DragUpdated: 
                        if (DragAndDrop.objectReferences.Length == 1)
                        {
                            if (DragAndDrop.objectReferences[0].GetType() == typeof(TerrainDataAsset))
                            {
                                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                                Event.current.Use();
                            }
                        }
                        break;
                    // Drop Terrain Data to Scene view
                    case EventType.DragPerform:
                        if (DragAndDrop.objectReferences.Length == 1)
                        {
                            if (DragAndDrop.objectReferences[0].GetType() == typeof(TerrainDataAsset))
                            {
                                // Get Terrain_Asset Object from Drag & Drop
                                TerrainDataAsset terrainData = AssetDatabase.LoadAssetAtPath<TerrainDataAsset>(DragAndDrop.paths[0]);
                                Selection.activeGameObject = new Terrain(terrainData.TerrainData).gameObject;
                                //Selection.activeGameObject = TerrainUtility.NewTerrain(terrainData).gameObject;
                                Event.current.Use();
                            }
                        }
                        break;
                }
            }
        }
        #endregion
        #endregion

        // Menu Item
        #region New Terrain
        [MenuItem("Terrain/New PatchStream Terrain")]
        private static void MenuItem()
        {
            // Create Instance of Terrain Data Asset (ScriptableObject)
            TerrainDataAsset _terrainData = (TerrainDataAsset)ScriptableObject.CreateInstance(typeof(TerrainDataAsset));
            // Create Terrain Data Asset in Project View
            AssetDatabase.CreateAsset(_terrainData, "Assets/New Terrain.asset");
            // Create Terrain in Scene View
            //Editor_Terrain.terrain = new Terrain(_terrainData);
        }
        #endregion
    }
}
#endif
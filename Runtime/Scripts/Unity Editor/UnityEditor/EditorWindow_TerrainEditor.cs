#if (UNITY_EDITOR)

using UnityEngine;
using UnityEditor;
using System.IO;

namespace TerrainSystem
{
    public class EditorWindow_TerrainEditor : EditorWindow
    {
        // Variables
        private static bool _bool_DragAndDrop;
        private static bool _bool_CreateDirectory;

        private static string _EditorPrefs_AssetDataPath = "TerrainDataAssetPath";
        private static string _EditorPrefs_CreateAssetDataPath = "CreateTerrainAssetDataPath";

        private static GUIContent _GUIContent_TerrainSettings_TitleContent = new GUIContent("Terrain Editor Settings", "Tooltip");
        private static GUIContent _GUIContent_DragAndDrop = new GUIContent("Enable/Disable Drag & Drop Option", "Allow Terrain SO drop into SceneView");

        private static GUIContent _GUIContent_DefaultTerrainDataAssetPath = new GUIContent("TerrainData Asset Default Path", "Create New Terrain Data Assets at this location");
        private static GUIContent _GUIContent_TextField_DefualtTerrainDataAssetPath = new GUIContent("Asset Path", "");
        private static GUIContent _GUIContent_LabelField_CreateTerrainDataAssetPath = new GUIContent("Create Directory at Path", "Enable/Disable create Directory if it doesnt exist");


        // MenuItems
        [MenuItem("Tools/Terrain System/New Terrain")]
        public static void MenuItem_NewTerrain()
        {
            TerrainDataAsset terrainDataAsset = ScriptableObject.CreateInstance<TerrainDataAsset>();
            // Set Default Path if empty
            if (EditorPrefs.GetString(_EditorPrefs_AssetDataPath) == "")
            {
                EditorPrefs.SetString(_EditorPrefs_AssetDataPath, "Assets/");
            }
            // Create Directory if necessary
            if (!Directory.Exists(EditorPrefs.GetString(_EditorPrefs_AssetDataPath)))
            {
                Directory.CreateDirectory(EditorPrefs.GetString(_EditorPrefs_AssetDataPath));
            }
            // Create Asset in Directory Path
            AssetDatabase.CreateAsset(terrainDataAsset, EditorPrefs.GetString(_EditorPrefs_AssetDataPath) + "New Terrain.asset");
            AssetDatabase.SaveAssets();

            Terrain terrain = new Terrain(terrainDataAsset);
            Selection.activeObject = terrain.gameObject;
        }
        [MenuItem("Tools/Terrain System/Settings")]
        public static void MenuItem_TerrainSettings()
        {
            EditorWindow_TerrainEditor terrainSettings = GetWindow<EditorWindow_TerrainEditor>();
            terrainSettings.titleContent = _GUIContent_TerrainSettings_TitleContent;
        }

        // Behaviors
        private void OnGUI()
        {
            ToggleDragAndDrop();
            EditorGUILayout.Space(5);
            DefaultTerrainData();
        }

        // Methods
        private void ToggleDragAndDrop()    // Toggle SceneView delegate to reduce overhead if required
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(_GUIContent_DragAndDrop);
            _bool_DragAndDrop = EditorPrefs.GetBool("UnityEditor_TerrainEditor_DragAndDrop");
            if (_bool_DragAndDrop != EditorGUILayout.Toggle(_bool_DragAndDrop, GUILayout.Width(15)))
            {
                EditorPrefs.SetBool("UnityEditor_TerrainEditor_DragAndDrop", !_bool_DragAndDrop);
                // Reimport UnityEditor_TerrainEditor Script to apply changes
                string[] assetGUIDs = AssetDatabase.FindAssets("UnityEditor_TerrainEditor", null);
                foreach (string assetGUID in assetGUIDs)
                {
                    string assetPath = AssetDatabase.GUIDToAssetPath(assetGUID);
                    var assetType = AssetDatabase.GetMainAssetTypeAtPath(assetPath);
                    if (assetType == typeof(MonoScript))
                    {
                        AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        private void DefaultTerrainData()
        {
            EditorGUILayout.LabelField(_GUIContent_DefaultTerrainDataAssetPath);
            if (EditorPrefs.GetString(_EditorPrefs_AssetDataPath) == "")    // Set Default Path if empty
            {
                EditorPrefs.SetString(_EditorPrefs_AssetDataPath, "Assets/");
            }
            string tData = EditorGUILayout.TextField(_GUIContent_TextField_DefualtTerrainDataAssetPath, EditorPrefs.GetString(_EditorPrefs_AssetDataPath));
            if (tData[tData.Length - 1] != '/')
            {
                tData += '/';
            }
            EditorPrefs.SetString(_EditorPrefs_AssetDataPath, tData);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(_GUIContent_LabelField_CreateTerrainDataAssetPath);
            if (EditorPrefs.GetBool(_EditorPrefs_CreateAssetDataPath) != EditorGUILayout.Toggle(EditorPrefs.GetBool(_EditorPrefs_CreateAssetDataPath), GUILayout.Width(15)))
            {
                EditorPrefs.SetBool(_EditorPrefs_CreateAssetDataPath, !EditorPrefs.GetBool(_EditorPrefs_CreateAssetDataPath));
                Debug.Log(EditorPrefs.GetBool(_EditorPrefs_CreateAssetDataPath));
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}

#endif
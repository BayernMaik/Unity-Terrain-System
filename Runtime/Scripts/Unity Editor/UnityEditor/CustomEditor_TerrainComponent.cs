#if (UNITY_EDITOR)
/*
using UnityEngine;
using UnityEditor;

using System.Collections;
using System.Collections.Generic;

using Elapsed.Noise;

namespace Elapsed.Terrain
{
    [ExecuteAlways]

    [ExecuteInEditMode]

    [CanEditMultipleObjects]

    [CustomEditor(typeof(TerrainComponent))]
    public class CustomEditor_TerrainComponent : Editor
    {
        private TerrainComponent terrainComponent;

        // SerializedObject
        private new SerializedObject serializedObject;
        private SerializedProperty _terrainDataProperty;
        private Object terrainDataObjectReferenceValue = null;

        private bool _FoldoutHeightMap;
        private bool _DropDownHeightMap;
        private string _loadPath = "";
        private int toolbarSelected = 0;


        private Rect toolBarRect = new Rect((Screen.width - 150) / 2, 5, 150, 20);
        private GUIContent[] toolBarContent;
        GUIContent terrainContent;
        Texture2D img;

        private IEnumerator iEnumerator;

        private Texture2D tex;

        private List<bool> _foldOutNoise;

        void OnEnable()
        {
            terrainComponent = (TerrainComponent)target;
            serializedObject = new SerializedObject(target);

            _terrainDataProperty = serializedObject.FindProperty("_terrainData");

            toolBarContent = new GUIContent[2] {
                new GUIContent("", AssetDatabase.LoadAssetAtPath<Texture>("Assets/Scripts/Terrain/UI/load.png"), "Load"),
                new GUIContent("", AssetDatabase.LoadAssetAtPath<Texture>("Assets/Scripts/Terrain/UI/globe.png"), "Terrain Setup")
            };

            terrainContent = EditorGUIUtility.TrTextContent("Terrain Data Asset", "The TerrainData asset that stores heightmaps, terrain textures, detail meshes and trees.");

            img = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Scripts/Terrain/Unity Editor/UI/load.png");

            _foldOutNoise = new List<bool>();
            for (int i = 0; i < terrainComponent.terrainData.iNoise.Count; i++)
            {
                _foldOutNoise.Add(false);
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            GUILayout.Space(toolBarRect.y * 2 + toolBarRect.height);
            toolbarSelected = GUI.Toolbar(toolBarRect.CenterHorizontal(), toolbarSelected, toolBarContent);
            switch (toolbarSelected)
            {
                case 0: GUI_Load(); break;
                case 1: GUI_TerrainSetup(); break;
                default: break;
            }
            serializedObject.ApplyModifiedProperties();
        }
        private void GUI_Load()
        {
            if (EditorGUIUtility.wideMode)
            {
                EditorGUIUtility.wideMode = false;
                EditorGUIUtility.labelWidth = 0;
            }

            if (GUILayout.Button("New Terrain")) { }
            EditorGUILayout.HelpBox("Unsafed Data will be lost", MessageType.Warning);


            EditorGUILayout.Space(10);

            EditorGUILayout.LabelField("Load from Terrain Asset");
            EditorGUILayout.PropertyField(_terrainDataProperty, terrainContent, GUILayout.ExpandWidth(true));
            if (terrainDataObjectReferenceValue != _terrainDataProperty.objectReferenceValue)
            {
                terrainDataObjectReferenceValue = _terrainDataProperty.objectReferenceValue;
            }

            EditorGUILayout.Space(10);

            EditorGUILayout.LabelField("Load from TerrainData Path");
            _loadPath = EditorGUILayout.TextField(_loadPath);
            GUILayout.Button("Load");
            EditorGUILayout.HelpBox("Directory not found", MessageType.Error);
        }
        private void GUI_TerrainSetup()
        {
            // Layout
            if (!EditorGUIUtility.wideMode)
            {
                EditorGUIUtility.wideMode = true;
                EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth - 212;
            }

            // TerrainData Asset
            EditorGUILayout.PropertyField(_terrainDataProperty, terrainContent, GUILayout.ExpandWidth(true));
            if (terrainDataObjectReferenceValue != _terrainDataProperty.objectReferenceValue)   // TerrainData Asset Changed
            {
                terrainDataObjectReferenceValue = _terrainDataProperty.objectReferenceValue;
            }

            // Terrain Name
            EditorGUILayout.LabelField(new GUIContent("Terrain Name"));
            terrainComponent.terrainData.name = EditorGUILayout.TextField(new GUIContent(""), terrainComponent.terrainData.name);

            // Chunk Setup
            EditorGUILayout.LabelField("Chunks Setup: ");

            terrainComponent.terrainData.voxelResolution = EditorGUILayout.Vector3IntField("SampleRes", terrainComponent.terrainData.voxelResolution);


            terrainComponent.terrainData.chunkBounds = EditorGUILayout.Vector3IntField("Bounds", terrainComponent.terrainData.chunkBounds);
            terrainComponent.terrainData.chunkBounds = Vector3Int.Max(Vector3Int.zero, terrainComponent.terrainData.chunkBounds);

            EditorGUILayout.HelpBox("Terrain Size: " + (terrainComponent.terrainData.chunkSize * terrainComponent.terrainData.chunkBounds), MessageType.Info);

            // Smooth Shading
            terrainComponent.terrainData.smoothShade = EditorGUILayout.Toggle("Smooth Shade", terrainComponent.terrainData.smoothShade);


            // Build Terrain
            if (GUILayout.Button("Build Terrain"))
            {
                BuildTerrain();
            }
            if (terrainComponent.isBuilding)
            {
                EditorGUI.ProgressBar(EditorGUILayout.GetControlRect(), terrainComponent.buildProgress, terrainComponent.buildProgressHint);
            }
            if (GUILayout.Button("Preview"))
            {
                UpdateChunks();
            }

            // HeightData //
            _FoldoutHeightMap = EditorGUILayout.Foldout(_FoldoutHeightMap, new GUIContent("HeightData"), true);
            if (_FoldoutHeightMap)
            {
                for (int i = 0; i < terrainComponent.terrainData.iNoise.Count; i++)
                {
                    switch (terrainComponent.terrainData.iNoise[i].GetType().ToString())
                    {
                        case "Elapsed.Noise.VoronoiNoise":
                            GUI_VoronoiNoise((Voronoi)terrainComponent.terrainData.iNoise[i], i);
                            break;
                        case "Elapsed.Noise.GradientNoise":
                            GUI_GradientNoise((GradientNoise)terrainComponent.terrainData.iNoise[i], i);
                            break;
                    }
                }
                if (GUILayout.Button("Add Layer"))
                {
                    terrainComponent.terrainData.iNoise.Add(new Voronoi());
                    _foldOutNoise.Add(true);
                }
            }
        }

        // Editor GUI Voronoi Noise
        private void GUI_VoronoiNoise(Voronoi voronoiNoise, int i)
        {
            // Foldout
            _foldOutNoise[i] = EditorGUILayout.Foldout(_foldOutNoise[i], new GUIContent(voronoiNoise.name), true);
            if (_foldOutNoise[i])
            {
                //EditorGUI.DrawPreviewTexture();
                // Texture
                voronoiNoise.texture2D = (Texture2D)EditorGUILayout.ObjectField(
                    new GUIContent("Texture"),
                    voronoiNoise.texture2D,
                    typeof(Texture2D),
                    false
                );

                EditorGUI.BeginChangeCheck();
                // Name
                voronoiNoise.name = EditorGUILayout.TextField("Name", voronoiNoise.name);
                // Tiling
                voronoiNoise.tiling = EditorGUILayout.Vector2Field("Tiling", voronoiNoise.tiling);
                // Offset
                voronoiNoise.offset = EditorGUILayout.Vector2Field("Offset", voronoiNoise.offset);
                // Angle Offset
                voronoiNoise.angleOffset = EditorGUILayout.FloatField("Angle Offset", voronoiNoise.angleOffset);
                // Cell Density
                voronoiNoise.cellDensity = EditorGUILayout.FloatField("Cell Density", voronoiNoise.cellDensity);
                // Edge1
                voronoiNoise.edge1 = EditorGUILayout.FloatField("Edge 1", voronoiNoise.edge1);
                // Edge2
                voronoiNoise.edge2 = EditorGUILayout.FloatField("Edge 2", voronoiNoise.edge2);
                if (EditorGUI.EndChangeCheck())
                {
                    voronoiNoise.Apply();
                }
                // Remove Button
                GUI.contentColor = Color.red;
                if (GUILayout.Button("Remove"))
                {
                    terrainComponent.terrainData.iNoise.Remove(voronoiNoise);
                    _foldOutNoise.RemoveAt(i);
                }
                GUI.contentColor = Color.white;
                EditorGUILayout.Space(5);
            }
        }
        // Editor GUI Gradient Noise
        private void GUI_GradientNoise(GradientNoise gradientNoise, int i)
        {
            // Foldout
            _foldOutNoise[i] = EditorGUILayout.Foldout(_foldOutNoise[i], new GUIContent(gradientNoise.name), true);
            if (_foldOutNoise[i])
            {
                EditorGUI.BeginChangeCheck();
                // Name
                gradientNoise.name = EditorGUILayout.TextField("Name", gradientNoise.name);
                // Tiling
                gradientNoise.tiling = EditorGUILayout.Vector2Field("Tiling", gradientNoise.tiling);
                // Offset
                gradientNoise.offset = EditorGUILayout.Vector2Field("Offset", gradientNoise.offset);
                // Scale
                gradientNoise.scale = EditorGUILayout.FloatField("Scale", gradientNoise.scale);
                // Edge1
                gradientNoise.edge1 = EditorGUILayout.FloatField("Edge 1", gradientNoise.edge1);
                // Edge2
                gradientNoise.edge2 = EditorGUILayout.FloatField("Edge 1", gradientNoise.edge2);
                if (EditorGUI.EndChangeCheck())
                {
                    gradientNoise.Apply();
                }
                // Remove Button
                GUI.contentColor = Color.red;
                if (GUILayout.Button("Remove"))
                {
                    terrainComponent.terrainData.iNoise.Remove(gradientNoise);
                    _foldOutNoise.RemoveAt(i);
                }
                GUI.contentColor = Color.white;
                EditorGUILayout.Space(5);
            }
        }



        private IEnumerator _iEnumerator;
        private IEnumerator _IEUpdateChunks;
        public void BuildTerrain()
        {
            //_iEnumerator = terrainComponent.IEBuild();
            _iEnumerator = terrainComponent.IE_GenerateTerrainData();
            EnableEditorApplicationUpdate();
        }
        private void UpdateChunks()
        {
            //_IEUpdateChunks = terrainComponent.UpdateChunks(Vector3.zero);
            EditorApplication.update += IterateUpdateChunks;
        }
        private void IterateUpdateChunks()
        {
            if (_IEUpdateChunks.MoveNext())
            { }
            else
            {
                EditorApplication.update -= IterateUpdateChunks;
            }
        }

        private void EnableEditorApplicationUpdate()
        {
            EditorApplication.update += IterateE;
        }
        private void IterateE()
        {
            if (_iEnumerator.MoveNext())
            {
                this.Repaint();
            }
            else
            {
                EditorApplication.update -= IterateE;
                this.Repaint();
            }
        }
    }
}
*/
#endif
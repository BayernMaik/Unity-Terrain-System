#if (UNITY_EDITOR)

using UnityEditor;
using UnityEngine;

namespace Elapsed.Terrain.PatchStream.Component
{
    [CustomEditor(typeof(TerrainComponent))]
    public class CustomEditor_TerrainComponent : Editor
    {
        /*
        // Variables
        #region  Variables
        // Terrain Target
        #region Terrain Target
        private TerrainComponent _terrain;
        #endregion
        // Serialized Object
        #region Serialized Object
        private SerializedObject _serializedObject;
        #endregion
        // Serialized Property
        #region Serialized Property
        private SerializedProperty _heightMap;
        #endregion
        #endregion

        // OnEnable
        #region OnEnable
        private void OnEnable()
        {
            _terrain = (TerrainComponent)target;
            _serializedObject = new SerializedObject(target);

            _heightMap = _serializedObject.FindProperty("_heightMap");
        }
        #endregion

        // OnInspectorGUI
        #region OnInspectorGUI
        public override void OnInspectorGUI()
        {
            _serializedObject.Update();

            // Terrain Size
            EditorGUILayout.LabelField("Terrain Size");
            _terrain.width = EditorGUILayout.FloatField("Width", _terrain.width);
            _terrain.length = EditorGUILayout.FloatField("Length", _terrain.length);
            _terrain.height = EditorGUILayout.FloatField("Height", _terrain.height);
            EditorGUILayout.Space();

            // Terrain Surface Range
            EditorGUILayout.LabelField("Surface Range");
            _terrain.minSurfaceRange = EditorGUILayout.FloatField("Min. Surface Range", _terrain.minSurfaceRange);
            _terrain.maxSurfaceRange = EditorGUILayout.FloatField("Max. Surface Range", _terrain.maxSurfaceRange);
            EditorGUILayout.Space();

            // Detail Resolution
            EditorGUILayout.LabelField("Detail Resolution");
            _terrain.detailResolution = EditorGUILayout.Vector2IntField("Detail Resolution", _terrain.detailResolution);
            _terrain.detailResolutionPerPatch = EditorGUILayout.Vector2IntField("Detail Per Patch", _terrain.detailResolutionPerPatch);
            EditorGUILayout.HelpBox(
                "Patch Size:       X " + _terrain.patchSize.x + "u, Y " + _terrain.patchSize.y + "u, Z " + _terrain.patchSize.z + "\n" +
                "Patch Dimension:  X " + _terrain.patchDimension.x + ", Y " + _terrain.patchDimension.y + "\n" +
                "Allocated Patches:" + (_terrain.patchDimension.x * _terrain.patchDimension.y),
                MessageType.Info
            );            
            EditorGUILayout.Space();

            // Detail Resolution
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_heightMap, new GUIContent("Height Map"));
            if (EditorGUI.EndChangeCheck())
            {

            }
            EditorGUILayout.HelpBox(
                "Require resampling on change",
                MessageType.Info
            );
            EditorGUILayout.Space();

            _serializedObject.ApplyModifiedProperties();
        }
        #endregion

        // OnSceneGUI
        #region OnSceneGUI
        private void OnSceneGUI()
        {
            if (!SceneView.mouseOverWindow)
            {
                return;
            }

        }
        #endregion
        */
    }
}
#endif
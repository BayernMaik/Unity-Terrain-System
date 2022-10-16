using UnityEngine;
using UnityEditor;

namespace Elapsed.Terrain.PatchStream.Component
{
    public class UnityEditor_TerrainSystem : MonoBehaviour
    {
        static UnityEditor_TerrainSystem()
        {
            if (EditorPrefs.GetBool("UnityEditor_TerrainEditor_DragAndDrop"))
            {
                SceneView.duringSceneGui += duringSceneGUI;
            }
        }

        private static void duringSceneGUI(SceneView sceneView)
        {
            if (Event.current.type == EventType.DragUpdated)
            {
                if (DragAndDrop.objectReferences.Length == 1)
                {
                    if (DragAndDrop.objectReferences[0].GetType() == typeof(TerrainDataAsset))
                    {
                        DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                        Event.current.Use();
                    }
                }
            }
            if (Event.current.type == EventType.DragPerform)
            {
                if (SceneView.mouseOverWindow)
                {
                    if (DragAndDrop.objectReferences.Length == 1)
                    {
                        if (DragAndDrop.objectReferences[0].GetType() == typeof(TerrainDataAsset))
                        {
                            TerrainDataAsset terrainData = AssetDatabase.LoadAssetAtPath<TerrainDataAsset>(DragAndDrop.paths[0]);
                            terrainData.DropToSceneView();
                            Event.current.Use();
                        }
                    }
                }
            }
        }
    }
}
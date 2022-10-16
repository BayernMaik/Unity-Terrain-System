using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainSystem
{
    [ExecuteInEditMode]
    public class OctreeComponent : MonoBehaviour
    {
        void OnDrawGizmosSelected()
        {
            //octree.Draw(Vector3.zero, new Vector3(10, 10, 10));
        }


        public IEnumerator IEBuildOctrees()
        {
            Octree octree = new Octree(5);
            Debug.Log("Number of Nodes: " + octree.CountNodes());
            Debug.Log("Octree: " + octree.GetNodeAt(new int[] { 0 }));

            yield return null;
        }
    }
}
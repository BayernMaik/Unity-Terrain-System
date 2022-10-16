using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainSystem
{
    public static class TerrainLOD
    {
        public static Mesh LOD1(Mesh mesh)
        {
            List<Vector3> newVerts = new List<Vector3>();
            for (int i = 0; i < mesh.vertices.Length; i++)
            {
                if (i % 2 == 0)
                {
                    newVerts.Add(mesh.vertices[i]);
                }
            }

            int[] triangles = new int[newVerts.Count];
            for (int i = 0; i < triangles.Length; i++)
            {
                triangles[i] = i;
            }

            Mesh returnMesh = new Mesh();
            returnMesh.vertices = newVerts.ToArray();
            returnMesh.triangles = triangles;
            returnMesh.RecalculateNormals();
            return returnMesh;
        }

    }
}
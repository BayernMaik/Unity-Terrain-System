using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainSystem
{
    public class Octree
    {
        // Variables
        private Octree[] _subOctrees = null;
        private bool _hasSubOctrees = false;

        // Constructors
        public Octree()
        {
        }
        public Octree(int maxDivisions)
        {
            BuildOctree(this, 0, maxDivisions);
        }
        
        /// <summary>
        /// SubOctrees of this Octree
        /// </summary>
        public Octree[] subOctrees { get { return _subOctrees; } }

        /// <summary>
        /// Builds and returns new Octree(new Octree()) with given amount of subdivisions
        /// </summary>
        /// <param name="octree"></param>
        /// <param name="subDivisions"></param>
        /// <param name="maxDivisions"></param>
        /// <returns></returns>
        public Octree BuildOctree(Octree octree, int subDivisions, int maxDivisions)
        {
            if (maxDivisions == 0)
            {
                _hasSubOctrees = false;
                return octree;
            }

            if (subDivisions < maxDivisions)
            {
                octree._subOctrees = new Octree[8];
                octree._hasSubOctrees = true;
                for (int i = 0; i < 8; i++)
                {
                    octree._subOctrees[i] = BuildOctree(new Octree(), subDivisions + 1, maxDivisions);
                }
            }

            return octree;
        }
        /// <summary>
        /// Find Node at given Branch Indices, return Null if there is none
        /// </summary>
        /// <param name="branch"></param>
        /// <returns></returns>
        public Octree GetNodeAt(int[] branch)
        {
            Octree octree = this;
            for (int i = 0; i < branch.Length; i++)
            {
                if (!octree._hasSubOctrees)
                {
                    return null;
                }
                octree = octree._subOctrees[branch[i]];
            }
            return octree;
        }
        /// <summary>
        /// Number of existing Nodes in Hirarchy
        /// </summary>
        /// <returns></returns>
        public int CountNodes()
        {
            int count = 1;
            if (this == null)
            {
                count = 0;
            }
            if (_subOctrees != null)
            {
                for (int i = 0; i < _subOctrees.Length; i++)
                {
                    count += _subOctrees[i].CountNodes();
                }
            }
            return count;
        }
        /// <summary>
        /// SubDivide an Octree
        /// </summary>
        public void SubDivide()
        {
            if (_hasSubOctrees)
            {
                return;
            }
            this._subOctrees = new Octree[8];
            for (int i = 0; i < 8; i++)
            {
                this._subOctrees[i] = new Octree();
            }
        }

        public void DrawWireCube(Vector3 position, Vector3 size)
        {
            //Gizmos.DrawCube(position + size / 2, size);
            Gizmos.DrawWireCube(position + size / 2, size);
        }
    }
}
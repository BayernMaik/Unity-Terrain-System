using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elapsed.Utility
{
    public class Physics_Manager : MonoBehaviour
    {
        private void Awake()
        {
            PhysicsUtility.Gravity.GravityMagnitude();
            PhysicsUtility.Gravity.GravityUp();
        }
        private void FixedUpdate()
        {
        }
    }
}
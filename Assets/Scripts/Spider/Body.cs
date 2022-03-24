using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Spider
{
    public class Body : MonoBehaviour
    {
         [SerializeField] private Transform[] _legTargets;


        private void Update()
        {
            HandleRotation();
        }


        private void HandleRotation()
        {
            Vector3 pairLegs1 = _legTargets[3].position - _legTargets[0].position;
            Vector3 pairLegs2 = _legTargets[2].position - _legTargets[1].position;

            transform.up = Vector3.Cross(pairLegs1, pairLegs2);

        }
    }
}

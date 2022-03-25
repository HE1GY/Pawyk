using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Spider
{
    public class Body : MonoBehaviour
    {
         [SerializeField] private Transform[] _legTips;


         private void Update()
        {
            HandleRotation();
        }


        private void HandleRotation()
        {
            Vector3 pairLegs1 = _legTips[3].position - _legTips[0].position;
            Vector3 pairLegs2 = _legTips[2].position - _legTips[1].position;

            transform.up = Vector3.Cross(pairLegs1, pairLegs2);

        }
        
    }
}

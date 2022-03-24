using System;
using UnityEngine;

namespace Spider
{
    public class RayThrower : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Transform _pointer;
    
        public Vector3 HitPosition { get; private set; }

        private void Awake()
        {
            HandleRayCasting();
        }


        private void Update()
        {
            HandleRayCasting();
        }

        private void HandleRayCasting()
        {
            Vector3 rayOrg = transform.position;
            Vector3 rayDir = -transform.up;
            Ray ray=new Ray(rayOrg,rayDir);
        
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 100, _layerMask))
            {
                HitPosition = raycastHit.point;
                _pointer.position = HitPosition;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  SplitScreen
{
    public class CameraController : MonoBehaviour
    {
        // Start is called before the first frame update
        public Transform target;
        public Vector3 offset;
        public float smoothness;
        private Vector3 _velocity;
        void Start()
        {
            if (target)
            {
                offset = transform.position - target.position;
            }
        }

        void FixedUpdate()
        {
            if (target)
            {
                transform.position = Vector3.SmoothDamp(
                    transform.position,
                    target.position + offset,
                    ref _velocity,
                    smoothness
                );
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}


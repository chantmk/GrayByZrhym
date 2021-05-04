﻿using UnityEngine;

namespace Players
{
    public class CameraManager : MonoBehaviour
    {
        private Transform target;
        public float SmoothTime = 0.3F;
        private Vector3 velocity = Vector3.zero;

        void Start()
        {
            target = GameObject.Find("Player").transform;
        }

        void Update()
        {
            // Define a target position above and behind the target transform
            Vector3 targetPosition = target.TransformPoint(new Vector3(0, 0, -10));

            // Smoothly move the camera towards that target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);
        }
    }
}
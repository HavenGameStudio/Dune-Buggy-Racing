using System;
using Haven.DuneBuggyRacing._Scripts.Core.Managers;
using UnityEngine;

namespace Haven.DuneBuggyRacing._Scripts.Vehicle
{
    // File: Buggy.cs
    // Author: Adrian Caamino
    // Purpose: Central configuration and data hub for the buggy systems.
    // AI usage: No AI code was generated automatically.
    
    [RequireComponent(typeof(Rigidbody))]
    public class Buggy : MonoBehaviour
    {
        [Header("Driving Forces")]
        public float motorForce = 100f;
        public float brakeForce = 100f;
        public float maxSteerAngle = 30f;

        [Header("Wheel Colliders")]
        public WheelCollider flWheelCollider;
        public WheelCollider frWheelCollider;
        public WheelCollider rlWheelCollider;
        public WheelCollider rrWheelCollider;

        [Header("Wheel Visuals")]
        public Transform flWheelTransform;
        public Transform frWheelTransform;
        public Transform rlWheelTransform;
        public Transform rrWheelTransform;
        
        [Space]
        private Rigidbody rb;

        private UIManager uiManager;
        
        /// <summary>
        /// get the current speed by getting the magnitude of the linear velocity of the rigidbody.
        /// </summary>
        public float currentSpeed
        {
            get
            {
                if (!rb)
                {
                    Debug.LogError($"Rigidbody is null. You might want to add rigidbody this gameobject. ");
                    return 0f;
                }

                return rb.linearVelocity.magnitude;
            }
        } 

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            uiManager = UIManager.Instance;
        }

        private void Update()
        {
            uiManager.UpdateSpeedText(currentSpeed);
        }
    }
}
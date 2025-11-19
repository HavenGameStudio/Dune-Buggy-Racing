using UnityEngine;

namespace Haven.DuneBuggyRacing._Scripts.Vehicle
{
    // File: BuggyMovementController.cs
    // Author: Adrian Caamino
    // Purpose: Handles player input to control a simple WheelCollider-based dune buggy.
    // Date: 2025-10-30
    // AI usage: No AI code was generated automatically. 

    public class BuggyMovementController : MonoBehaviour
    {
        [Header("Driving Forces")]
        [SerializeField] private float motorForce = 100f;
        [SerializeField] private float brakeForce = 100f;
        [SerializeField, Tooltip("Maximum steering angle in degrees")]
        private float maxSteerAngle = 30f;

        [Header("Wheel Colliders")]
        [SerializeField] private WheelCollider flWheelCollider;
        [SerializeField] private WheelCollider frWheelCollider;
        [SerializeField] private WheelCollider rlWheelCollider;
        [SerializeField] private WheelCollider rrWheelCollider;

        [Header("Wheel Visuals")]
        [SerializeField] private Transform flWheelTransform;
        [SerializeField] private Transform frWheelTransform;
        [SerializeField] private Transform rlWheelTransform;
        [SerializeField] private Transform rrWheelTransform;

        private float _horizontalInput;
        private float _verticalInput;
        private bool _isBraking;

        // For debugging/telemetry
        private float _currentSteerAngle;
        private float _currentBrakeForce;

        private void Update()
        {
            // Get input
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
            _isBraking = Input.GetKey(KeyCode.Space);
        }

        private void FixedUpdate()
        {
            HandleMotor();
            HandleSteering();
            UpdateWheels();
        }

        /// <summary>
        /// Applies motor and brake force to the front wheels.
        /// </summary>
        private void HandleMotor()
        {
            flWheelCollider.motorTorque = _verticalInput * motorForce;
            frWheelCollider.motorTorque = _verticalInput * motorForce;

            _currentBrakeForce = _isBraking ? brakeForce : 0f;
            ApplyBraking();
        }

        /// <summary>
        /// Applies braking torque to all wheels.
        /// </summary>
        private void ApplyBraking()
        {
            flWheelCollider.brakeTorque = _currentBrakeForce;
            frWheelCollider.brakeTorque = _currentBrakeForce;
            rlWheelCollider.brakeTorque = _currentBrakeForce;
            rrWheelCollider.brakeTorque = _currentBrakeForce;
        }

        /// <summary>
        /// Applies steering to front wheels.
        /// </summary>
        private void HandleSteering()
        {
            _currentSteerAngle = maxSteerAngle * _horizontalInput;
            flWheelCollider.steerAngle = _currentSteerAngle;
            frWheelCollider.steerAngle = _currentSteerAngle;
        }

        /// <summary>
        /// Updates the visual transform of a wheel to match its collider.
        /// </summary>
        private static void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
        {
            if (wheelCollider == null || wheelTransform == null) return;

            wheelCollider.GetWorldPose(out var pos, out var rot);
            wheelTransform.position = pos;
            wheelTransform.rotation = rot;
        }

        /// <summary>
        /// Syncs all wheel meshes to collider pose.
        /// </summary>
        private void UpdateWheels()
        {
            UpdateSingleWheel(flWheelCollider, flWheelTransform);
            UpdateSingleWheel(frWheelCollider, frWheelTransform);
            UpdateSingleWheel(rlWheelCollider, rlWheelTransform);
            UpdateSingleWheel(rrWheelCollider, rrWheelTransform);
        }
    }
}

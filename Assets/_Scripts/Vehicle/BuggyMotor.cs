using UnityEngine;

namespace Haven.DuneBuggyRacing._Scripts.Vehicle
{
    // File: Buggy.cs
    // Author: Adrian Caamino
    // Purpose: Handles the forward and backward movement of the buggy. 
    // AI usage: No AI code was generated automatically.
    public class BuggyMotor : MonoBehaviour
    {
        private enum VehicleType{ Fwd, Rwd, Awd}

        [SerializeField, Tooltip("The type of vehicle motor (Fwd: " +
                                 "Front Wheel Drive - only the 2 wheels of the front drives the vehicle" +
                                 "Rwd: Rear Wheel Drive - only the 2 wheels of the rear drives the vehicle." +
                                 "Awd: Rear Wheel Drive - all 4 wheels drives the vehicle.")] 
        private VehicleType vehicleType = VehicleType.Fwd;
        
        private Buggy _buggy;
        private float _currentBrakeForce;

        private void Awake() => _buggy = GetComponent<Buggy>();
        
        /// <summary>
        /// Method to apply torque to the wheels to move the car.
        /// </summary>
        /// <param name="verticalInput"></param>
        /// <param name="isBraking"></param>
        public void ApplyMotor(float verticalInput, bool isBraking)
        {
            float motorForce = _buggy.motorForce;
            float brakeForce = _buggy.brakeForce;

            // Apply motor torque based on drive type
            switch (vehicleType)
            {
                case VehicleType.Fwd:
                    ApplyTorqueToWheels(verticalInput * motorForce, _buggy.flWheelCollider, _buggy.frWheelCollider);
                    break;

                case VehicleType.Rwd:
                    ApplyTorqueToWheels(verticalInput * motorForce, _buggy.rlWheelCollider, _buggy.rrWheelCollider);
                    break;

                case VehicleType.Awd:
                    ApplyTorqueToWheels(verticalInput * motorForce,
                        _buggy.flWheelCollider, _buggy.frWheelCollider,
                        _buggy.rlWheelCollider, _buggy.rrWheelCollider);
                    break;
            }

            _currentBrakeForce = isBraking ? brakeForce : 0f;
            ApplyBraking();
        }

        /// <summary>
        /// Helper method to apply motor torque to specified wheels.
        /// </summary>
        private void ApplyTorqueToWheels(float torque, params WheelCollider[] wheels)
        {
            foreach (var wheel in wheels)
                wheel.motorTorque = torque;
        }

        /// <summary>
        /// Apply a break torque to all wheels to stop the vehicle.
        /// </summary>
        private void ApplyBraking()
        {
            _buggy.flWheelCollider.brakeTorque = _currentBrakeForce;
            _buggy.frWheelCollider.brakeTorque = _currentBrakeForce;
            _buggy.rlWheelCollider.brakeTorque = _currentBrakeForce;
            _buggy.rrWheelCollider.brakeTorque = _currentBrakeForce;
        }
        
        /// <summary>
        /// Updates all wheels by calling the UpdateWheel method.
        /// </summary>
        public void UpdateWheels()
        {
            UpdateWheel(_buggy.flWheelCollider, _buggy.flWheelTransform);
            UpdateWheel(_buggy.frWheelCollider, _buggy.frWheelTransform);
            UpdateWheel(_buggy.rlWheelCollider, _buggy.rlWheelTransform);
            UpdateWheel(_buggy.rrWheelCollider, _buggy.rrWheelTransform);
        }
        
        /// <summary>
        /// Updates and sets the position and rotation of the wheel model.
        /// </summary>
        /// <param name="col">the wheel collider to get the world pose.</param>
        /// <param name="t">Transform of the wheel model to rotate visually.</param>
        private static void UpdateWheel(WheelCollider col, Transform t)
        {
            if (col == null || t == null) return;
            col.GetWorldPose(out var pos, out var rot);
            t.SetPositionAndRotation(pos, rot);
        }
    }
}
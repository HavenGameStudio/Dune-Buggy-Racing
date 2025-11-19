using UnityEngine;

namespace Haven.DuneBuggyRacing._Scripts.Vehicle
{
    // File: Buggy.cs
    // Author: Adrian Caamino
    // Purpose: Handles the steering of the buggy.
    // AI usage: No AI code was generated automatically.
    public class BuggySteering : MonoBehaviour
    {
        private Buggy _buggy;

        private void Awake()
        {
            _buggy = GetComponent<Buggy>();
        }
        
        /// <summary>
        /// Applies the steering of the front wheel colliders.
        /// </summary>
        /// <param name="horizontalInput"></param>
        public void ApplySteering(float horizontalInput)
        {
            float steerAngle = _buggy.maxSteerAngle * horizontalInput;
            _buggy.flWheelCollider.steerAngle = steerAngle;
            _buggy.frWheelCollider.steerAngle = steerAngle;
        }
    }
}
using UnityEngine;

namespace Haven.DuneBuggyRacing._Scripts.Vehicle
{
    // File: Buggy.cs
    // Author: Adrian Caamino
    // Purpose: Handles all the inputs of all the buggy.
    //NOTE: I used Unity's Old Input System instead of the new one because it is recommendable for webGL builds. 
    // AI usage: No AI code was generated automatically.
    [RequireComponent(typeof(Buggy), typeof(BuggyMotor), typeof(BuggySteering))]
    public class BuggyController : MonoBehaviour
    {
        [Header("Keycode Controls")]
        
        [SerializeField, Tooltip("Keycode to press on keyboard to apply brake")] 
        private KeyCode applyBrakeKeyCode = KeyCode.Space;

        public bool enableControls;
        
        private BuggyMotor _motor;
        private BuggySteering _steering;

        private float _horizontalInput;
        private float _verticalInput;
        private bool _isBraking;

        private void Awake()
        {
            _motor = GetComponent<BuggyMotor>();
            _steering = GetComponent<BuggySteering>();
        }
        
        
        // since I decided to use Unity's old input system, I am getting the input on the update method. 
        private void Update()
        {
            if (!enableControls) return;
            
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
            _isBraking = Input.GetKey(applyBrakeKeyCode);
        }
        
        //I put the physics calls to fixed update since it meant for physics changes.
        private void FixedUpdate()
        {
            _motor.ApplyMotor(_verticalInput, _isBraking);
            _steering.ApplySteering(_horizontalInput);
            _motor.UpdateWheels();
        }
    }
}
using System;
using UnityEngine;

namespace PhysicsComponents
{
    public class CharacterPhysics : PhysicsBase
    {
        public Player Player;

        public CharacterProfileData Settings;
        
        private bool _endedJumpEarly;
        private float _currentVerticalSpeed;
        private float _fallSpeed;
        private float _apexPoint;
        
        public Vector2 Velocity { get; set; }
        public bool JumpingThisFrame { get; set; }
        
        private void Start()
        {
            if (_rigidbody2D == null)
            {
                _rigidbody2D = GetComponent<Rigidbody2D>();
            }
            
            if (Player == null)
            {
                Player = Player.Instance;
            }
        }

        private void Update()
        {
            UpdateHitResults();
            CalculateJump();
        }

        private void FixedUpdate()
        {
            CalculateGravity();
            CalculateJumpApex();
            SetVelocityY(_currentVerticalSpeed);
        }
        
        private void CalculateJumpApex() 
        {
            if (!Grounded) 
            {
                _apexPoint = Mathf.InverseLerp(Settings.JumpApexThreshold, 0, Mathf.Abs(Velocity.y));
                _fallSpeed = Mathf.Lerp(Settings.MinFallSpeed, Settings.MaxFallSpeed, _apexPoint);
                return;
            }
            _apexPoint = 0;
        }

        private void CalculateJump() {
            if (Player.Settings.JumpKey.PressedThisFrame() && Grounded) 
            {
                _currentVerticalSpeed = Settings.JumpHeight;
                _endedJumpEarly = false;
                JumpingThisFrame = true;
            }
            else 
            {
                JumpingThisFrame = false;
            }

            
            if (!Grounded && Player.Settings.JumpKey.LiftedThisFrame() && !_endedJumpEarly && Velocity.y > 0) 
            {
                _endedJumpEarly = true;
            }
        }
        
        private void CalculateGravity()
        {
            if (Grounded) 
            {
                if (_currentVerticalSpeed < 0) _currentVerticalSpeed = 0;
                return;
            }
            
            var fallSpeed = _endedJumpEarly && _currentVerticalSpeed > 0 ? _fallSpeed * Settings.JumpEndEarlyGravityModifier : _fallSpeed;
                
            _currentVerticalSpeed -= fallSpeed * Time.deltaTime;
                
            if (_currentVerticalSpeed < Settings.FallClamp) _currentVerticalSpeed = Settings.FallClamp;
            
        }

        private void OnHit()
        {
            
        }
    }
}
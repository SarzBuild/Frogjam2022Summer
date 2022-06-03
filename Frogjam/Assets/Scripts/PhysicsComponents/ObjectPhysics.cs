using System;
using UnityEngine;

namespace PhysicsComponents
{
    public class ObjectPhysics : PhysicsBase
    {
        public ObjectSpawner ObjectSpawner { get { return _objectSpawner ;} }
        [SerializeField] private ObjectSpawner _objectSpawner;
    
        public Player Player { get { return _player ;} }
        [SerializeField] private Player _player;

        public Vector2 Velocity;
    
        [SerializeField] private float FallClamp = -20;
        [SerializeField] private float MaximumFallSpeed = 80;
        [SerializeField] private float MinimumFallSpeed = 40;
        [SerializeField] private float Force = 5;
    
        private float ApexPoint;
        private float CurrentFallSpeed;
    
        public void SetComponents(ObjectSpawner objectSpawner)
        {
            if (_rigidbody2D == null)
            {
                _rigidbody2D = GetComponent<Rigidbody2D>();
            }

            if (_player == null)
            {
                _player = Player.Instance;
            }
        
            if(_objectSpawner == null)
            {
                _objectSpawner = objectSpawner;
            }
        
            _canSetVelocity = false;
        }
    
    
        private void CalculateGravity()
        {
            if (_groundCheck) return;
            if (_canSetVelocity)
            {
                ApexPoint = Mathf.InverseLerp(10f, 0, Mathf.Abs(_appliedVelocity.y));
                CurrentFallSpeed = Mathf.Lerp(MinimumFallSpeed, MaximumFallSpeed, ApexPoint);
                Velocity.y -= CurrentFallSpeed * Time.fixedDeltaTime;
                if (Velocity.y < FallClamp) Velocity.y = FallClamp;
                return;
            }
            Velocity.y = 0f;
        }

        private void CalculateDeceleration()
        {
            Velocity.x = Mathf.MoveTowards(Velocity.x, 0, 8 * Time.fixedDeltaTime);
        }

        private void Update()
        {
            if (Player.Settings.MouseLeftKey.Lifted())
            {
                DropObject();
            }
        }

        private void DropObject()
        {
            if (transform.parent != null)
            {
                _canSetVelocity = true;
                ObjectSpawner.RemoveFromOwner(gameObject);
                Velocity += Player.Velocity/Force;
            }
        }
    
        private void FixedUpdate()
        {
            SetVelocity(Velocity);
            CalculateGravity();
            CalculateDeceleration();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            Velocity /= 5;
        }

        public void SetOwner(Transform parent)
        {
            SetVelocityZero();
            _canSetVelocity = false;
            ObjectSpawner.SetToOwner(parent,gameObject);
        }
    }
}
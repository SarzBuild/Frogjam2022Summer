using UnityEngine;

namespace PhysicsComponents
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class PhysicsBase : MonoBehaviour
    {
        [SerializeField] private protected Transform _groundCheck;
        [SerializeField] public LayerMask _groundLayerMask;
        [SerializeField] private protected float _groundCheckRadius;
        [SerializeField] private protected bool _canSetVelocity;
        [SerializeField] public Vector2 _appliedVelocity;
        [SerializeField] private Vector2 _currentVelocity;
        [SerializeField] protected Rigidbody2D _rigidbody2D;

        public void ApplyVelocity() => _currentVelocity = _rigidbody2D.velocity;
    
        public void SetVelocityZero()
        {
            _appliedVelocity = Vector2.zero;
            SetFinalVelocity();
        }
    
        public void SetVelocityXWithYZero(float velocity)
        {
            _appliedVelocity = new Vector2(velocity, 0);
            SetFinalVelocity();
        }
    
        public void SetVelocityX(float velocity)
        {
            _appliedVelocity = new Vector2(velocity, _currentVelocity.y);
            SetFinalVelocity();
        }

        public void SetVelocityY(float velocity)
        {
            _appliedVelocity = new Vector2(_currentVelocity.x, velocity);
            SetFinalVelocity();
        }
        
        public void SetVelocity(float velocity, Vector2 direction)
        {
            _appliedVelocity = direction * velocity;
            SetFinalVelocity();
        }

        public void SetVelocity(Vector2 velocity)
        {
            _appliedVelocity = velocity;
            SetFinalVelocity();
        }
        
        private void SetFinalVelocity()
        {
            if (!_canSetVelocity) return;
            _rigidbody2D.velocity = _appliedVelocity;
            _currentVelocity = _appliedVelocity;
        }
    
        public void ToggleVelocity()
        {
            _rigidbody2D.velocity = Vector2.zero;
            _canSetVelocity = !_canSetVelocity;
        }

        #region Collisions

        public void UpdateHitResults()
        {
            UpdateGrounded();
        }
    
        public bool Grounded { get { return GroundedResult; } }
        public Collider2D GroundedResult { get; private set; }
        public void UpdateGrounded() { GroundedResult = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayerMask); }

        #endregion
    }
}
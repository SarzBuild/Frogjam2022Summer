using UnityEngine;

namespace Generics
{
    public class GenericDirectionalMovement : MonoBehaviour
    {
        public TransformTypes Direction;
        public enum TransformTypes
        {
            FORWARDS,
            BACKWARDS,
            LEFT,
            RIGHT,
            UP,
            DOWN
        }
    
        public float Speed;
        private Vector3 _direction;

        private void FixedUpdate()
        {
            _direction = Direction switch
            {
                TransformTypes.UP => transform.up,
                TransformTypes.DOWN => -transform.up,
                TransformTypes.RIGHT => transform.right,
                TransformTypes.LEFT => -transform.right,
                TransformTypes.FORWARDS => transform.forward,
                TransformTypes.BACKWARDS => -transform.forward,
                _ => _direction
            };
            transform.position += _direction * Speed;
        }
    }
}
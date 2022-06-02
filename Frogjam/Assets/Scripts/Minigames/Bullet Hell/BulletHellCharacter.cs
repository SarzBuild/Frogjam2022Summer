using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellCharacter : MonoBehaviour
{

    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private float _decelerationSpeed;
    private void Update()
    {
        Vector2 mousePosition = GetMousePositionInWorld();
        Vector2 movementDelta = (mousePosition - new Vector2(transform.position.x, transform.position.y)).normalized;
        float angleDelta = Vector2.Dot(_rigidbody.velocity, movementDelta);

        if (angleDelta > 0)
        {
            _rigidbody.AddForce(_speed * movementDelta);
        }
        else
        {
            _rigidbody.velocity = _rigidbody.velocity * (angleDelta + 1);
            _rigidbody.AddForce(_speed * movementDelta);
        }

        
        // if moving away from the mouse, decelerate

    }

    private Vector2 GetMousePositionInWorld()
    {
        // Converts screen position of a click to world position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(mousePosition.x, mousePosition.y);
    }
}

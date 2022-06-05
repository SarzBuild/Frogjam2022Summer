using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _lifespan = 10;
    private float _timer = 0;
    private bool _isTear = false;
    [SerializeField] private Sprite _tearSprite;
    [SerializeField] private Sprite _largeBulletSprite;
    private Rigidbody2D _rigidbody;
    public enum BulletTypes
    {
        Small,
        Large,
        Tear
    }

    private void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer > _lifespan)
        {
            Destroy(gameObject);
        }
        if(_isTear)
        {
            // Rotate the tear in accordance with velocity

            // First get the angle of movement
            float movementAngle;
            if (_rigidbody.velocity.x == 0)
            {
                if (_rigidbody.velocity.y > 0)
                {
                    movementAngle = 180;
                }
                else
                {
                    movementAngle = 0;
                }
            }
            else
            {
                movementAngle = 90 + Mathf.Atan(_rigidbody.velocity.y / _rigidbody.velocity.x) * 360 / (Mathf.PI * 2);
            }

            if (_rigidbody.velocity.x < 0)
            {
                movementAngle = movementAngle + 180;
            }
            // Next set rotation accordingly
            transform.eulerAngles = new Vector3(0, 0, movementAngle);
            transform.eulerAngles = new Vector3(0, 0, movementAngle);
        }
    }

    public void SetType(BulletTypes type)
    {
        if(type == BulletTypes.Large)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = _largeBulletSprite;
        }
        if(type == BulletTypes.Tear)
        {
            _isTear = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            gameObject.GetComponent<SpriteRenderer>().sprite = _tearSprite;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _lifespan = 10;
    private float _timer = 0;
    private bool _isTear = false;
    [SerializeField] private Sprite _tearSprite;

    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer > _lifespan)
        {
            Destroy(gameObject);
        }
        if(_isTear)
        {
            // Rotate in accordance with velocity
        }
    }

    public void BecomeTear()
    {
        _isTear = true;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        gameObject.GetComponent<SpriteRenderer>().sprite = _tearSprite;
    }
}

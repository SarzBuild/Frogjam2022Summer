using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _lifespan = 10;
    private float _timer = 0;

    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer > _lifespan)
        {
            Destroy(gameObject);
        }
    }
}

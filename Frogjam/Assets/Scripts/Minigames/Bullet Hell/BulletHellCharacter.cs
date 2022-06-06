using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class BulletHellCharacter : MonoBehaviour
{
    
    [Header("Pointers")]
    [SerializeField] private Transform _respawnPoint;
    [HideInInspector] public List<BulletSpawner> BulletSpawners;
    [SerializeField] private TimerOfDoom _timerOfDoom;
    [SerializeField] private AudioSource _deathSound;
    [SerializeField] private AudioSource _damageSound;
    public GameEventData StartPhase3;
    [Header("Parameters")]
    public float MaxHitPoints;
    [SerializeField] private float _minimumSpeed;
    public bool DEBUG_GRADIENTMOVEMENT;

    private Rigidbody2D _rigidbody;
    private float _speed;
    [HideInInspector] public float CurrentHitPoints;
    private float _deathCount = 0;
    

    private void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        BulletSpawners = new List<BulletSpawner>();
        CurrentHitPoints = MaxHitPoints;
    }

    private void Update()
    {
        // Real simple movement
        Vector2 mousePosition = GetMousePositionInWorld();
        float distanceToTarget = (mousePosition - new Vector2(transform.position.x, transform.position.y)).magnitude;

        if(DEBUG_GRADIENTMOVEMENT)
        {
            // Gradient speed option
            _speed = Mathf.Ceil(Mathf.Pow(distanceToTarget, 2));
            if (_speed < _minimumSpeed)
            {
                _speed = _minimumSpeed;
            }
        }
        else
        {
            // Binary speed option
            if (distanceToTarget < 1)
            {
                _speed = _minimumSpeed;
            }
            else
            {
                _speed = _minimumSpeed * 2;
            }
        }

        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), mousePosition, _speed * Time.deltaTime);
        // Since we're not really using physics here, this must be done
        if(_rigidbody.velocity != Vector2.zero)
        {
            _rigidbody.velocity = Vector2.zero;
        }
        
    }

    private Vector2 GetMousePositionInWorld()
    {
        // Converts screen position of a click to world position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(mousePosition.x, mousePosition.y);
    }

    private void Respawn()
    {
        _deathCount++;
        if(_deathCount == 3)
        {
            StartPhase3.Raise();
        }
        if(_deathCount > 3)
        {

        }
        CurrentHitPoints = MaxHitPoints;
        transform.position = _respawnPoint.position;
        _timerOfDoom.ResetTimer();
        // Despawn bullets
        foreach(BulletSpawner spawner in BulletSpawners)
        {
            spawner.DespawnBullets();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            Destroy(collision.gameObject);
            CurrentHitPoints -= 1;
            // TODO: Update health bar UI
            if(CurrentHitPoints <= 0)
            {
                _deathSound.Play();
                Respawn();
            }
            else
            {
                _damageSound.Play();
            }
        }
    }
}

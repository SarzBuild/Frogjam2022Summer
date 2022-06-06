using System;
using System.Collections;
using System.Collections.Generic;
using GeneralJTUtils;
using PhysicsComponents;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player _instance;
    public static Player Instance {
        get
        {
            if (_instance != null) return _instance;
            
            var singleton = FindObjectOfType<Player>();
            if (singleton != null) return _instance;
            
            var go = new GameObject();
            _instance = go.AddComponent<Player>();
            return _instance;
        }
    }
    
    public Sprite MouseCursor { get { return _MouseCursor ;} }
    [SerializeField] private Sprite _MouseCursor;
    public KeyMapData Settings { get { return _keyMapData ;} }
    [SerializeField] private KeyMapData _keyMapData;

    public Transform ObjectHoldPosition;
    public SpriteRenderer SpriteRenderer;
    
    public Vector2 Velocity { get; private set; }
    public Vector2 LastPosition { get; private set; }

    private void Awake()
    {
        Singleton();
    }

    private void Singleton()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this; 
        }
    }

    private void Update()
    {
        UpdateMouseHit();

        UpdateCursorSprite();
        CheckForObject();
    }

    private void UpdateCursorSprite()
    {
        if (SpriteRenderer.sprite != MouseCursor)
        {
            SpriteRenderer.sprite = MouseCursor;
        }
    }
    
    private void CheckForObject()
    {
        if (!MouseHit) return;
        if (MouseHitResult.transform.gameObject.layer == LayerMask.NameToLayer("ObjectBag") && Settings.MouseLeftKey.PressedThisFrame())
        {
            MouseHitResult.transform.GetComponent<ObjectSpawner>().SetOwner(this);
        }

        if (MouseHitResult.transform.gameObject.layer == LayerMask.NameToLayer("PickableObject") && Settings.MouseLeftKey.PressedThisFrame())
        {
            MouseHitResult.transform.GetComponent<ObjectPhysics>().SetOwner(transform);
        }
    }

    private void FixedUpdate()
    {
        UpdatePlayerVelocity();
    }
    
    private void UpdatePlayerVelocity()
    {
        Velocity = ((Vector2)transform.position - LastPosition) / Time.deltaTime;
        LastPosition = transform.position;
    }

    private void LateUpdate()
    {
        UpdatePlayerLocation();
    }

    private void UpdatePlayerLocation()
    {
        transform.position = JTUtils.GetMousePositionWorld2D(Camera.main);
    }

    public bool MouseHit { get { return MouseHitResult; } }
    public RaycastHit2D MouseHitResult { get; private set; }
    public void UpdateMouseHit() { MouseHitResult = Physics2D.Raycast(JTUtils.GetMousePositionWorld2D(Camera.main), Vector2.zero); }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Velocity,Vector3.one);
        Gizmos.DrawWireCube(LastPosition,Vector3.one);
    }
}

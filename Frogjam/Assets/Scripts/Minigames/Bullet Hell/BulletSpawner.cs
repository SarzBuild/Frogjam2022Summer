using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public BulletHellCharacter Player;
    public GameObject BulletObject;

    // Store data for different bullet patterns (aka firing modes)
    [HideInInspector] public FiringMode MachineGun;
    [HideInInspector] public FiringMode Sprinkler;
    [HideInInspector] public FiringMode QuadShot;
    [HideInInspector] public FiringMode Tears;

    

    // Current firing mode
    [HideInInspector] public FiringMode CurrentFiringMode;

    [HideInInspector] public List<FiringMode> FiringModeList;

    private float _firingTimer = 0;
    private float _switchModeTimer = 0;
    private float _cryingTimer = 0;

    private List<GameObject> _bullets;


    [HideInInspector] public bool Attacking = false;
    [HideInInspector] public bool Crying = false;
    
    // Audio
    [SerializeField] private AudioSource _tearsAudio;
    [SerializeField] private AudioSource _bulletAudio;

    private void Awake()
    {
        // Define firing modes (this got so messy! I really should have used scriptable objects lol)
        float[] sprinklerAngles = new float[1];
        sprinklerAngles[0] = Random.Range(0f, 360f);
        Sprinkler = new FiringMode(sprinklerAngles, 0.1f, 1f, 0, 25, 4, false, Bullet.BulletTypes.Large);
        FiringModeList.Add(Sprinkler);
        float[] quadShotAngles = new float[4];
        quadShotAngles[0] = 15;
        quadShotAngles[1] = 30;
        quadShotAngles[2] = - 15;
        quadShotAngles[3] = - 30;
        QuadShot = new FiringMode(quadShotAngles, 0.4f, 2f, 10, 0, 3, true, Bullet.BulletTypes.Small);
        FiringModeList.Add(QuadShot);
        float[] machineGunAngles = new float[1];
        MachineGun = new FiringMode(machineGunAngles, 0.05f, 5f, 20, 0, 1f, true, Bullet.BulletTypes.Small);
        FiringModeList.Add(MachineGun);
        float[] tearsAngles = new float[6];
        tearsAngles[0] = 60;
        tearsAngles[1] = 65;
        tearsAngles[2] = 70;
        tearsAngles[3] = 110;
        tearsAngles[4] = 115;
        tearsAngles[5] = 120;
        Tears = new FiringMode(tearsAngles, 0.6f, 6f, 0, 0, 5, false, Bullet.BulletTypes.Tear);
        SwitchFiringMode();
        // List of bullets, to despawn them later
        _bullets = new List<GameObject>();
    }

    private void Start()
    {
        // Add this to player's bullet spawner list
        Player.BulletSpawners.Add(gameObject.GetComponent<BulletSpawner>());
    }

    private void Update()
    {
        if(Attacking)
        {
            _firingTimer += Time.deltaTime;
            if (_firingTimer >= CurrentFiringMode.RateOfFire)
            {
                Shoot(CurrentFiringMode);
                _firingTimer = 0;
            }

            _switchModeTimer += Time.deltaTime;
            if (_switchModeTimer >= CurrentFiringMode.Duration)
            {
                SwitchFiringMode();
                _switchModeTimer = 0;
            }

            _cryingTimer += Time.deltaTime;
            if (Crying && _cryingTimer > Tears.RateOfFire)
            {
                _cryingTimer = 0;
                Shoot(Tears);
            }
        }
        
    }

    private void SwitchFiringMode()
    {
        CurrentFiringMode = FiringModeList[0];
        FiringModeList.Add(FiringModeList[0]);
        FiringModeList.RemoveAt(0);
        string firingModesInList = "";
        foreach (FiringMode mode in FiringModeList)
        {
            firingModesInList += mode.ToString() + " ";
        }
        //Debug.Log(firingModesInList);
    }
    
    // Fire a projectile.
    private void Shoot(FiringMode firingMode)
    {
        for (int x = 0; x < firingMode.FiringAngles.Length; x++)
        {
            // Instantiate bullet
            GameObject bullet = Instantiate(BulletObject, transform.position, Quaternion.identity);
            _bullets.Add(bullet);
            bullet.GetComponent<Bullet>().SetType(firingMode.BulletType);
            // Set angle and speed
            float angleRadians = firingMode.FiringAngles[x] * Mathf.PI / 180; // convert to radians
            // Get angle towards player
            Vector2 vectorTowardsPlayer = (Player.transform.position - transform.position);
            float angleTowardsPlayer = Mathf.Asin(vectorTowardsPlayer.y / vectorTowardsPlayer.magnitude);
            if(vectorTowardsPlayer.x < 0)
            {
                angleTowardsPlayer = Mathf.PI - angleTowardsPlayer;
            }
            // Shift angle accordingly
            if(firingMode.AimAtPlayer)
            {
                angleRadians += angleTowardsPlayer;
            }
            // Add variance
            angleRadians = angleRadians + Random.Range(-firingMode.FiringAngleVariance, firingMode.FiringAngleVariance) * Mathf.PI / 180;
            // Then convert to vector2 as done below
            Vector2 movementDirection = new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians));
            bullet.GetComponent<Rigidbody2D>().velocity = movementDirection * firingMode.BulletSpeed;

            // Change angle of firing
            firingMode.FiringAngles[x] += firingMode.ShiftAngles;   
        }

        // Audio
        if (firingMode.BulletType == Bullet.BulletTypes.Tear)
        {
            _tearsAudio.Play();
        }
        else
        {
            Debug.Log("bullet sound");
            //_bulletAudio.Play();
        }
    }

    public void DespawnBullets()
    {;
        foreach(GameObject bullet in _bullets)
        {
            Destroy(bullet);
        }
    }
}

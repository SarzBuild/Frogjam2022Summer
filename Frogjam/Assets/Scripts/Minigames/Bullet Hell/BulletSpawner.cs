using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public BulletHellCharacter Player;
    public GameObject Bullet;

    // Store data for different bullet patterns (aka firing modes)
    public FiringMode MachineGun;
    public FiringMode Sprinkler;
    public FiringMode QuadShot;

    // Current firing mode
    public FiringMode CurrentFiringMode;

    public List<FiringMode> FiringModeList;

    private float _firingTimer;
    private float _switchModeTimer;

    private void Awake()
    {
        // Define firing modes
        float[] machineGunAngles = new float[1];
        MachineGun = new FiringMode(machineGunAngles, 0.05f, 5f, 0, 1.5f);
        float[] sprinklerAngles = new float[1];
        sprinklerAngles[0] = Random.Range(0f, 360f);
        Sprinkler = new FiringMode(sprinklerAngles, 0.1f, 1f, 25, 6);
        float[] quadShotAngles = new float[4];
        quadShotAngles[0] = 15;
        quadShotAngles[1] = 30;
        quadShotAngles[2] = - 15;
        quadShotAngles[3] = - 30;
        QuadShot = new FiringMode(quadShotAngles, 0.4f, 2f, 0, 3);
        FiringModeList.Add(MachineGun);
        FiringModeList.Add(Sprinkler);
        FiringModeList.Add(QuadShot);
        SwitchFiringMode();
    }

    private void Update()
    {
        _firingTimer += Time.deltaTime;
        if(_firingTimer >= CurrentFiringMode.RateOfFire)
        {
            Shoot();
            _firingTimer = 0;
        }
        _switchModeTimer += Time.deltaTime;
        if(_switchModeTimer >= CurrentFiringMode.Duration)
        {
            SwitchFiringMode();
            _switchModeTimer = 0;
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
            firingModesInList += mode + " ";
        }
        Debug.Log(firingModesInList);
    }
    
    // Fire a projectile.
    // For the angle, 0 is towards the player, then it's counterclockwise
    private void Shoot()
    {
        for (int x = 0; x < CurrentFiringMode.FiringAngles.Length; x++)
        {
            // Instantiate bullet
            GameObject bullet = Instantiate(Bullet, transform.position, Quaternion.identity);
            // Set angle and speed
            float angleRadians = CurrentFiringMode.FiringAngles[x] * Mathf.PI / 180; // convert to radians
            // Get angle towards player
            Vector2 vectorTowardsPlayer = (Player.transform.position - transform.position);
            float angleTowardsPlayer = Mathf.Asin(vectorTowardsPlayer.y / vectorTowardsPlayer.magnitude);
            if(vectorTowardsPlayer.x < 0)
            {
                angleTowardsPlayer = Mathf.PI - angleTowardsPlayer;
            }
            // Shift angle accordingly
            angleRadians += angleTowardsPlayer;
            // Then convert to vector2 as done below
            Vector2 movementDirection = new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians));
            bullet.GetComponent<Rigidbody2D>().velocity = movementDirection * CurrentFiringMode.BulletSpeed;

            // Change angle of firing mdoe
            CurrentFiringMode.FiringAngles[x] += CurrentFiringMode.ShiftAngles;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public BulletHellCharacter Player;
    public GameObject Bullet;

    enum FiringModes 
    {
        MachineGun,
        QuadShot,
        Sprinkler
    }

    private FiringModes _currentFiringMode;
    
}

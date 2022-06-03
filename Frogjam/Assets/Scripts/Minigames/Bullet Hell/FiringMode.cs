using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringMode : MonoBehaviour
{
    public float RateOfFire;
    public float BulletSpeed;
    public float[] FiringAngles; // Angle of 0 is towards the player. Each entry = 1 shot, so an array of size 4 will shoot 4 times at once.
    public float ShiftAngles; // Used by the sprinkler to change the firing angle after each shot
    public float Duration;

    public FiringMode(float[] firingAngles, float rateOfFire, float bulletSpeed, float shiftAngles, float duration)
    {
        FiringAngles = firingAngles;
        RateOfFire = rateOfFire;
        BulletSpeed = bulletSpeed;
        ShiftAngles = shiftAngles;
        Duration = duration;
    }

}

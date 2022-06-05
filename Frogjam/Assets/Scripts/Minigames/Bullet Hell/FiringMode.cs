using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringMode : MonoBehaviour
{
    // An instance of this class exists for each firing mode and defines its behaviour.
    public float RateOfFire;
    public float BulletSpeed;
    public float[] FiringAngles; // Angle of 0 is towards the player, then angles increase counterclockwise. Each entry in the array = 1 shot, so an array of size 4 will shoot 4 bullets at once, at the listed angles.
    public float FiringAngleVariance; // The shot will deviate from its intended angle, +/- this many degrees. Used to create a spray effect
    public float ShiftAngles; // Used by the sprinkler to change the firing angle after each shot
    public float Duration;
    public bool AimAtPlayer;
    public bool ShootsTears;

    public FiringMode(float[] firingAngles, float rateOfFire, float bulletSpeed, float firingAngleVariance, float shiftAngles, float duration, bool aimAtPlayer, bool shootsTears)
    {
        FiringAngles = firingAngles;
        RateOfFire = rateOfFire;
        BulletSpeed = bulletSpeed;
        FiringAngleVariance = firingAngleVariance;
        ShiftAngles = shiftAngles;
        Duration = duration;
        AimAtPlayer = aimAtPlayer;
        ShootsTears = shootsTears;
    }

}

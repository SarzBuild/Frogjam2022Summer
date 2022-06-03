using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newProfile", menuName = "Data/Games/EndlessRunner/CharacterData")]
public class CharacterProfileData : ScriptableObject
{
    public float JumpHeight = 25;
    public float FallClamp = -10;
    public float MinFallSpeed = 50;
    public float MaxFallSpeed = 90;
    public float JumpApexThreshold = 10;
    public float JumpEndEarlyGravityModifier = 6;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The sole purpose of this script is to manage the phases of the bullet hell
// 1st phase: no gameplay, just dialogue
// 2nd phase: timer appears, combat begins
// 3rd phase: life counter appears
public class BulletHellPhases : MonoBehaviour
{
    public enum Phases
    {
        Dialogue,
        Timer,
        LifeCounter
    }

    public Phases CurrentPhase;

    private void Awake()
    {
        CurrentPhase = Phases.Dialogue;
    }

    private void Update()
    {
        // TODO: First phase lasts until a set dialogue event
        // At end of 1st phase, enable timer, then froggerina starts shooting

        // Second phase ends after 3 deaths
    }


}

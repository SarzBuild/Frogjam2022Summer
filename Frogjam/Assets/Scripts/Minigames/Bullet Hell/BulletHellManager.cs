using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The sole purpose of this script is to manage the phases of the bullet hell
// 1st phase: no gameplay, just dialogue
// 2nd phase: timer appears, combat begins
// 3rd phase: starts after the player has died 5 times. Life counter appears
public class BulletHellManager : MonoBehaviour
{
    [SerializeField] private GameObject _frogSkull;
    [SerializeField] private GameObject _lifeBar;
    [SerializeField] private TimerOfDoom _timerOfDoom;
    [SerializeField] private GameObject _lifeLimitUI;
    [SerializeField] private BulletSpawner _bulletSpawner;

    public bool DEBUG_STARTPHASE2;
    public bool DEBUG_STARTCOMBAT;

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
        
        // First phase lasts until a set dialogue event
        // At end of 1st phase, enable timer, then froggerina starts shooting
        if(DEBUG_STARTPHASE2)
        {
            StartPhase2();
        }
        if(DEBUG_STARTCOMBAT)
        {
            StartCombat();
        }
    }

    public void StartPhase2()
    {
        CurrentPhase = Phases.Timer;
        _frogSkull.SetActive(true);
        _lifeBar.SetActive(true);
        // TODO: call Froggerina dialogue event
        
    }

    public void StartCombat()
    {
        // TODO: call this event when Froggerina's done talking
        _timerOfDoom.TimerCountingDown = true;
        _bulletSpawner.Attacking = true;
    }

    public void StartPhase3()
    {
        _lifeLimitUI.SetActive(true);
    }

    public void EndBulletHell()
    {
        // I have no clue what needs to happen here
    }
}

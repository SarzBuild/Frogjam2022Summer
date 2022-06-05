using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

public class GymEventTest : MonoBehaviour
{
    public GameEventData GameEventData;
    public Player Player;

    private void Start()
    {
        if (Player == null)
        {
            Player = Player.Instance;
        }
    }
    
    private void Update()
    {
        if (Player.Settings.MouseLeftKey.PressedThisFrame())
        {
            GameEventData.Raise();
        }
    }

    public void EventRaisedFunction()
    {
        Debug.Log("The event has been raised!");
    }

    public void EventRaisedFunctionWithParam(string value)
    {
        Debug.Log(value);
    }
}

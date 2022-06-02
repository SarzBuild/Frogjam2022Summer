using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is just a wrapper script for Keys to easily changed them or create different profiles.
/// </summary>

[CreateAssetMenu(fileName = "newKey", menuName = "Data/Inputs/Key")]
public class KeyData : ScriptableObject
{
    [SerializeField] private KeyCode _key; //Private method as we don't want to change the key by code.
    [SerializeField] private float _keyDownTime; //The elapsed time from when an input change state from Lifted to Pressed;
    [SerializeField] private float _keyUpTime; //The elapsed time from when an input change state from Pressed to Lifted;

    public bool Lifted() => !Input.GetKey(_key);

    public bool Pressed() => Input.GetKey(_key);

    public bool PressedThisFrame()
    {
        _keyDownTime = Time.time;
        return Input.GetKeyDown(_key);  
    }

    public bool LiftedThisFrame()
    {
        _keyUpTime = Time.time;
        return Input.GetKeyUp(_key);
    }

    public float PressedTime()
    {
        if (!Input.GetKey(_key))
        {
            return 0;
        }

        return Time.time - _keyDownTime;
    }

    public float LiftedTime()
    {
        if (Input.GetKey(_key))
        {
            return 0;
        }

        return Time.time - _keyUpTime;
    }
}

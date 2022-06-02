using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// We can create different maps for different keyboards type around the world if we want.
/// </summary>

[CreateAssetMenu(fileName = "newKey", menuName = "Data/Inputs/KeyMap")]
public class KeyMapData : ScriptableObject
{
    public KeyData MouseRightKey;
    public KeyData MouseLeftKey;
    
    //TODO Gotta add more keys
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private uint[] seed = new uint[25];
    public string SeedSequence;
    
    private void Start()
    {
        GenerateMap();
    }
    
    private void GenerateMap()
    {
        for (int i = 0; i < seed.Length; i++)
        {
            seed[i] = (uint)Random.Range(0,10); //0 to 9
            SeedSequence += seed[i].ToString();
        }    
    }
}

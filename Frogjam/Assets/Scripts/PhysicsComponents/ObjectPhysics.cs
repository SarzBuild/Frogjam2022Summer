using System;
using System.Collections;
using System.Collections.Generic;
using PhysicsComponents;
using UnityEngine;
using static GeneralJTUtils.JTUtils;
using Random = UnityEngine.Random;

public class ObjectPhysics : PhysicsBase
{ 
    
    /*private void Awake()
    {
        if (_rigidbody2D == null)
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        _rigidbody2D.gravityScale = 0;
        
    }*/


    private void Start()
    {
        GenerateMap();
    }

    private uint[] seed = new uint[25];
    private void GenerateMap()
    {
        string sequence = "";
        for (int i = 0; i < seed.Length; i++)
        {
            seed[i] = (uint)Random.Range(0,10);
            sequence += seed[i].ToString();
        }    
    }
}
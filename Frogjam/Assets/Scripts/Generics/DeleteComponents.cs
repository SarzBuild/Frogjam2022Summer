using System;
using System.Collections;
using System.Collections.Generic;
using Generics;
using UnityEngine;

public class DeleteComponents : MonoBehaviour
{
    public MapGenerator MapGenerator;
    
    private void Start()
    {
        MapGenerator = MapGenerator.Instance;
    }

    private void Update()
    {
        if (MapGenerator.StateGame == MapGenerator.StateOfGame.Dead)
        {
            var t = GetComponent<SelfDestroyAfterTimeAmount>();
            var p = GetComponent<GenericDirectionalMovement>();
            Destroy(t);
            Destroy(p);
        }
    }
}

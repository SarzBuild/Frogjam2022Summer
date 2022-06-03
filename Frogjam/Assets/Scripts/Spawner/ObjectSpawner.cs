using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using GeneralJTUtils;
using PhysicsComponents;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject ObjectToSpawn { get { return _objectToSpawn ;} }
    public Player Player { get; private set; }
    
    
    [SerializeField] private GameObject _objectToSpawn;

    private void Start()
    {
        if (_objectToSpawn == null)
        {
            Assert.IsTrue(false, "There is no object to spawn on " + name);
        }
    }


    public virtual void SetOwner(Player player)
    {
        Assert.IsNotNull(player, "Player on ObjectSpawner " + name + " is null");
        Player = player;
        PostSetOwner();
    }

    protected virtual void PostSetOwner()
    {
        if (Player?.ObjectHoldPosition != null)
        {
            SetToOwner(Player.ObjectHoldPosition, CreateNewInstanceOfObject());
        }
    }

    protected virtual GameObject CreateNewInstanceOfObject()
    {
        return Instantiate(ObjectToSpawn);
    }

    public virtual void SetToOwner(Transform parent, GameObject newObject)
    {
        var t = newObject.GetComponent<ObjectPhysics>();

        var tempTransform = newObject.transform;
        tempTransform.SetParent(parent);
        tempTransform.localPosition = Vector3.zero; 
        tempTransform.localRotation = Quaternion.identity; 
        t.SetComponents(this);
        //tempTransform.localScale = new Vector3(1, 1, 1);
    }

    public virtual void RemoveFromOwner(GameObject gameObject)
    {
        gameObject.transform.SetParent(null);
    }

    public void SetGameObject(GameObject gameObject) => _objectToSpawn = gameObject;
    
    
}

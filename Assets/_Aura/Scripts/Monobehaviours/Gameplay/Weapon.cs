using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject ammoPrefab;

    static List<GameObject> ammoPool;
    public int poolSize;

    private void Awake()
    {
        if(ammoPool == null)
        {
            ammoPool = new List<GameObject>();
        }

        for(int i = 0; i<poolSize; i++)
        {
            GameObject ammoObject = Instantiate(ammoPrefab);
            ammoObject.SetActive(false);
            ammoPool.Add(ammoObject);   
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            FireAmmo();
        }
    }

    private void OnDestroy()
    {
        ammoPool = null;
    }
    private GameObject SpawnAmmo(Vector3 _location)
    {
        //loop through every object in pool
        //find the first inactive one
        //activate it and set its location
        //then return it
        foreach (var ammo in ammoPool)//ToDo:will need a refactor to allow addition of new objects to a pool
        {
            if(ammo.activeSelf == false)
            {
                ammo.SetActive(true);

                ammo.transform.position = _location;

                return ammo;
            }
        }
        return null;
    }
    private void FireAmmo()
    {
        throw new NotImplementedException();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Framework for a generic SpawnPoint
/// that will spawner whatever Gameobject we
/// have it reference
/// </summary>
public class SpawnPoint : MonoBehaviour
{
    //the GameObject to be spawned
    public GameObject prefabToSpawn;
    public float repeatInterval;
    public bool isPlayer;

    private void OnEnable()
    {
        if(isPlayer == true)
        {
           RPGGameManager.Instance.OnSetUpPlayerSystem += SpawnObject;
        }
    }
    private void Start()
    {
        if(repeatInterval > 0)
        {
            InvokeRepeating("SpawnObject",0f,repeatInterval);
        }
    }

    private GameObject SpawnObject()
    {
        if(prefabToSpawn != null)
        {
          return  Instantiate(prefabToSpawn,transform.position, Quaternion.identity); 
        }
        return null;
    }

}

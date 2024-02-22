using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGGameManager : MonoBehaviour
{
    public event Func<GameObject> OnSetUpPlayerSystem;//Only the PlayerSpawnPoint will be interested in this Func delegate
    public event Action OnSetUpGameSystems;//the other components in the game world who don't need to return anything to the manager
    private CinemachineVirtualCamera playerVCam;
    //Singleton SetUp
    private static RPGGameManager m_instance;
    public static RPGGameManager Instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindAnyObjectByType<RPGGameManager>();

                if(m_instance == null)
                {
                    m_instance = new GameObject().AddComponent<RPGGameManager>();
                }
            }
            return m_instance;
        }
    }

    private void Awake()
    {
        if(m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        playerVCam = GameObject.FindGameObjectWithTag("PlayerVCam").GetComponent<CinemachineVirtualCamera>();
    }



    private GameObject instantiatedPlayer;//cache the instantiate player
    void Start()
    {
        SetUpPlayerSystem();
    }

    private void SetUpPlayerSystem()
    {
      instantiatedPlayer = OnSetUpPlayerSystem?.Invoke();
      playerVCam.Follow = instantiatedPlayer.transform;
      
    }
}

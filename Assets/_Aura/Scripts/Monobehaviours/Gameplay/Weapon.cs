using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject ammoPrefab;
    public float weaponVelocity;
    public int poolSize;
    [HideInInspector] public Animator animator;


    bool isFiring;
    Camera localCamera;
    float positiveSlope;
    float negativeSlope;

    static List<GameObject> ammoPool;

    #region Monobehaviour Callbacks
    private void Awake()
    {
        if (ammoPool == null)
        {
            ammoPool = new List<GameObject>();
        }

        for (int i = 0; i < poolSize; i++)
        {
            GameObject ammoObject = Instantiate(ammoPrefab);
            ammoObject.SetActive(false);
            ammoPool.Add(ammoObject);
        }
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        isFiring = false;
        localCamera = Camera.main;

        Vector2 lowerLeft = localCamera.ScreenToWorldPoint(Vector2.zero);
        Vector2 upperRight = localCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 upperLeft = localCamera.ScreenToWorldPoint(new Vector2(0, Screen.height));
        Vector2 lowerRight = localCamera.ScreenToWorldPoint(new Vector2(Screen.width, 0));

        positiveSlope = GetSlope(lowerLeft, upperRight);
        negativeSlope = GetSlope(upperLeft, lowerRight);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isFiring = true;
            FireAmmo();
        }
        UpdateState();
    }

    private void OnDestroy()
    {
        ammoPool = null;
    } 
    #endregion
    #region Fire Direction Utility
    float GetSlope(Vector2 _pointOne, Vector2 _pointTwo)
    {
        return (_pointTwo.y - _pointOne.y) / (_pointTwo.x - _pointOne.x);
    }
    bool HigherThanPositiveSlopeLine(Vector2 _inputPosition)
    {
        Vector2 playerPosition = gameObject.transform.position;
        Vector2 mousePosition = localCamera.ScreenToWorldPoint(_inputPosition);

        float yIntercept = playerPosition.y - (positiveSlope * playerPosition.x);
        float inputIntercept = mousePosition.y - (positiveSlope * mousePosition.x);

        return inputIntercept > yIntercept;
    }

    bool HigherThanNegativeSlopeLine(Vector2 _inputPosition)
    {
        Vector2 playerPosition = gameObject.transform.position;
        Vector2 mousePosition = localCamera.ScreenToWorldPoint(_inputPosition);

        float yIntercept = playerPosition.y - (negativeSlope * playerPosition.x);
        float inputIntercept = mousePosition.y - (negativeSlope * mousePosition.x);

        return inputIntercept > yIntercept;
    }

    Quadrant GetQuadrant()
    {
        bool higherThanPosSlopeLine = HigherThanPositiveSlopeLine(Input.mousePosition);
        bool higherThanNegSlopeLine = HigherThanNegativeSlopeLine(Input.mousePosition);

        if (!higherThanPosSlopeLine && higherThanNegSlopeLine)
        {
            return Quadrant.East;
        }
        else if (!higherThanPosSlopeLine && !higherThanNegSlopeLine)
        {
            return Quadrant.South;
        }
        else if (higherThanPosSlopeLine && !higherThanNegSlopeLine)
        {
            return Quadrant.West;
        }
        else
        {
            return Quadrant.North;
        }
    } 
    #endregion
    #region Firing Utility

    private void UpdateState()
    {
        if (isFiring)
        {
            Vector2 quadrantVector;
            Quadrant quadEnum = GetQuadrant();

            switch (quadEnum)
            {
                case Quadrant.East:
                    quadrantVector = new Vector2(1f, 0f);
                    break;
                case Quadrant.South:
                    quadrantVector = new Vector2(0f, -1f);
                    break;
                case Quadrant.West:
                    quadrantVector = new Vector2(-1f, 0f);
                    break;
                case Quadrant.North:
                    quadrantVector = new Vector2(0f, 1f);
                    break;
                default:
                    quadrantVector = new Vector2(0f, 0f);
                    break;
            }
            animator.SetBool("isFiring", true);
            animator.SetFloat("fireXDir", quadrantVector.x);
            animator.SetFloat("fireYDir", quadrantVector.y);

            isFiring = false;
        }
        else
        {
            animator.SetBool("isFiring", false);
        }
    }
    private GameObject SpawnAmmo(Vector3 _location)
    {
        //loop through every object in pool
        //find the first inactive one
        //activate it and set its location
        //then return it
        foreach (var ammo in ammoPool)//ToDo:will need a refactor to allow addition of new objects to a pool
        {
            if (ammo.activeSelf == false)
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
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject ammo = SpawnAmmo(transform.position);

        if (ammo != null)
        {
            Arc arcScript = ammo.GetComponent<Arc>();
            float travelDuration = 1.0f / weaponVelocity;

            var zeroedOutMousePos = new Vector3(mousePosition.x, mousePosition.y, 0f);
            StartCoroutine(arcScript.TravelArc(zeroedOutMousePos, travelDuration));
        }
    } 
    #endregion
}
public enum Quadrant
{
    East,
    South,
    West,
    North
}

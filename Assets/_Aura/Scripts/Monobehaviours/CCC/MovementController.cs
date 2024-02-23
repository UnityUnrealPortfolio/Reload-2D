using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float m_moveSpeed;

    private Animator m_animator;
    private Rigidbody2D m_rigidBody;
    private Vector2 m_moveInput= Vector2.zero;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
        UpdateStates();
    }

    private void UpdateStates()
    {
      if(Mathf.Approximately(m_moveInput.x, 0)&&
            Mathf.Approximately(m_moveInput.y, 0))
        {
            m_animator.SetBool("isWalking", false);
        }
        else
        {
            m_animator.SetBool("isWalking", true);
        }

        m_animator.SetFloat("xDir", m_moveInput.x);
        m_animator.SetFloat("yDir",m_moveInput.y);
    }

    private void MoveCharacter()
    {
        //get input from keyboard - will refactor out later
        float sidewaysInput = Input.GetAxisRaw("Horizontal");
        float upDownInput = Input.GetAxisRaw("Vertical");

        m_moveInput.x = sidewaysInput;
        m_moveInput.y = upDownInput;

        m_moveInput.Normalize();

        m_rigidBody.velocity = m_moveInput * m_moveSpeed;
    }
}
public enum CharStates
{
    WalkEast = 1,
    WalkSouth = 2,
    WalkWest = 3,
    WalkNorth = 4,
    IdleSouth = 5
}//ToDo:Remove as we have refactored the animation logic to a blend tree

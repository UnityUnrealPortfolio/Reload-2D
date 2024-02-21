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
       //transition animations based on movement velocity
      if(m_rigidBody.velocity.x > 0)
        {
            m_animator.SetInteger("AnimationState", (int)CharStates.WalkEast);
        }
      else if(m_rigidBody.velocity.x < 0)
        {
            m_animator.SetInteger("AnimationState",(int)CharStates.WalkWest);
        }
      else if(m_rigidBody.velocity.y > 0)
        {
            m_animator.SetInteger("AnimationState",(int)CharStates.WalkNorth);
        }
      else if(m_rigidBody.velocity.y < 0)
        {
            m_animator.SetInteger("AnimationState", (int)CharStates.WalkSouth);
        }
        else
        {
            m_animator.SetInteger("AnimationState", (int)CharStates.IdleSouth);
        }
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
}

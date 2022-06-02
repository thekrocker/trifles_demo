using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    protected PlayerStateMachine PlayerSm;


    private void Awake()
    {
        PlayerSm = GetComponent<PlayerStateMachine>();
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void DoSwipeTransition()
    {
    }

    public virtual Vector3 SetMovement() => Vector3.zero;
}

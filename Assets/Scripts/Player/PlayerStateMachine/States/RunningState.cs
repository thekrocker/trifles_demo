using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : BaseState
{
    

    public override void Enter()
    {
        base.Enter();
    }

    public override void DoSwipeTransition()
    {
        if (InputManager.Instance.SwipeLeft) PlayerSm.ChangeLane(-1); // Lane swap, go left.
        if (InputManager.Instance.SwipeRight) PlayerSm.ChangeLane(1); // Lane swap, go right.
    }
    
    public override Vector3 SetMovement()
    {
        Vector3 moveVector = Vector3.zero;

        moveVector.x = PlayerSm.GetSnapLaneValue();
        moveVector.y = -1.0f; //change this later.
        moveVector.z = PlayerSm.moveSpeed;
        
        return moveVector;
    }
    
    public override void Exit()
    {
        base.Exit();
    }
}

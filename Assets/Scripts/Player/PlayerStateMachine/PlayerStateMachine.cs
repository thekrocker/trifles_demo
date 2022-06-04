using System;
using Player.PlayerStateMachine;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerStats playerStats;
    
    public CharacterController Controller { get; set; }
    public Rigidbody Rb { get; set; }

    public Vector3 MoveVector { get; set; }
    public int CurrentLane { get; set; } // will hold -1 , 0 , 1 values for snapping
    public bool IsGrounded { get; set; }

    public float distanceBetweenLanes = 3f;
    public float swipeSpeed;


    private BaseState _currentState;


    private void Start()
    {
        Rb = GetComponent<Rigidbody>();

        SetInitialState();
    }

    private void SetInitialState()
    {
        _currentState = GetComponent<RunningState>();
        _currentState.Enter();
    }

    private void Update()
    {
        Tick();
    }
    
    private void Tick()
    {
        MoveVector = _currentState.SetMovement();
        _currentState.DoSwipeTransition();
    }


    private void FixedUpdate()
    {
        Rb.MovePosition(transform.position + MoveVector * Time.fixedDeltaTime);
    }
    

    public void ChangeLane(int direction)
    {
        CurrentLane = Mathf.Clamp(CurrentLane + direction, -1, 1);
    }

    public float GetSnapLaneValue()
    {
        float snapeLaneValue;

        if (Math.Abs(transform.position.x - (CurrentLane * distanceBetweenLanes)) >
            Mathf.Epsilon) // if we are not in same lane 
        {
            float desiredPosDelta = (CurrentLane * distanceBetweenLanes) - transform.position.x;
            snapeLaneValue = (desiredPosDelta > 0) ? 1 : -1;
            snapeLaneValue *= swipeSpeed;

            float
                distance = snapeLaneValue *
                           Time.fixedDeltaTime; // 0.01f per frame // 0.01 - 0.005 would cause issues so we divided 1 / 0.001f and got 1000 which we multiply with 0.005 ends up with 5f.. it calculates the exact distance.

            if (Mathf.Abs(distance) > Mathf.Abs(desiredPosDelta)) // if we moved so much..
            {
                snapeLaneValue = desiredPosDelta * (1f / Time.fixedDeltaTime);
            }
        }
        else
        {
            snapeLaneValue = 0f;
        }

        return snapeLaneValue;
    }

    public void ChangeState(BaseState nextState)
    {
        _currentState.Exit();
        Debug.Log($"Previous state: {_currentState}, Current state: {nextState}");
        _currentState = nextState;
        _currentState.Enter();
    }
}
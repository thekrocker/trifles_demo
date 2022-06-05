using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Interfaces;
using Player.PlayerStateMachine;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform playerModel;
    [SerializeField] private PlayerStats stats;

    [SerializeField] private float moveVerticalDuration = .2f;

    public bool IsPlayerInArea { get; private set; }

    private float _initialSpeed;
    private float _boostDuration;

    private void Awake()
    {
        stats.moveSpeed = stats.initialSpeed;
    }


    private void OnTriggerEnter(Collider other)
    {
        SetInteractions(other);
    }

    private void SetInteractions(Collider other)
    {
        var istackable = other.GetComponent<IStackable>();
        var iGate = other.GetComponent<IGate>();
        var iBoost = other.GetComponent<IBoost>();
        var iObstacle = other.GetComponent<IObstacle>();

        istackable?.Stack(playerModel);
        iGate?.UseGatePower();
        iBoost?.Boost(transform);
        iBoost?.Boost(stats);

        if (iObstacle != null)
        {
            IsPlayerInArea = true;
            iObstacle?.Interact();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var iObstacle = other.GetComponent<IObstacle>();

        if (iObstacle != null) IsPlayerInArea = false;
    }


    public void SetPlayerPosition(int count, Action act)
    {
        playerModel.DOMoveY(count, moveVerticalDuration).SetEase(Ease.Linear).OnComplete(() => { act?.Invoke(); });
    }
}
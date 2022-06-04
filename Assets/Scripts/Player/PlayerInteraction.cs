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

    private void OnTriggerEnter(Collider other)
    {
        var istackable = other.GetComponent<IStackable>();
        var iGate = other.GetComponent<IGate>();
        var iBoost = other.GetComponent<IBoost>();

        if (istackable != null)
        {
            istackable.Stack(playerModel);
            SetPlayerPosition();
        }

        iGate?.UseGatePower();
        
        iBoost?.Boost(transform);
    }

    public void SetSpeed(float boostSpeed)
    {
        stats.moveSpeed = boostSpeed;
    }

    private void SetPlayerPosition()
    {
        playerModel.DOMoveY(StackSystem.Instance.CurrentCubeStacks.Count, 0.3f).SetEase(Ease.Linear);
    }
}
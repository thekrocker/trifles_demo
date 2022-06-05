using System;
using System.Collections;
using System.Collections.Generic;
using Player.PlayerStateMachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Player
{
    public class PlayerBoosts : MonoBehaviour
    {
        [SerializeField] private PlayerStats stats;

        [Title("Speed")] [SerializeField] private float boostSpeed = 30f;
        [SerializeField] private float boostDuration = 3f;

        private void OnEnable() => EventManager.OnCombo += SetFeverMode;
        private void OnDisable() => EventManager.OnCombo -= SetFeverMode;

        private Coroutine _boostRorutine;

        private void SetFeverMode()
        {
            if (_boostRorutine == null) _boostRorutine = StartCoroutine(CO_SetFeverMode()); // For overlaps multiple times, I block to get a new move boost while there is already active one.. 
            

            IEnumerator CO_SetFeverMode()
            {
                EventManager.OnComboActivated?.Invoke(true);
                GameManager.Instance.FeverModeActive = true;
                stats.moveSpeed = boostSpeed;
                
                yield return new WaitForSeconds(boostDuration);
                
                stats.moveSpeed = stats.initialSpeed;
                GameManager.Instance.FeverModeActive = false;
                EventManager.OnComboActivated?.Invoke(false);
                _boostRorutine = null;
            }
        }
    }
}
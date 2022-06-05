using System;
using System.Collections;
using Player.PlayerStateMachine;
using UnityEngine;

namespace Interfaces
{
    public class SpeedBoost : MonoBehaviour, IBoost
    {

        [SerializeField] private float boostSpeed = 30f;
        [SerializeField] private float boostDuration = 3f;

        private Coroutine _boostRorutine;
        
        public void Boost(PlayerStats pStats)
        {
            if (_boostRorutine == null) // For overlaps multiple times, I block to get a new move boost while there is already active one.. 
            {
                _boostRorutine = StartCoroutine(CO_SetSpeed());
            }

            IEnumerator CO_SetSpeed()
            {
                pStats.moveSpeed = boostSpeed;
                yield return new WaitForSeconds(boostDuration);
                pStats.moveSpeed = pStats.initialSpeed;

                _boostRorutine = null;
            }
        }
    }
}
using System;
using DG.Tweening;
using Player.PlayerStateMachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Interfaces
{
    public class JumpBoost : MonoBehaviour, IBoost
    {
        [SerializeField] private Transform currrentArea;
        [SerializeField] private Transform targetArea;
        
        [Title("Jump Properties")]
        [SerializeField] private float offset = 15f;
        [SerializeField] private float jumpPower = 15f;
        [SerializeField] private int numJumps = 1;
        [SerializeField] private float duration = 1f;

        

        private void Awake()
        {
            CalculateDistance();
        }

        private float CalculateDistance() => Vector3.Distance(currrentArea.position, targetArea.position) + offset;

        public void Boost(Transform playerTransform)
        {
            playerTransform
                .DOJump(playerTransform.position + (playerTransform.forward * CalculateDistance()), jumpPower, numJumps,
                    duration).SetEase(Ease.Linear);
        }
    }
}
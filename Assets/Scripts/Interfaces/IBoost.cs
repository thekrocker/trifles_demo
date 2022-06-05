using DG.Tweening;
using Player.PlayerStateMachine;
using UnityEngine;

namespace Interfaces
{
    public interface IBoost
    {
        public virtual void Boost(Transform playerTransform)
        {
        }

        public virtual void Boost(PlayerStats pStats)
        {
        }


    }
}
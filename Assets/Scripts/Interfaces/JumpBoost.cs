using DG.Tweening;
using UnityEngine;

namespace Interfaces
{
    public class JumpBoost : MonoBehaviour, IBoost
    {
        public void Boost(Transform playerTransform)
        {
            Debug.Log(playerTransform.forward);
            playerTransform.DOJump(playerTransform.position + playerTransform.forward * 30f, 5f, 1, 2f).SetEase(Ease.Linear);
        }
    }
}
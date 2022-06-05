using UnityEngine;

namespace Player.PlayerStateMachine
{
    [CreateAssetMenu(menuName = "Player/PlayerStats")]
    public class PlayerStats : ScriptableObject
    {
        public float initialSpeed = 15f;
        public float moveSpeed;
    }
}
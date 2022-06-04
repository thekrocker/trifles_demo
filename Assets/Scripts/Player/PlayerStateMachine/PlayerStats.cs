using UnityEngine;

namespace Player.PlayerStateMachine
{
    [CreateAssetMenu(menuName = "Player/PlayerStats")]
    public class PlayerStats : ScriptableObject
    {
        public float moveSpeed;
    }
}
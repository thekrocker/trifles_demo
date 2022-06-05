using UnityEngine;

namespace Game_Props
{
    public class RegularObstacle : BaseObstacle
    {
        [field: SerializeField] public int Cost { get; set; }

        public override void Interact()
        {
            if (GameManager.Instance.FeverModeActive)
            {
                transform.Scale(Vector3.zero, .5f, () => gameObject.SetActive(false));
                return;
            }
            
            StackSystem.Instance.HandleRegularObstacle(Cost);
            OnTrigger?.Invoke();
        }
    }
}
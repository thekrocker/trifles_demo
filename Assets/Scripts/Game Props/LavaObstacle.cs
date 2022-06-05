using UnityEngine;

namespace Game_Props
{
    public class LavaObstacle : BaseObstacle
    {
        public override void Interact()
        {
            if (GameManager.Instance.FeverModeActive)
            {
                transform.Scale(Vector3.zero, .5f, () => gameObject.SetActive(false));
                return;
            }
            
            StackSystem.Instance.HandleLavaObstacle();
            
        }
        
        
    }
}
using UnityEngine;

public class MatchSystem : MonoBehaviour
{
    public static MatchSystem Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    
    
}
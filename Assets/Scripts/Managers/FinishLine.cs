using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Player.PlayerStateMachine;
using UnityEngine;

public class FinishLine : MonoBehaviour
{

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerStateMachine sm))
        {
            EventManager.OnWin?.Invoke();
            Debug.Log("Finish game..");
        }
    }
}

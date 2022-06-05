using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool FeverModeActive { get; set; }


    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
}
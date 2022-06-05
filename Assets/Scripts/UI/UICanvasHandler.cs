using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvasHandler : MonoBehaviour
{
    [SerializeField] private GameObject feverTextObj;
    [SerializeField] private GameObject gameOverPanel;
    
    private void OnEnable()
    {
        EventManager.OnComboActivated += HandleText;
        EventManager.OnWin += HandleGameOver;
        EventManager.OnDie += HandleGameOver;
    }

    private void OnDisable()
    {
        EventManager.OnComboActivated -= HandleText;
        EventManager.OnWin -= HandleGameOver;
        EventManager.OnDie -= HandleGameOver;


    }

    private void HandleText(bool status)
    {
        feverTextObj.SetActive(status);
    }

    private void HandleGameOver()
    {
        gameOverPanel.SetActive(true);
    }

}

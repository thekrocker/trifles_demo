using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    private Button _restartButton;
    void Start()
    {
        _restartButton = GetComponent<Button>();

        SetRestartButton();
    }

    private void SetRestartButton()
    {
        _restartButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }
}

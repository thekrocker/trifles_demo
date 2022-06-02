using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;


    #region PublicProperties // Public properties

    public Vector2 TouchPosition { get; set; }
    public bool Tap { get; private set; }

    #endregion


    #region Privates

    private Vector2 _startDrag;
    private bool _swipeLeft;
    private bool _swipeRight;

    #endregion


    private PlayerInputAction _playerInputAction;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        DontDestroyOnLoad(gameObject);

        SetInput();
    }

    private void OnEnable() => _playerInputAction.Enable();
    private void OnDisable() => _playerInputAction.Disable();

    private void LateUpdate()
    {
        throw new NotImplementedException();
    }

    private void ResetInputs()
    {
        Tap = false;
        _swipeLeft = false;
        _swipeRight = false;
    }

    private void SetInput()
    {
        _playerInputAction = new PlayerInputAction();
        // Register here.

        _playerInputAction.Gameplay.Tap.performed += context => OnTap(context);
        _playerInputAction.Gameplay.TouchPosition.performed += context => OnPosition(context);
        _playerInputAction.Gameplay.StartDrag.performed += context => OnStartDrag(context);
        _playerInputAction.Gameplay.EndDrag.performed += context => OnEndDrag(context);
    }

    private void OnTap(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    private void OnStartDrag(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    private void OnEndDrag(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    private void OnPosition(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }
}
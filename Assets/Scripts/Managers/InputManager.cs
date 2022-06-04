using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [SerializeField] private float swipeDeadzone = 50f;
    

    #region Public Properties // Public properties
    public bool SwipeLeft { get; private set; }
    public bool SwipeRight { get; private set; }
    public Vector2 TouchPosition { get; set; }
    public bool Tap { get; private set; }

    #endregion


    #region Privates

    private Vector2 _startDrag;

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
        ResetInputs();
    }

    private void ResetInputs()
    {
        Tap = SwipeLeft = SwipeRight = false;
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
        Tap = true;
    }

    private void OnStartDrag(InputAction.CallbackContext context)
    {
        _startDrag = TouchPosition;
    }

    private void OnEndDrag(InputAction.CallbackContext context)
    {
        Vector2 delta = TouchPosition - _startDrag;
        float sqrDistance = delta.sqrMagnitude;

        if (sqrDistance > swipeDeadzone) // swiped enough
        {
            float x = Mathf.Abs(delta.x);
            float y = Mathf.Abs(delta.y); // might delete this later

            if (x > y) // left or right
            {
                if (delta.x > 0)
                {
                    SwipeRight = true;
                }
                else
                {
                    SwipeLeft = true;
                }
            }
        }

        _startDrag = Vector2.zero; // Reset drag value
    }

    private void OnPosition(InputAction.CallbackContext context)
    {
        TouchPosition = context.ReadValue<Vector2>();
    }
}
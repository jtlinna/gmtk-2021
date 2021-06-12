using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    private const float GRAVITY_ACCELERATION = 9.81f;

    private bool _characterActive = true;

    private Vector2 _movementDirection = Vector2.zero;
    private float _verticalMovementVelocity = 0f;

    [SerializeField]
    private CinemachineVirtualCamera _vCam = null;
    [SerializeField]
    private CharacterController _characterController = null;
    [SerializeField]
    private Transform _graphicsTransform = null;
    [SerializeField]
    private float _gravityMultiplier = 5f;
    [SerializeField]
    private float _movementSpeed = 10f;
    [SerializeField]
    private float _jumpPower = 30f;

    public void OnValidate()
    {
        if(_characterController == null)
        {
            _characterController = GetComponent<CharacterController>();
        }

        if(_vCam == null)
        {
            _vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        }
    }

    // Start is called before the first frame update
    public void Start()
    {
        InputManager.ControlScheme.Yeet.MovementX.performed += OnMovementX;
        InputManager.ControlScheme.Yeet.MovementX.canceled += OnMovementX;

        InputManager.ControlScheme.Yeet.MovementY.performed += OnMovementY;
        InputManager.ControlScheme.Yeet.MovementY.canceled += OnMovementY;

        InputManager.ControlScheme.Yeet.Action.performed += OnAction;
    }

    public void OnDestroy()
    {
        InputManager.ControlScheme.Yeet.MovementX.performed -= OnMovementX;
        InputManager.ControlScheme.Yeet.MovementX.canceled -= OnMovementX;

        InputManager.ControlScheme.Yeet.MovementY.performed -= OnMovementY;
        InputManager.ControlScheme.Yeet.MovementY.canceled -= OnMovementY;

        InputManager.ControlScheme.Yeet.Action.performed -= OnAction;
    }

    // Update is called once per frame
    public void Update() { }

    public void FixedUpdate()
    {
        _verticalMovementVelocity = ApplyGravity(_verticalMovementVelocity);

        Vector2 playerInputDirection = Vector2.zero;
        if(_characterActive && _movementDirection != Vector2.zero)
        {
            playerInputDirection = _movementDirection.normalized * _movementSpeed;
            TurnCharacter(-Vector2.SignedAngle(Vector2.up, playerInputDirection));
        }

        Vector3 frameMovement = new Vector3(playerInputDirection.x, _verticalMovementVelocity, playerInputDirection.y) * Time.fixedDeltaTime;

        _characterController.Move(frameMovement);
    }

    private float ApplyGravity(float currentVerticalVelocity)
    {
        if(currentVerticalVelocity <= 0 && _characterController.isGrounded)
        {
            return -1f;
        }

        float adjustedVerticalVelocity = currentVerticalVelocity - (GRAVITY_ACCELERATION * _gravityMultiplier * Time.fixedDeltaTime);
        return adjustedVerticalVelocity;
    }

    private void TurnCharacter(float angle)
    {
        _graphicsTransform.localRotation = Quaternion.Euler(0, angle, 0);
    }

    public void SetEnabled(bool f)
    {
        _vCam.gameObject.SetActive(f);
        _characterActive = f;
    }

    public void KillCharacter() { }

    private void OnAction(InputAction.CallbackContext obj)
    {
        if(!_characterActive)
        {
            return;
        }

        // TODO: Add check for power-up
        if(_characterController.isGrounded)
        {
            _verticalMovementVelocity = _jumpPower;
        }
    }

    private void OnMovementX(InputAction.CallbackContext obj) => _movementDirection.x = obj.ReadValue<float>();
    private void OnMovementY(InputAction.CallbackContext obj) => _movementDirection.y = obj.ReadValue<float>();
}

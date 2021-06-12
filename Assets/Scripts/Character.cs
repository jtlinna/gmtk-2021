using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    private const float GRAVITY_ACCELERATION = 9.81f;

    private readonly RaycastHit[] _raycastHelper = new RaycastHit[5];

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
    private float _pushForce = 3f;
    [SerializeField]
    private float _pushDistance = 0.75f;
    [SerializeField]
    private float _jumpPower = 30f;

    private float _currentMovementSpeed = 0f;

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
        _currentMovementSpeed = _movementSpeed;

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
        RecoverMovementSpeed();
        HandlePushing();
        HandleMovement();
    }

    private void HandleMovement()
    {
        _verticalMovementVelocity = ApplyGravity(_verticalMovementVelocity);

        Vector2 playerInputDirection = Vector2.zero;
        if(_characterActive && _movementDirection != Vector2.zero)
        {
            playerInputDirection = _movementDirection.normalized * _currentMovementSpeed;
            TurnCharacter(-Vector2.SignedAngle(Vector2.up, playerInputDirection));
        }

        Vector3 frameMovement = new Vector3(playerInputDirection.x, _verticalMovementVelocity, playerInputDirection.y) * Time.fixedDeltaTime;

        _characterController.Move(frameMovement);
    }

    private void HandlePushing()
    {
        if(!GameManager.Instance.IsPowerUpActive(PowerUpIdentifier.PushBlocks))
        {
            return;
        }

        int hits = Physics.RaycastNonAlloc(transform.position + _characterController.center, _graphicsTransform.forward, _raycastHelper, _pushDistance, int.MaxValue, QueryTriggerInteraction.Ignore);

        for(int i = 0; i < hits; i++)
        {
            RaycastHit hit = _raycastHelper[i];

            if(hit.collider.GetComponentInChildren<PushableCollider>() is PushableCollider pushableCollider)
            {
                pushableCollider.RigidbodyProxy.AddForce(-hit.normal.normalized * _pushForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
                _currentMovementSpeed = _movementSpeed * 0.4f;
                return;
            }
        }
    }

    private void RecoverMovementSpeed()
    {
        if(_currentMovementSpeed >= _movementSpeed)
        {
            return;
        }

        _currentMovementSpeed += _movementSpeed * Time.fixedDeltaTime;

        if(_currentMovementSpeed >= _movementSpeed)
        {
            _currentMovementSpeed = _movementSpeed;
        }
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

    public void Kill() { }

    public void LevelCompleted() { }

    private void OnAction(InputAction.CallbackContext obj)
    {
        if(!_characterActive)
        {
            return;
        }

        if(_characterController.isGrounded && GameManager.Instance.IsPowerUpActive(PowerUpIdentifier.Jump))
        {
            _verticalMovementVelocity = _jumpPower;
        }
    }

    private void OnMovementX(InputAction.CallbackContext obj) => _movementDirection.x = obj.ReadValue<float>();
    private void OnMovementY(InputAction.CallbackContext obj) => _movementDirection.y = obj.ReadValue<float>();

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position + _characterController.center, _graphicsTransform.forward * _pushDistance);
    }
}

using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    private const float GRAVITY_ACCELERATION = 9.81f;

    private readonly RaycastHit[] _raycastHelper = new RaycastHit[5];

    private bool _characterActive = true;
    private bool _blockUpdate = false;

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
    [SerializeField]
    private float _jumpAllowedAfterAirTimeThreshold = 0.2f;
    [SerializeField]
    private float _yeetForce = 420f;
    [SerializeField]
    private AudioSource _pushAudioSource;
    [SerializeField]
    private float _pushAudioStopDelay = 0.25f;
    [SerializeField]
    private float _landingAudioDelay = 0.25f;
    [SerializeField]
    private float _footStepInterval = 0.2f;
    [SerializeField]
    private AudioSource _oneShotAudioSource;
    [SerializeField]
    private AudioClip _jumpAudio;
    [SerializeField]
    private AudioClip _landingAudio;
    [SerializeField]
    private AudioClip _dashAudio;
    [SerializeField]
    private AudioSource _footStepAudioSource;
    [SerializeField]
    private ParticleSystem[] _jumpParticles = new ParticleSystem[0];

    [SerializeField]
    private IkHelpers _ikHelper = null;
    [SerializeField]
    private Transform _tetherTarget = null;

    public Transform GetTetherTransform => _tetherTarget;

    private float _currentMovementSpeed = 0f;
    private float _lastGroundedAt;
    private float _stopPushAudioAt = 0f;
    private float _landingAudioAllowedAt;

    private bool _hasJumped = false;
    private float _footStepTimer = 0f;

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
    public void Update()
    {
        if(Time.time >= _stopPushAudioAt)
        {
            AudioUtils.Stop(_pushAudioSource);
        }

        HandleFootSteps();
    }

    public void FixedUpdate()
    {
        RecoverMovementSpeed();
        if(_hasJumped && _characterController.isGrounded && Time.time >= _landingAudioAllowedAt)
        {
            AudioUtils.PlayOneShot(_oneShotAudioSource, _landingAudio);
            _hasJumped = false;
        }

        if(_blockUpdate)
        {
            return;
        }

        HandlePushing();
        HandleMovement();

        if(_characterController.isGrounded)
        {
            _lastGroundedAt = Time.time;
        }
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
            _stopPushAudioAt = Time.time + _pushAudioStopDelay;
            return;
        }

        PushableCollider pushTarget = GetPushTarget(out RaycastHit hit);
        if(pushTarget != null)
        {
            pushTarget.RigidbodyProxy.AddForce(-hit.normal.normalized * _pushForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
            _currentMovementSpeed = _movementSpeed * 0.4f;
            AudioUtils.Play(_pushAudioSource);
        }
        else
        {
            _stopPushAudioAt = Time.time + _pushAudioStopDelay;
        }
    }

    private PushableCollider GetPushTarget(out RaycastHit hit)
    {
        int hits = Physics.RaycastNonAlloc(transform.position + (_characterController.center * 0.5f), _graphicsTransform.forward, _raycastHelper, _pushDistance, int.MaxValue, QueryTriggerInteraction.Ignore);

        for(int i = 0; i < hits; i++)
        {
            hit = _raycastHelper[i];

            if(hit.collider.GetComponentInChildren<PushableCollider>() is PushableCollider pushableCollider)
            {
                return pushableCollider;
            }
        }

        hit = new RaycastHit();
        return null;
    }

    private YeetableCollider GetYeetTarget(out RaycastHit hit)
    {
        int hits = Physics.RaycastNonAlloc(transform.position + (_characterController.center * 0.5f), _graphicsTransform.forward, _raycastHelper, _pushDistance, int.MaxValue, QueryTriggerInteraction.Ignore);

        for(int i = 0; i < hits; i++)
        {
            hit = _raycastHelper[i];

            if(hit.collider.GetComponentInChildren<YeetableCollider>() is YeetableCollider pushableCollider)
            {
                return pushableCollider;
            }
        }

        hit = new RaycastHit();
        return null;
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

    private void HandleFootSteps()
    {
        if(!_characterActive)
        {
            return;
        }
        _footStepTimer -= Time.deltaTime;

        if(_movementDirection.magnitude >= 0.05f && _footStepTimer <= 0f && !_hasJumped)
        {
            AudioUtils.Play(_footStepAudioSource);
            _footStepTimer = _footStepInterval;
        }
    }

    public void SetEnabled(bool f)
    {
        _vCam.gameObject.SetActive(f);
        _characterActive = f;
    }

    public void Kill() { }

    public void LevelCompleted()
    {
        if(_ikHelper != null)
        {
            _ikHelper.SetAsWon();
        }
        PlayJumpParticles();
    }

    private void PlayJumpParticles()
    {
        foreach(ParticleSystem ps in _jumpParticles)
        {
            if(ps != null)
            {
                ps.Play();
            }
        }
    }

    private float _preparationDuration = 0.25f;
    private float _dashDuration = 0.5f;
    private float _dashDistance = 15f;

    private float _dashHeight = 1f;

    private IEnumerator DashRoutine()
    {
        float originHeight = transform.position.y;
        float preparationTimer = _preparationDuration;
        float dashTimer = _dashDuration;

        _blockUpdate = true;

        AudioUtils.PlayOneShot(_oneShotAudioSource, _jumpAudio);

        while(preparationTimer > 0f)
        {
            preparationTimer -= Time.deltaTime;
            float prepT = 1f - (preparationTimer / _preparationDuration);
            float height = Mathf.Lerp(originHeight, originHeight + _dashHeight, prepT);

            transform.position = new Vector3(transform.position.x, height, transform.position.z);

            yield return null;
        }

        AudioUtils.PlayOneShot(_oneShotAudioSource, _dashAudio);
        PlayJumpParticles();
        while(dashTimer > 0f)
        {
            dashTimer -= Time.deltaTime;
            float frameMovement = (_dashDistance / _dashDuration) * Time.deltaTime;
            CollisionFlags flags = _characterController.Move(_graphicsTransform.forward.normalized * frameMovement);

            if((flags & CollisionFlags.Sides) == CollisionFlags.Sides)
            {
                // Force transfer from player to a block
                YeetableCollider pushTarget = GetYeetTarget(out RaycastHit hit);
                if(pushTarget != null)
                {
                    pushTarget.RigidbodyProxy.AddForce(-hit.normal.normalized * _yeetForce, ForceMode.VelocityChange);
                    _currentMovementSpeed = _movementSpeed * 0.4f;

                    _blockUpdate = false;
                    yield break;
                }
            }

            yield return null;
        }

        _blockUpdate = false;
    }

    private void OnAction(InputAction.CallbackContext obj)
    {
        if(!_characterActive || _blockUpdate)
        {
            return;
        }

        if(_characterController.isGrounded && GameManager.Instance.IsPowerUpActive(PowerUpIdentifier.Dash))
        {
            StartCoroutine(DashRoutine());
            return;
        }

        bool canJump;
        if(_jumpAllowedAfterAirTimeThreshold <= 0)
        {
            canJump = _characterController.isGrounded;
        }
        else
        {
            canJump = Time.time <= (_lastGroundedAt + _jumpAllowedAfterAirTimeThreshold);
        }
        if(canJump && GameManager.Instance.IsPowerUpActive(PowerUpIdentifier.Jump))
        {
            AudioUtils.PlayOneShot(_oneShotAudioSource, _jumpAudio);
            _verticalMovementVelocity = _jumpPower;
            _lastGroundedAt = 0;
            _hasJumped = true;
            _landingAudioAllowedAt = Time.time + _landingAudioDelay;
            PlayJumpParticles();
        }
    }

    private void OnMovementX(InputAction.CallbackContext obj) => _movementDirection.x = obj.ReadValue<float>();
    private void OnMovementY(InputAction.CallbackContext obj) => _movementDirection.y = obj.ReadValue<float>();

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position + (_characterController.center * 0.5f), _graphicsTransform.forward * _pushDistance);
    }
}

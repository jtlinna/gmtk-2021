using UnityEngine;

public class IkHelpers : MonoBehaviour
{
    [Header("Legs")]
    [SerializeField]
    private Transform _rightFootTarget = null;

    [SerializeField]
    private Transform _leftFootTarget = null;

    [SerializeField]
    private Transform _rightRestingtTarget = null;

    [SerializeField]
    private Transform _leftRestingTarget = null;

    [SerializeField]
    private Transform _rightFootAim = null;

    [SerializeField]
    private Transform _leftFootAim = null;

    [SerializeField]
    private Transform _characterPositionTransform;

    [SerializeField]
    private float _distanceThreshold = 2f;

    [SerializeField]
    private float _smoothDamp = 0.1f;

    [SerializeField]
    private float _dampSpeed = 1f;

    [SerializeField]
    private CharacterController _characterController = null;

    private Vector3 _lastPosition;

    private bool _moveOddFoot = false;

    private Vector3 _leftFootPosition;
    private Vector3 _rightFootPosition;

    private Vector3 _rightFootVelocity;
    private Vector3 _leftFootVelocity;

    [Header("Torso")]
    [SerializeField]
    private Transform _torsoTransform = null;

    [SerializeField]
    [Range(0, 1)]
    float _torsoSpeed = 1f;

    [SerializeField]
    [Range(0, 0.01f)]
    float _torsoRange = 0.2f;

    [Header("Hands")]
    [SerializeField]
    private Transform _rightHandTarget = null;

    [SerializeField]
    private Transform _leftHandTarget = null;

    [SerializeField]
    private Transform _rightHandAim = null;

    [SerializeField]
    private Transform _leftHandAim = null;
    

    [SerializeField]
    private float _smoothDampHands = 0.1f;

    [SerializeField]
    private float _dampSpeedHands = 1f;

    [SerializeField]
    private Transform _rightHandToucher;
    [SerializeField]
    private Transform _leftHandToucher;

    [SerializeField]
    private LayerMask _HandToucherLayerMask;

    [SerializeField]
    private float _groapingDistance = 4f;

    private Vector3 _leftHandPosition;
    private Vector3 _rightHandPosition;

    private Vector3 _rightHandVelocity;
    private Vector3 _leftHandVelocity;
    
    

    void loop()
    {
        float yPos = Mathf.PingPong(Time.time * _torsoSpeed, 1) * _torsoRange;
        _torsoTransform.localPosition =
            new Vector3(_torsoTransform.localPosition.x, 0.003f + yPos, _torsoTransform.localPosition.z);
    }

    private void Awake()
    {
        Stand();
    }

    void FixedUpdate()
    {
        loop();

        if (_characterController.velocity.magnitude < 0.1f)
        {
            Stand();
            
        }

        if (Vector3.Distance(_characterPositionTransform.position, _lastPosition) > _distanceThreshold)
        {
            if (_moveOddFoot)
            {
                PlaceLeftFoot();
            }
            else
            {
                PlaceRightFoot();
            }

            SetLastPosition();
        }
        Ray ray = new Ray(_leftHandToucher.position, _rightHandToucher.forward);
        if (Physics.Raycast(ray, out RaycastHit leftHit, _groapingDistance, _HandToucherLayerMask))
        {
            _leftHandPosition = leftHit.point;
        }
        ray = new Ray(_rightHandToucher.position, _rightHandToucher.forward);
        if (Physics.Raycast(ray, out RaycastHit rightHit, _groapingDistance, _HandToucherLayerMask))
        {
            _rightHandPosition = rightHit.point;
        }

        _leftHandTarget.position = Vector3.SmoothDamp(_leftHandTarget.position, _leftHandPosition,
            ref _leftHandVelocity, _smoothDampHands, _dampSpeedHands, Time.fixedDeltaTime);
        _rightHandTarget.position = Vector3.SmoothDamp(_rightHandTarget.position, _rightHandPosition,
            ref _rightHandVelocity, _smoothDampHands, _dampSpeedHands, Time.fixedDeltaTime);
        _rightFootTarget.position = Vector3.SmoothDamp(_rightFootTarget.position, _rightFootPosition,
            ref _rightFootVelocity, _smoothDamp, _dampSpeed, Time.fixedDeltaTime);
        _leftFootTarget.position = Vector3.SmoothDamp(_leftFootTarget.position, _leftFootPosition,
            ref _leftFootVelocity, _smoothDamp, _dampSpeed, Time.fixedDeltaTime);
    }

    void Stand()
    {
        Ray ray = new Ray(_leftRestingTarget.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit leftHit, 10f))
        {
            _leftFootPosition = leftHit.point;
        }

        ray = new Ray(_rightRestingtTarget.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit rightHit, 10f))
        {
            _rightFootPosition = rightHit.point;
        }
        _leftHandPosition = _leftHandAim.position;
        _rightHandPosition = _rightHandAim.position;
    }

    void PlaceLeftFoot()
    {
        Ray ray = new Ray(_leftFootAim.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 10f))
        {
            _leftFootPosition = hit.point;
        }

        _leftHandPosition = _leftHandAim.position;
        _moveOddFoot = false;
    }

    void PlaceRightFoot()
    {
        Ray ray = new Ray(_rightFootAim.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 10f))
        {
            _rightFootPosition = hit.point;
        }
        
        _rightHandPosition = _rightHandAim.position;
        _moveOddFoot = true;
    }

    void SetLastPosition()
    {
        _lastPosition = _characterPositionTransform.position;
    }
}
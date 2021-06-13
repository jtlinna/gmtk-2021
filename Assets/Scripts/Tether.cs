using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tether : MonoBehaviour
{
    [Serializable]
    private class TetherHandler
    {

        [SerializeField]
        private LineRenderer _lineRenderer;

        [SerializeField]
        private Transform _targetPointTransform;

        public bool IsConnected { get; set; }
        public bool IsTethered { get; set; }
        public float MiddleYPosition { get; set; }
        public float LerpCounter { get; set; } = 0f;
        public Character ConnectedCharacter { get; set; }

        private readonly List<Vector3> _pointList = new List<Vector3>();

        private Vector3 _middlePoint = new Vector3();
        private Vector3 _currentVelocity;
        private float _distanceThreshold = 0.1f;

        public void HandleFixedUpdate(AnimationCurve curve, Transform originPointTransform, float vertexCount, float maxSpeed, float maxDistance, float smoothTime)
        {
            if(IsConnected)
            {
                MiddleYPosition =
                    curve.Evaluate(Vector3.Distance(_targetPointTransform.position, originPointTransform.position) /
                                   maxDistance);
                if(IsTethered)
                {
                    _targetPointTransform.position = ConnectedCharacter.GetTetherTransform.position;
                }
                else
                {
                    _targetPointTransform.position = Vector3.Lerp(_targetPointTransform.position, ConnectedCharacter.GetTetherTransform.position, LerpCounter += Time.fixedDeltaTime / 5);
                    if(Vector3.Distance(_targetPointTransform.position, ConnectedCharacter.GetTetherTransform.position) <= _distanceThreshold)
                    {
                        IsTethered = true;
                    }
                }
            }
            else if(!IsTethered)
            {
                _targetPointTransform.position = Vector3.Lerp(_targetPointTransform.position, originPointTransform.position, LerpCounter += Time.fixedDeltaTime / 5);
                if(Vector3.Distance(_targetPointTransform.position, originPointTransform.position) <= _distanceThreshold)
                {
                    IsTethered = true;
                }
            }

            _pointList.Clear();

            _middlePoint = Vector3.SmoothDamp(_middlePoint,
                new Vector3((originPointTransform.transform.position.x + _targetPointTransform.transform.position.x) / 2,
                    MiddleYPosition,
                    (originPointTransform.transform.position.z + _targetPointTransform.transform.position.z) / 2),
                ref _currentVelocity, smoothTime, maxSpeed, Time.fixedDeltaTime);

            for(float ratio = 0; ratio <= 1; ratio += 1 / vertexCount)
            {
                Vector3 tangent1 = Vector3.Lerp(originPointTransform.position, _middlePoint, ratio);
                Vector3 tangent2 = Vector3.Lerp(_middlePoint, _targetPointTransform.position, ratio);
                Vector3 curveValue = Vector3.Lerp(tangent1, tangent2, ratio);

                _pointList.Add(curveValue);
            }


            _lineRenderer.positionCount = _pointList.Count;


            _lineRenderer.SetPositions(_pointList.ToArray());
        }
    }


    [SerializeField]
    private Transform _originPointTransform;
    [SerializeField]
    private float _vertexCount = 12;
    [SerializeField]
    private float _smoothTime = 2;
    [SerializeField]
    private float _maxSpeed = 2;
    [SerializeField]
    private AnimationCurve curve = null;
    [SerializeField]
    private float MaxDistance;
    [SerializeField]
    private List<TetherHandler> _tetherHandlers = new List<TetherHandler>();
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _connectAudio;
    [SerializeField]
    private AudioClip _disconnectAudio;
    [SerializeField]
    private float _audioDelayFromStart = 0.25f;

    private readonly Dictionary<Character, TetherHandler> _tetherHandlerLookup = new Dictionary<Character, TetherHandler>();

    private float _audioAllowedAt;

    private void Start()
    {
        _audioAllowedAt = Time.time + _audioDelayFromStart;
    }


    public void OnCharacterEnter(Character character)
    {
        if(_tetherHandlerLookup.ContainsKey(character))
        {
            return;
        }

        TetherHandler handler = _tetherHandlers.FirstOrDefault(h => !h.IsConnected);
        if(handler == null)
        {
            return;
        }

        handler.IsConnected = true;
        handler.ConnectedCharacter = character;
        handler.LerpCounter = 0;
        handler.IsTethered = false;
        _tetherHandlerLookup[character] = handler;

        if(Time.time >= _audioAllowedAt)
        {
            AudioUtils.PlayOneShot(_audioSource, _connectAudio);
        }
    }

    public void OnCharacterExit(Character character)
    {
        if(!_tetherHandlerLookup.TryGetValue(character, out TetherHandler handler))
        {
            return;
        }

        handler.ConnectedCharacter = null;
        handler.MiddleYPosition = 1f;
        handler.IsConnected = false;
        handler.IsTethered = false;
        handler.LerpCounter = 0f;
        _tetherHandlerLookup.Remove(character);

        if(Time.time >= _audioAllowedAt)
        {
            AudioUtils.PlayOneShot(_audioSource, _disconnectAudio);
        }
    }

    void FixedUpdate()
    {
        foreach(TetherHandler handler in _tetherHandlers)
        {
            handler.HandleFixedUpdate(curve, _originPointTransform, _vertexCount, _maxSpeed, MaxDistance, _smoothTime);
        }
    }
}
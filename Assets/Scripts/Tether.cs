using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tether : MonoBehaviour
{
    [SerializeField]
    private Transform _originPointTransform;

    private Vector3 _middlePoint = new Vector3();

    [SerializeField]
    private Transform _targetPointTransform;
    
    private Character _connectedCharacter;
    
    [SerializeField]
    private LineRenderer _linerenderer;

    [SerializeField]
    private float _vertexCount = 12;

    private float _middleYposition = 2f;

    [SerializeField]
    private float _smoothTime = 2;

    [SerializeField]
    private float _maxSpeed = 2;

    [SerializeField]
    private AnimationCurve curve = null;

    private Transform _originalPoint3;
    private List<Transform> _point3Overrides = new List<Transform>();

    [SerializeField]
    private float MaxDistance;

    private bool _isConnected;
    private bool _isTethered;
    private float _lerpCounter = 0f;
    private float _distanceThreshold = 0.1f;


    public void OnCharacterEnter(Character character)
    {
        if (_point3Overrides.Contains(character.transform))
        {
            return;
        }
        _point3Overrides.Add(character.transform);
        _isConnected = true;
        _connectedCharacter = character;
        _lerpCounter = 0;
        _isTethered = false;
    }

    public void OnCharacterExit(Character character)
    {
        if (!_point3Overrides.Contains(character.transform))
        {
            return;
        }
        
        _connectedCharacter = null;
        _middleYposition = 1f;
        _point3Overrides.Remove(character.transform);
        _isConnected = false;
        _lerpCounter = 0f;
        _middleYposition = 1f;
        _isTethered = false;
    }

    private Vector3 _currentVelocity;
    private Vector3 _returningVelocity;
    private List<Vector3> _pointList = new List<Vector3>();

    void FixedUpdate()
    {
       
        if (_isConnected)
        {
            _middleYposition =
                curve.Evaluate(Vector3.Distance(_targetPointTransform.position, _originPointTransform.position) /
                               MaxDistance);
            if (_isTethered)
            {
                _targetPointTransform.position = _connectedCharacter.GetTetherTransform.position;
            }
            else
            {
                _targetPointTransform.position = Vector3.Lerp(_targetPointTransform.position, _connectedCharacter.GetTetherTransform.position, _lerpCounter += Time.fixedDeltaTime / 5);
                if (Vector3.Distance(_targetPointTransform.position, _connectedCharacter.GetTetherTransform.position) <= _distanceThreshold)
                {
                    _isTethered = true;
                }
            }
        }
        else if(!_isTethered)
        {
            _targetPointTransform.position = Vector3.Lerp(_targetPointTransform.position, _originPointTransform.position, _lerpCounter += Time.fixedDeltaTime / 5);
            if (Vector3.Distance(_targetPointTransform.position, _originPointTransform.position) <= _distanceThreshold)
            {
                _isTethered = true;
            }
        }

        _pointList = new List<Vector3>();

        _middlePoint = Vector3.SmoothDamp(_middlePoint,
            new Vector3((_originPointTransform.transform.position.x + _targetPointTransform.transform.position.x) / 2,
                _middleYposition,
                (_originPointTransform.transform.position.z + _targetPointTransform.transform.position.z) / 2),
            ref _currentVelocity, _smoothTime, _maxSpeed, Time.fixedDeltaTime);

        for (float ratio = 0; ratio <= 1; ratio += 1 / _vertexCount)
        {
            Vector3 tangent1 = Vector3.Lerp(_originPointTransform.position, _middlePoint, ratio);
            Vector3 tangent2 = Vector3.Lerp(_middlePoint, _targetPointTransform.position, ratio);
            Vector3 curve = Vector3.Lerp(tangent1, tangent2, ratio);

            _pointList.Add(curve);
        }


        _linerenderer.positionCount = _pointList.Count;


        _linerenderer.SetPositions(_pointList.ToArray());
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tether : MonoBehaviour
{
    [SerializeField]
    private Transform _point1;

    
    private Vector3 _middlePoint = new Vector3();

    [SerializeField]
    private Transform _point3;

    [SerializeField]
    private LineRenderer _linerenderer;

    [SerializeField]
    private float _vertexCount = 12;

    [SerializeField]
    private float _middleYposition = 2f;

    [SerializeField]
    private float _smoothTime = 2;

    [SerializeField]
    private float _maxSpeed = 2;

    private void Start()
    {
        // since the line exists in world space we have to center this object
        transform.position = Vector3.zero;
    }

    private Vector3 _currentVelocity;
    private List<Vector3> _pointList = new List<Vector3>();
    void FixedUpdate()
    {
     // Vector3 stupidOffset = new Vector3(Random.Range(-_randomOffset, _randomOffset),
     //     Random.Range(-_randomOffset, _randomOffset), Random.Range(-_randomOffset, _randomOffset));
        
        
        _middlePoint = Vector3.SmoothDamp(_middlePoint, new Vector3((_point1.transform.position.x + _point3.transform.position.x) / 2,
            _middleYposition, (_point1.transform.position.z + _point3.transform.position.z) / 2), ref _currentVelocity, _smoothTime, _maxSpeed, Time.fixedDeltaTime);

        if (1 / _vertexCount <= 1)
        {
            _pointList = new List<Vector3>();
            for (float ratio = 0; ratio <= 1; ratio += 1 / _vertexCount)
            {
                Vector3 tangent1 = Vector3.Lerp(_point1.position, _middlePoint, ratio);
                Vector3 tangent2 = Vector3.Lerp(_middlePoint, _point3.position, ratio);
                Vector3 curve = Vector3.Lerp(tangent1, tangent2, ratio);

                _pointList.Add(curve);
            }

            _linerenderer.positionCount = _pointList.Count;
        }
        else
        {
            for (int i = 0; i < _pointList.Count; i++)
            {
                Vector3 tangent1 = Vector3.Lerp(_point1.position, _middlePoint, _pointList[i].z);
                Vector3 tangent2 = Vector3.Lerp(_middlePoint, _point3.position, _pointList[i].z);
                Vector3 curve = Vector3.Lerp(tangent1, tangent2, _pointList[i].z);
                _pointList[i] = curve;
                Debug.Log("æsdjs");
            }
        }

        _linerenderer.SetPositions(_pointList.ToArray());
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    private Transform _t;
    [SerializeField, Range(0.1f, 360)] private float _Speed;
    [SerializeField, Range(-1, 1)] private int _Direction;
    void Start()
    {
        _t = transform;
    }

    void LateUpdate()
    {
        _t.Rotate(0,0, _Speed * _Direction * Time.deltaTime);
    }
}

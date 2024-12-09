using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDirection : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _Rb;
    float _angle;
    Transform _t;
    private bool _canRotate;
    private void Start()
    {
        _canRotate = true;
        _t = transform;
    }
    void Update()
    {
        if (!_canRotate) return;
        _angle = Mathf.Atan2(_Rb.linearVelocity.y, _Rb.linearVelocity.x) * Mathf.Rad2Deg;
        _t.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _canRotate = false;
    }
}

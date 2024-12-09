using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouth : MonoBehaviour
{
    [SerializeField] private float _Speed;
    void Update()
    {
        transform.position += _Speed * Time.deltaTime * Vector3.right;
    }
}

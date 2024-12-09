using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] List<Transform> _Points;
    [SerializeField] float _Speed;


    private int _index;


    void Update()
    {
        if (transform.position != _Points[_index].position)
            transform.position = Vector3.MoveTowards(transform.position, _Points[_index].position, _Speed * Time.deltaTime);
        else
        {
            if (_index + 1 >= _Points.Count)
                _index = 0;
            else
                _index++;
        }
    }
}

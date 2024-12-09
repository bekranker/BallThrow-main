using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialClick : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }
    }
}
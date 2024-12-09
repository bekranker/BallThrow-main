using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroySound : MonoBehaviour
{
    private void Start()
    {
        GameObject[] soundObj = GameObject.FindGameObjectsWithTag("GameMusic");
        if (soundObj.Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}

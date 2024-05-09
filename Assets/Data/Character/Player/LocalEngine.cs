using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalEngine : MonoBehaviour
{
    private Vector3 desiredRotation = new Vector3(-90f, 0f, 0f);
    void Start()
    {

    }
    void Update()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(desiredRotation);
    }
}

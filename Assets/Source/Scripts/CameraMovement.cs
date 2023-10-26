using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Vector3 _offset = new Vector3(0, 25, -9);

    private void Start()
    {
        Camera.main.transform.rotation = Quaternion.Euler(70, 0, 0);
    }

    private void Update()
    {
        Camera.main.transform.position = transform.position + _offset;
    }
}

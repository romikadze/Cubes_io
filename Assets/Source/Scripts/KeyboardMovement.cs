using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMovement : MonoBehaviour
{
    private float _inputX;
    private float _inputZ;
    private float _speed;
    private Cube _cube;

    private void Start()
    {
        _cube = GetComponent<Cube>();
        UpdateSpeed();
        _cube.scoreChanged += UpdateSpeed;
    }

    private void FixedUpdate()
    {
        _inputX = Input.GetAxis("Horizontal");
        _inputZ = Input.GetAxis("Vertical");
        Vector3 translation = new Vector3(_inputX, 0, _inputZ) * _speed * Time.deltaTime;
        transform.Translate(translation, Space.World);

        if (transform.position.y < -5)
        {
            transform.position = GetRandomSpawnPosition();
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(-15, 15);
        float z = Random.Range(-15, 15);

        return new Vector3(x, 0.5f, z);
    }

    private void UpdateSpeed()
    {
        _speed = _cube.GetSpeed();
    }
}

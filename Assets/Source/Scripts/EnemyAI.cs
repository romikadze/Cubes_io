using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Food[] _food;
    private Vector3 _moveDirection = Vector3.zero;
    private float _speed;
    private float _brainSpeed = 0.8f;
    private float _distance = 100f;
    private Vector3 _direction;
    private Cube _cube;

    private void Start()
    {
        UpdateSpeed();
        StartCoroutine(UpdateTargetsCoroutine());
        _cube = GetComponent<Cube>();
        _cube.scoreChanged += UpdateSpeed;
    }

    //Movement
    private void Update()
    {
        transform.Translate(_moveDirection * _speed * Time.deltaTime, Space.World);

        if (transform.position.y < -5)
            transform.position = GetRandomSpawnPosition();
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(-15, 15);
        float z = Random.Range(-15, 15);

        return new Vector3(x, 0.5f, z);
    }

    private void UpdateSpeed()
    {
        _speed = GetComponent<Cube>().GetSpeed();
    }

    private void UpdateTargets()
    {
        _food = FindObjectsOfType<Food>();


        _moveDirection = FindNearestTarget();
    }

    private Vector3 FindNearestTarget()
    {
        _direction = Vector3.zero;
        _distance = 100;

        foreach (var target in _food)
        {
            if (_distance > (target.transform.position - transform.position).magnitude && target.gameObject != _cube.gameObject)
            {
                _direction = target.transform.position - transform.position;
                _distance = _direction.magnitude;

                if (target.GetScore() > GetComponent<Food>().GetScore())
                    _direction *= -1;
            }
        }

        return _direction.normalized;
    }

    private IEnumerator UpdateTargetsCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_brainSpeed);
            UpdateTargets();
        }
    }
}

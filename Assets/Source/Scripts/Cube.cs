using System;
using UnityEngine;

[RequireComponent(typeof(Food))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    private Food _food;
    private Food _enemyFood;
    private float _enemyScore;
    private Vector3 _punchDirection;


    public Action scoreChanged;

    private void Start()
    {
        _food = GetComponent<Food>();
    }

    private void Damage(Food food, float scoreDamage, Vector3 punchDirection)
    {
        food.GetComponent<Rigidbody>().AddForce(punchDirection * 10 + Vector3.up * 5, ForceMode.Impulse);
        food.ChangeScore(-scoreDamage);
    }

    private void Eat(float scoreIncrease)
    {
        _food.ChangeScore(scoreIncrease);
    }

    public float GetSpeed()
    {
        _food = GetComponent<Food>();
        return _speed / _food.GetScore();
    }

    private void ChangeSize()
    {
        transform.localScale = Vector3.one * _food.GetScore();
        scoreChanged?.Invoke();
    }

    private float GetScoreChange(float enemyScore)
    {
        float scoreChange;

        if (enemyScore > 0.5f)
            scoreChange = 0.3f;
        else
            scoreChange = enemyScore;
        return scoreChange;
    }

    //Take damage or get enemy score when touching the cube
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Food>() != null)
        {
            _enemyFood = collision.gameObject.GetComponent<Food>();
            _enemyScore = _enemyFood.GetScore();
            _punchDirection = (transform.position - collision.gameObject.transform.position).normalized;

            if (_food.GetScore() > _enemyScore)
            {
                Eat(GetScoreChange(_enemyScore));
                Damage(collision.gameObject.GetComponent<Food>(), GetScoreChange(_enemyScore), _punchDirection);
            }
            else if (_food.GetScore() == _enemyScore)
            {
                Damage(_enemyFood, 0, _punchDirection);
            }

            ChangeSize();
        }
    }
}

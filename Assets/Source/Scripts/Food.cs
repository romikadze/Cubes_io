using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private float _score = 1;

    public void ChangeScore(float amount)
    {
        _score += amount;
        if (_score < 0.5f)
            Die();
    }

    public float GetScore()
    {
        return _score;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

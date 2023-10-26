using System.Collections;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ChangeEnemyColors : MonoBehaviour
{
    private float _enemyColorUpdateRate = 2f;
    private EnemyColors _enemyColors;
    private Cube[] _enemies;
    Food _enemy;
    Food _player;

    void Start()
    {
        _enemyColors = new EnemyColors(new Color(0.15f, 0.55f, 0.25f), new Color(0.55f, 0.4f, 0.15f), new Color(0.55f, 0.15f, 0.15f));
        StartCoroutine(UpdateEnemyColorsCoroutine());
        _player = GetComponent<Food>();
    }

    private void UpdateEnemyColors()
    {
        GetCubes();
        foreach (var enemy in _enemies)
        {
            _enemy = enemy.GetComponent<Food>();

            if (enemy.gameObject == gameObject) continue;

            if (_enemy.GetScore() < _player.GetScore() - 0.15)
                SetColor(enemy, _enemyColors.WEAK);
            else if (_enemy.GetScore() > _player.GetScore() + 0.15)
                SetColor(enemy, _enemyColors.STRONG);
            else
                SetColor(enemy, _enemyColors.EQUAL);
        }
    }
    private void SetColor(Cube cubeToChange, Color color)
    {
        cubeToChange.GetComponent<MeshRenderer>().material.color = color;
    }

    private IEnumerator UpdateEnemyColorsCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_enemyColorUpdateRate);
            UpdateEnemyColors();
        }
    }

    private void GetCubes()
    {
        _enemies = FindObjectsOfType<Cube>();
    }

    private struct EnemyColors
    {
        public Color WEAK;
        public Color EQUAL;
        public Color STRONG;

        public EnemyColors(Color weak, Color equal, Color strong)
        {
            WEAK = weak;
            EQUAL = equal;
            STRONG = strong;
        }
    }
}

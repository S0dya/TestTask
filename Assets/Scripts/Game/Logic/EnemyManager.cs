using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private Transform enemyParentTransform;

    [SerializeField] private Transform playerTransform; //remove later

    private List<Enemy> enemies = new();

    private bool _enemiesAttackingPlayer;

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            var prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            var enemyObj = Instantiate(prefab, new Vector2(Random.Range(10, 20), prefab.transform.position.y), Quaternion.identity, enemyParentTransform);
            var enemy = enemyObj.GetComponent<Enemy>();

            enemies.Add(enemy);

            enemy.Init(this);
        }
    }

    public void EnemyAttacked()
    {
        if (_enemiesAttackingPlayer) return;

        _enemiesAttackingPlayer = true;

        foreach (var enemy in enemies)
            enemy.SetAttackTarget(playerTransform);
    }

    public void EnemyDied(Enemy enemy)
    {
        enemies.Remove(enemy);

        Destroy(enemy.gameObject);
    }
}

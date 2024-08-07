using System.Collections;
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
        for (int i = 0; i < 1; i++) SpawnEnemy();
    }

    public void EnemyAttacked()
    {
        if (_enemiesAttackingPlayer) return;

        _enemiesAttackingPlayer = true;

        foreach (var enemy in enemies)
            enemy.SetAttackTarget(playerTransform);

        StartCoroutine(SpawnEnemiesCoroutine(3, 6));
    }

    public void EnemyDied(Enemy enemy)
    {
        enemies.Remove(enemy);

        Destroy(enemy.gameObject);
    }

    private Enemy SpawnEnemy()
    {
        var prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        var enemyObj = Instantiate(prefab, 
            new Vector2((Random.value > 0.5f ? -40 : 40) + playerTransform.position.x, prefab.transform.position.y), Quaternion.identity, enemyParentTransform);
        var enemy = enemyObj.GetComponent<Enemy>();

        enemies.Add(enemy);

        enemy.Init(this);

        return enemy;
    }

    IEnumerator SpawnEnemiesCoroutine(int count, float delay)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnEnemy().SetAttackTarget(playerTransform);

            yield return new WaitForSeconds(Random.Range(delay/2, delay));
        }
    }
}

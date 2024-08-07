using UnityEngine;

[System.Serializable]
class EnemyAttackInfo : AttackInfo
{
    public string AttackName = "Punch"; //change to int later

    public float Cooldown = 2;
}
public class EnemyCombat : Combat
{
    [SerializeField] private EnemyAttackInfo[] attacks;

    private EnemyAttackInfo _curAttack;

    public string SetRandomAttack()
    {
        _curAttack = attacks[Random.Range(0, attacks.Length)];

        return _curAttack.AttackName;
    }
    public void PerformAttack()
    {
        var colliders = PerformAttack(_curAttack);

        if (colliders.Length > 0) 
            colliders[0].GetComponent<Player>()?.ChangeHP(-_curAttack.Damage);
    }

    public float GetCooldown()
    {
        return _curAttack.Cooldown;
    }
}
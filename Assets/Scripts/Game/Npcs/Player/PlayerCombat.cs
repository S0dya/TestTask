using System.Linq;
using UnityEngine;

[System.Serializable]
class AttackInfo
{
    public AttackEnum AttackName;

    public float Damage = 2;

    public Vector2 ColliderSize = Vector2.one;
    public Vector2 AttackLocalPosition;
}
enum AttackEnum
{
    StartHit,
    StartKick,
}

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private AttackInfo[] attacks;

    [Header("Other")]
    [SerializeField] private LayerMask enemyLayer;

    private AttackInfo _curAttack;

    //input
    public void Hit()
    {
        SetAttackInfo(AttackEnum.StartKick);

        PerformAttack();
    }
    public void Kick()
    {
        SetAttackInfo(AttackEnum.StartKick);

        PerformAttack();
    }

    private void SetAttackInfo(AttackEnum attackEnum)
    {
        _curAttack = attacks.First(attackInfo => attackInfo.AttackName == attackEnum);
    }
    private void PerformAttack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(_curAttack.AttackLocalPosition, _curAttack.ColliderSize.x / 2, enemyLayer);

        foreach (Collider hitCollider in hitColliders)
        {
            //hitCollider.GetComponent<Enemy>()?.TakeDamage(_curAttack.Damage);
        }
    }
}

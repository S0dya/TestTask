using System.Linq;
using UnityEngine;

[System.Serializable]
class PlayerAttackInfo : AttackInfo
{
    public AttackEnum AttackName;

}
enum AttackEnum
{
    StartHit,
    StartKick,
}

public class PlayerCombat : Combat
{
    [SerializeField] private PlayerAttackInfo[] attacks;

    private PlayerAttackInfo _curAttack;

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

    public void PerformAttack()
    {
        foreach (var hitCollider in PerformAttack(_curAttack))
            hitCollider.GetComponent<Enemy>()?.ChangeHP(-_curAttack.Damage);
    }

    private void SetAttackInfo(AttackEnum attackEnum)
    {
        _curAttack = attacks.First(attackInfo => attackInfo.AttackName == attackEnum);
    }

    private void OnDrawGizmosSelected()
    {
        if (_curAttack != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere((Vector2)transform.position + _curAttack.AttackLocalPosition, _curAttack.ColliderSize.x / 2);
        }
    }
}

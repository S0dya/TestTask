using UnityEngine;

[System.Serializable]
public class AttackInfo
{
    public int Damage = 2;

    public Vector2 ColliderSize = Vector2.one;
    public Vector2 AttackLocalPosition;
}
public class Combat : MonoBehaviour
{
    [SerializeField] private LayerMask oponentLayer;

    protected private Collider2D[] PerformAttack(AttackInfo attackInfo)
    {
        return Physics2D.OverlapCircleAll(transform.TransformPoint(attackInfo.AttackLocalPosition), attackInfo.ColliderSize.x / 2, oponentLayer);
    }
}
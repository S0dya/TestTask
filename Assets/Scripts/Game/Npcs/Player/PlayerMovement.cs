using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float movementSpeed = 4;

    [Header("Other")]
    [SerializeField] private Rigidbody2D rb;

    //reusable

    private float _movementDirection;

    private void Update()
    {
        rb.velocity = new Vector2(_movementDirection * movementSpeed, 0);

    }

    public void SetMoveement(float value)
    {
        _movementDirection = value;
    }
}

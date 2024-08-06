using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float movementSpeed = 4;

    [Header("Other")]
    [SerializeField] private Rigidbody2D rb;

    //reusable

    private Vector2 _movementDirection;

    private void Update()
    {
        rb.velocity = new Vector2(_movementDirection.x * movementSpeed, 0);


    }

    public void Move(Vector2 direction)
    {
        _movementDirection = direction;
    }
}

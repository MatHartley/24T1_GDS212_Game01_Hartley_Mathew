using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveCooldown;
    [SerializeField] private float cooldownMax;
    private bool isMoving = false;

    [Header("Transform Location")]
    private float x;
    private float y;
    private Vector2 nextPos;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveCooldown = cooldownMax;
    }

    private void Update()
    {
        moveCooldown -= Time.deltaTime;

        if (moveCooldown <= 0)
        {
            if (!isMoving)
            {
                x = Random.Range(-.5f, .5f);
                y = Random.Range(-.5f, .5f);
                nextPos = new Vector2(x, y);
                rb.velocity = nextPos;
                isMoving = true;
            }
            else
            {
                nextPos = new Vector2(0, 0);
                isMoving = false;
            }
            moveCooldown = cooldownMax;
        }
    }
}
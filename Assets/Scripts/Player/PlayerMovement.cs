using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public Vector2 moveDir;
    [HideInInspector] public Vector2 lastMovedDir;

    private PlayerStats player;
    private Animator animator;
    private SpriteRenderer sR;
    private Rigidbody2D rb;

    private void Start()
    {
        player = GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
        sR = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        lastMovedDir = new Vector2(1, 0f);
    }
    void Update()
    {
        InputManager();
    }

    void FixedUpdate()
    {
        Move();
    }

    void InputManager()
    {
        if(GameManager.instance.isGameOver)
        {
            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(moveX, moveY).normalized;

        if(moveDir.x != 0  || moveDir.y != 0)
        {
            lastMovedDir = moveDir;
        }

        if(moveDir.x != 0 && moveDir.y != 0)
        {
            lastMovedDir = new Vector2(moveDir.x, 0);
        }
    }

    private void Move()
    {
        if (GameManager.instance.isGameOver)
        {
            return;
        }

        MoveAnimator();
        rb.velocity = new Vector2(player.CurrentMoveSpeed * moveDir.x, player.CurrentMoveSpeed * moveDir.y);
        
    }

    private void MoveAnimator()
    {
        if(moveDir.x < 0 || lastMovedDir.x < 0)
        {
            sR.flipX = true;
        }
        else
        {
            sR.flipX = false;
        }

        if (moveDir.x == 0 && moveDir.y == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }
    }
}

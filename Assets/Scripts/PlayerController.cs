using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //https://youtu.be/nlBwNx-CKLg - 2D Animation with Blend Trees Unity Tutorial

    [SerializeField] private float speed_ = 10.0f;
    private Rigidbody2D rb_;
    private Animator animator_;
    private Vector2 move_dir_;
    private Vector2 last_move_dir_;

    public event Action OnEncountered;

    private void Awake()
    {
        rb_ = GetComponent<Rigidbody2D>();
        animator_ = GetComponent<Animator>();
    }

    public void DoUpdate()
    {
        ProcessInputs();
        //Move();
        Animate();
    }

    public void DoFixedUpdate()
    {
        Move();
    }

    private void ProcessInputs()
    {
        float input_x = Input.GetAxisRaw("Horizontal");
        float input_y = Input.GetAxisRaw("Vertical");

        if ((input_x == 0 && input_y == 0) && (move_dir_.x != 0 || move_dir_.y != 0))
        {
            last_move_dir_ = move_dir_;
        }

        move_dir_ = new Vector2(input_x, input_y).normalized;
    }

    private void Animate()
    {
        animator_.SetFloat("Velocity", rb_.velocity.magnitude);
        animator_.SetFloat("LookDirX", move_dir_.x);
        animator_.SetFloat("LookDirY", move_dir_.y);
        animator_.SetFloat("LastLookDirX", last_move_dir_.x);
        animator_.SetFloat("LastLookDirY", last_move_dir_.y);

        if (move_dir_.x < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        if (move_dir_.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void Move()
    {
        rb_.velocity = new Vector2(move_dir_.x * speed_, move_dir_.y * speed_);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BattleGrass"))
        {
            if (UnityEngine.Random.Range(1,100) < 10)
            {
                rb_.velocity = Vector2.zero; //stop player from moving when battle starts
                OnEncountered();
            }
        }
    }
}
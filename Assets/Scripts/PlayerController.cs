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
    private float encounter_cooldown_ = 0;

    public event Action OnEncountered;

    public BattleUnit playerCreature;

    private SfxFootstepController footstep_controller_;

    void Start()
    {
        Debug.Log(Time.time);
        StartCoroutine("DelayStart");
        Debug.Log(Time.time);
        OtherStart();
        footstep_controller_ = GetComponent<SfxFootstepController>();
    }
    IEnumerable DelayStart()
    {
        Debug.Log("Lay start");
        if (Time.time < 1.0f) {
            Debug.Log(Time.time);
            yield return new WaitForEndOfFrame(); }
        Debug.Log("Lay end");
    }
    void OtherStart()

    {
        Debug.Log("meow");
        //stuff

    }

    private void Awake()
    {
        rb_ = GetComponent<Rigidbody2D>();
        animator_ = GetComponent<Animator>();
    }

    public void DoUpdate()
    {
        if (encounter_cooldown_ > 0)
        {
            encounter_cooldown_ -= Time.deltaTime;
        }
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
        if (encounter_cooldown_ > 0)
        {
            return;
        }
        if (other.gameObject.CompareTag("BattleGrass"))
        {
            if (UnityEngine.Random.Range(1,100) < 40)
            {
                encounter_cooldown_ = 3.0f;
                rb_.velocity = Vector2.zero; //stop player from moving when battle starts
                animator_.SetFloat("Velocity", rb_.velocity.magnitude); //reset anim
                OnEncountered();
            }
            if (other.gameObject.GetComponent<SfxFootstepId>() != null)
            {
                footstep_controller_.SetCurrFootstepSfx(other.gameObject.GetComponent<SfxFootstepId>().GetFootstepId());
                Debug.Log(">>> Stay");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<SfxFootstepId>() != null)
        {
            footstep_controller_.SetCurrFootstepSfx(other.gameObject.GetComponent<SfxFootstepId>().GetFootstepId());
            Debug.Log(">>> Enter");
        }
        if (other.gameObject.tag == "ContainerTrigger")
        {
            playerCreature.CheatHeal();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<SfxFootstepId>() != null)
        {
            footstep_controller_.ResetFootstepSfx();
            Debug.Log(">>> Exit");
        }
    }
}
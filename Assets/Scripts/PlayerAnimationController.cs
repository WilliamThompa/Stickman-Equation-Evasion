using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerAnimationController : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    public Movement movement;
    public PlayerCombat playerCombat;
    public LevelManager levelmanager;

    private int curState;
    private float _lockedUntil;

    private static readonly int Idle = Animator.StringToHash("PlayerIdle");
    private static readonly int Dash = Animator.StringToHash("PlayerDash");
    private static readonly int DoubleJump = Animator.StringToHash("PlayerDoubleJump");
    private static readonly int Fall = Animator.StringToHash("PlayerFall");
    private static readonly int Falling = Animator.StringToHash("PlayerFalling");
    private static readonly int Grounds = Animator.StringToHash("PlayerGrounds");
    private static readonly int StandPunch = Animator.StringToHash("PlayerStandPunch");
    private static readonly int Running = Animator.StringToHash("PlayerRunning");
    private static readonly int Jump = Animator.StringToHash("PlayerStillJump");
    public bool isFalling;

    public float dummynum;
    public int stateNum;
    public bool grounds;
    public bool isJumping;
    public bool isDoubleJumping;
    public bool isPunching;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var state = GetState();
        if (state == curState) return;
        animator.CrossFade(state, 0.5f, 0);
        curState = state;
        grounds = movement.Grounds;
        isPunching = playerCombat.attacking;

        stateNum = state;
        isJumping = movement.isJumping;
        isDoubleJumping = movement.isDoubleJumping;
    }

    private int GetState()
    {
        dummynum = movement.falling;

        if (Time.time < _lockedUntil) return curState;

        if (movement.IsGrounded() && movement.falling < -4)
        {
            if (curState == Idle)
            {
                return LockState(Grounds, animator.GetCurrentAnimatorStateInfo(0).length / 4);
            }
            return LockState(Grounds, animator.GetCurrentAnimatorStateInfo(0).length);
        }
        if (movement.falling < -2 && movement.falling > -6) return Fall;
        if (movement.falling < -6)
        {
            isFalling = true;
            return Falling;
        }

        if (movement.isDashing) return Dash;
        if (movement.isDoubleJumping) return DoubleJump;
        if (movement.isJumping) return Jump;
        

        if (movement.IsGrounded() && movement.falling >= -2) return movement.anim_running ? Running : Idle;
        if (movement.IsGrounded()) return Idle;

        int LockState(int s, float t)
        {
            _lockedUntil = Time.time + t;
            return s;
        }

        return 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

}

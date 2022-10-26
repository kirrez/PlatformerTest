using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations
{
    private Animator Animator;

    public PlayerAnimations(Animator animator)
    {
        Animator = animator;
    }

    public void Idle()
    {
        Animator.SetInteger("State", 0);
    }

    public void Walk()
    {
        Animator.SetInteger("State", 1);
    }

    public void JumpRising()
    {
        Animator.SetInteger("State", 2);
    }

    public void JumpFalling()
    {
        Animator.SetInteger("State", 3);
    }

    public void Dying()
    {
        Animator.SetTrigger("Dead");
    }

    public void Attack()
    {
        Animator.SetInteger("State", 4);
    }

    public void AirAttack()
    {
        Animator.SetInteger("State", 5);
    }

    public void Sit()
    {
        Animator.SetInteger("State", 6);
    }

    public void SitAttack()
    {
        Animator.SetInteger("State", 7);
    }

    public void RollDown()
    {
        Animator.SetInteger("State", 8);
    }

    public void Crouch()
    {
        Animator.SetInteger("State", 9);
    }

    public void DamageTaken()
    {
        Animator.SetInteger("State", 10);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightBossAttack : MonoSingleton<NightBossAttack>
{
    public int attackDamage = 20;
    private float attackRange = 1f;
    private float attackTime=1f;
    
    public Vector3 attackOffset;
    public LayerMask attackMask;
    public Transform attackPoint;
    public Animator animator;

    public void Attack()
    {
        if(PlayerController.Instance.isDeath==false)
        {
            Vector3 pos = transform.position;
            pos += transform.right * attackOffset.x;
            pos += transform.up * attackOffset.y;

            Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
            if (colInfo != null && PlayerController.Instance.isDeath==false && PlayerController.Instance.isDashing==false)
                colInfo.GetComponent<PlayerController>().TakeDamage(attackDamage);
        }
    }

    IEnumerator Attacking()
    {
        animator.SetBool("isAttacking",true);
        yield return new WaitForSeconds(attackTime);
        animator.SetBool("isAttacking",false);
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange/7);
    }
}

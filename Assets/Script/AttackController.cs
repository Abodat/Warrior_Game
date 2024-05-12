using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoSingleton<AttackController>
{
    private float attackTime = 0.4f;
    public float attackRange = 0.5f;
    public int attackDamage = 40;

    private bool canAttack = true;
    public bool canMove = true;

    public Transform attackPoint;
    public LayerMask enemyLayers;
    
    void Update()
    {
        
    }

    public void Attack(bool _isGrounded,Animator _animator)
    {
        if (_isGrounded && canAttack)
        {
            canAttack = false;
            canMove = false;
            _animator.SetTrigger("Attack");
            PlayerController.Instance.rb.velocity = new Vector2(0,0);
            StartCoroutine(AttackTimer());
            
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<NightBoss>().TakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(attackTime);
        canAttack = true;
        canMove = true;
    }
}

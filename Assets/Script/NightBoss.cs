using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class NightBoss : MonoBehaviour
{
    public Animator animator;
    public Transform player;

    public bool isFlipped = false;
    private int maxHealth = 200;
    private int currentHealth;
    private float deathTime = 2f;
    
    void Start()
    {
        currentHealth = maxHealth;
        HealthController.Instance.SetBossMaxHealth(maxHealth);
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale=flipped;
            transform.Rotate(0f,180f,0f);
            isFlipped = false;
        }
        
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        HealthController.Instance.SetBossHealth(currentHealth);
        if (animator.GetBool("isAttacking")==false) 
         animator.SetTrigger("Hurt");
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("IsDead",true);
        StartCoroutine(DeathTimer());
        GameManager.Instance.WinGame();
    }

    private IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(deathTime);
            Destroy(gameObject);
            //GetComponent<Collider2D>().enabled = false;
           // this.enabled = false;
    }
}

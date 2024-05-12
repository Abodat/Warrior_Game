using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class NightBoss : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    private int currentHealth;
    private float deathTime = 2f;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Öldü.");
        
        animator.SetBool("IsDead",true);
        StartCoroutine(DeathTimer());
        
    }

    private IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(deathTime);
            Destroy(gameObject);
            //GetComponent<Collider2D>().enabled = false;
           // this.enabled = false;
    }
}

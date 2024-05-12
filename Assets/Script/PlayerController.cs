using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController> 
{
    public Animator animator;

    private Vector2 movement;
    
    private float horizontal;
    public float moveSpeed = 2f;
    public float jumpPower = 4f;
    private float speed;
    public float dashingPower = 2f;
    private float dashingTime = 0.2f;
    public float dashingCooldown = 0.3f;

    private bool isDeath = false;
    private bool isFacingRight = true;
    private bool canDash = true;
    private bool isDashing;

    public int maxHealth = 100;
    public int currentHealth;
    
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private Transform grounCheck;
    [SerializeField] private LayerMask groundLayer;

    public HealtAndStamina healtAndStamina;

    void Start(){
        
        currentHealth = maxHealth;
        healtAndStamina.SetMaxHealth(maxHealth);
    }

    void Update(){
        
        horizontal = Input.GetAxis("Horizontal");
        movement.x= Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        speed = (movement.x == 0) ? 0 :movement.x / Mathf.Abs(movement.x) ;

        animator.SetFloat("Speed",Mathf.Abs(speed));
        animator.SetBool("IsGrounded",IsGrounded());
        
        if(rb.velocity.y !=0){
        animator.SetFloat("Vertical",rb.velocity.y/Mathf.Abs(rb.velocity.y));
        }
        
        else{
            animator.SetFloat("Vertical",0);
        }

        if (Input.GetKeyDown("w") && IsGrounded()){
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        if (Input.GetAxis("Horizontal")!=0){
            animator.SetFloat("Horizontal", movement.x);
        }

        if (Input.GetKeyDown(KeyCode.Space) && AttackController.Instance.canMove){
            AttackController.Instance.Attack(IsGrounded(),animator);
            TakeDamage(10);

        }
        
        else if (Input.GetKeyDown(KeyCode.LeftShift) && canDash){
            StartCoroutine(Dash());
        }
        
        Flip();
    }

    void FixedUpdate(){
        if (isDashing || !AttackController.Instance.canMove){
            return;
        }
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        
    }

    public bool IsGrounded(){
        return Physics2D.OverlapCircle(grounCheck.position, 0.2f, groundLayer);
    }

    private void Flip(){
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f){
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash(){
        if (isFacingRight && IsGrounded()){
            canDash = false;
            isDashing = true; 
            rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
            animator.SetTrigger("Dash");
            yield return new WaitForSeconds(dashingTime);
            isDashing = false;
            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;
            
        }
        else if(!isFacingRight && IsGrounded()){
            canDash = false;
            isDashing = true; 
            rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
            animator.SetTrigger("Dash");
            yield return new WaitForSeconds(dashingTime);
            isDashing = false;
            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;
        }
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        healtAndStamina.SetHealth(currentHealth);
        
        if(currentHealth<=0)
            Death();
    }

    public void Death(){
        animator.SetBool("IsDead",true);
    }
}

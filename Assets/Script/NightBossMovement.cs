using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightBossMovement : StateMachineBehaviour
{
   public float speed = 1.5f;
   public float attackRange = 4f;

   Transform player;
   Rigidbody2D rb;
   NightBoss boss;
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      player = GameObject.FindGameObjectWithTag("Player").transform;
      rb = animator.GetComponent<Rigidbody2D>();
      boss = animator.GetComponent<NightBoss>();
   }

   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      if ( PlayerController.Instance.isDeath==false)
      {
         boss.LookAtPlayer();
               
               Vector2 target = new Vector2(player.position.x, rb.position.y);
               Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
               rb.MovePosition(newPos);
         
               if (Vector2.Distance(player.position, rb.position) <= attackRange)
               {
                  animator.SetTrigger("Attack");
               }
      }

      else
      {
         animator.SetBool("playerDetected",false);
      }
      
   }

   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      animator.ResetTrigger("Attack");
   }
}

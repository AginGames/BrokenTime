using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossattack : StateMachineBehaviour
{
    public float speed = 0.05f;

    Transform player;
    Rigidbody2D rb;
    Vector2 movement;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(player.position.x, player.position.y);
        Vector2 newPosit = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosit);
        movement.y = newPosit.y - rb.position.y;
        movement.x = newPosit.x - rb.position.x;

        animator.SetFloat("movement.y", movement.y);
        animator.SetFloat("movement.x", movement.x);
        //Debug.Log("Function Called");
        //rb.MovePosition(rb.position + target * speed * Time.fixedDeltaTime);
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}

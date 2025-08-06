using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppenHeimerDemage : MonoBehaviour
{
    public float demageDelay = 1;
    private float time;
    private Health health;
    private Animator animator;
    private void Awake()
    {
        time = Time.fixedTime;
        health = GameObject.FindGameObjectWithTag("logic").GetComponent<Health>();
        animator = GetComponent<Animator>();
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        while (collision.gameObject.layer == 3)
        {
            if ((Time.time - time > demageDelay))
            {
                health.RemoveHealth(100);
                time = Time.fixedTime;

            }
        }
    }*/
    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((Time.time - time > demageDelay) && collision.gameObject.layer == 3)
        {
            health.RemoveHealth(500);
            time = Time.fixedTime;
            //animator.SetTrigger("attack");

        }
    }
}

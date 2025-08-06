using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballscript : MonoBehaviour
{
    public float DeadZone = 20f;
    private void Start()
    {
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 0))
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            GameObject.FindGameObjectWithTag("boss").GetComponent<Health>().RemoveHealth(50);
            GetComponent<Animator>().SetTrigger("bumm");
            //Destroy(this.gameObject);
        }
    }
    void Update()
    {
        if(transform.position.y > DeadZone || transform.position.y < -DeadZone || transform.position.x > DeadZone || transform.position.x < -DeadZone)
        {
            Destroy(gameObject);
        }
    }
}

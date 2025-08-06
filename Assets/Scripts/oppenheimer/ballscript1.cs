using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballscript2 : MonoBehaviour
{
    public float DeadZone = 20f;
    private void Start()
    {
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 3))
        {
            GameObject.FindGameObjectWithTag("logic").GetComponent<Health>().RemoveHealth(500);
            Destroy(this.gameObject);
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

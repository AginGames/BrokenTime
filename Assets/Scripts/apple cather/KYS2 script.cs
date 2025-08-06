using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KYS2scipt : MonoBehaviour
{
    public float deadZone = -20f;
    public applespawner logic;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<applespawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < deadZone)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 3))
        {
            logic.removeScore();
            Destroy(this.gameObject);
        }
    }
}
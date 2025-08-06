using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KYSscipt : MonoBehaviour
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
            logic.addScore(1);
            Destroy(this.gameObject);
        }
    }
}

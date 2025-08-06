using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private Camera cam;
    private Rigidbody2D playerRigidbody;
    private Rigidbody2D rb;
    Vector2 mousePos;
    private float time;
    public bool canShoot = false;
    public float shootDelay = 0.5f;
    public float bulletForce = 20f;
    void Start()
    {
        time = Time.fixedTime - shootDelay;
        cam = FindAnyObjectByType<Camera>();
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || (Input.GetMouseButtonDown(0))) && (Time.fixedTime - time > shootDelay) && (canShoot))
        {
            Shoot();
            time = Time.fixedTime;
        }
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
    private void FixedUpdate()
    {
        Vector2 shootDir = mousePos - playerRigidbody.position;
        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg - 90;
        rb.rotation = angle;
        rb.position = playerRigidbody.position;
    }
}

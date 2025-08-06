using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour, IData
{
    public float speed = 10f;

    public Rigidbody2D rb;

    Vector2 movement;
    public Animator animator;
    public bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        EnableControls();
    }
    public void DisableControls()
    {
        canMove = false;
    }
    public void EnableControls()
    {
        canMove=true;
    }
    public void LoadData(GameData data)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            this.transform.position = data.playerPosition;
        }

    }
    public void SaveData(ref GameData data)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            data.playerPosition = this.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("horizontal", movement.x);
            animator.SetFloat("vertical", movement.y);
            animator.SetFloat("speed", movement.sqrMagnitude);


        }


    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}

using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private bool isGrounded;
    // public Transform groundCheck;

    // shoot
    /* public float shotgunRecoilForce = 5f;
     public Transform firePoint;
     public GameObject bulletPrefab;*/
    public float shootForce;

    private SpriteRenderer PlayerSprite;
    public SpriteRenderer HeadSprite;
    private Animator animator;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        PlayerSprite = GetComponent<SpriteRenderer>();
        //  groundCheck = transform.Find("GroundCheck"); // Create an empty GameObject at the player's feet for checking ground.
    }

    private void Update()
    {

        if (!GameManager.gameFailed)
            HandleInput();
        ///HandleShooting();
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        // Move();
        //   Debug.Log(isGrounded);
    }
    #region Movement_Functions
    private void HandleInput()
    {
        // Horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput > 0)
        {
            animator.SetBool("isRunning", true);
            PlayerSprite.flipX = false;
            HeadSprite.flipX = false;
        }
        else if (horizontalInput < 0)
        {
            animator.SetBool("isRunning", true);
            PlayerSprite.flipX = true; HeadSprite.flipX = true;
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        rb.velocity = movement;

        // Jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

    }

    private void Move()
    {
        // Smooth horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 targetVelocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, 0.1f);
    }

    private void Jump()
    {
        animator.SetBool("inAir", true);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void CheckGrounded()
    {
        // Check if the player is grounded using a small circle at the player's feet
        //  isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, LayerMask.GetMask("Ground"));
    }
    #endregion

    /*private void HandleShooting()
    {
        // Shooting with pump shotgun
        *//*  if (Input.GetButtonDown("Fire1")) // Assuming "Fire1" is the left mouse button
          {
              Shoot();
              ApplyRecoil();
          }*//*
    }
    private void Shoot()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)firePoint.position).normalized;

        // Instantiate the bullet with the calculated direction
        GameObject bullet = Instantiate(Resources.Load("BulletPrefab"), firePoint.position, Quaternion.identity) as GameObject;
        //   bullet.GetComponent<Rigidbody2D>().velocity = direction * bullet.GetComponent<BulletController>().bulletSpeed;

    }*/

    public void BackForce(Component sender, object data)
    {
        if (data is Vector3)
        {
            Vector3 direction = (Vector3)data;
            rb.AddForce(-(direction - rb.gameObject.transform.position) * shootForce, ForceMode2D.Impulse);
        }
    }
    public void ApplyRecoil(Vector3 direction)
    {
        // Apply recoil force to the player
        rb.AddForce(-(direction - rb.gameObject.transform.position) * shootForce, ForceMode2D.Impulse);

        //  rb.AddForce(Vector2.left * shotgunRecoilForce, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            animator.SetBool("inAir", false);
            isGrounded = true;
        }
        else if (collision.gameObject.tag == "Obstacle")
        {
            GameManager.gameFailed = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            animator.SetBool("inAir", true);
            isGrounded = false;
        }
        else if(collision.gameObject.tag == "Enemy")
        {

        }
    }
}

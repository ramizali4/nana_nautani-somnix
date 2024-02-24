using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NanaAI_FInal : MonoBehaviour
{
    public float speed = 5f; // Adjust the speed as needed  
    private Rigidbody2D rb;
    public float upwardForce = 50f;

    public float delayDuration = 0.1f; // Adjust this to set the delay duration

    public float moveSpeed = 5f; // Adjust the speed as needed
                                 //  public float moveDistance = 5f; // Adjust the distance to move back and forth

    private Vector3 startPosition;

    public Transform leftPosition;
    public Transform rightPosition;

    private bool isMovingRight = true;
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (!GameManager.gameFailed)
        {
            if (isMovingRight)
            {
                spriteRenderer.flipX = false;
                MoveToPosition(rightPosition.position);

                if (transform.position.x >= rightPosition.position.x)
                {
                    isMovingRight = false;
                }
            }
            else
            {
                MoveToPosition(leftPosition.position);
                spriteRenderer.flipX = true;

                if (transform.position.x <= leftPosition.position.x)
                {
                    isMovingRight = true;
                }
            }
        }
    }

    void MoveToPosition(Vector3 targetPosition)
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            GameManager.gameFailed = true;
            //   LevelFailed_Event.OnEventRaised(this, true);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.gameFailed = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Jumper")
        {
            rb.AddForce(Vector2.up * upwardForce, ForceMode2D.Impulse);
            Invoke("AddForceAfterDelay", delayDuration);
        }
    }
    void AddForceAfterDelay()
    {
        // Apply an upward force to the Rigidbody2D after the delay
        //  rb.AddForce(Vector2.up * upwardForce, ForceMode2D.Impulse);

        // Optionally, add force to the right
        rb.AddForce(Vector2.right * upwardForce / 3, ForceMode2D.Impulse);
    }
}
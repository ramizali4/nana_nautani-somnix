using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NanaAI : MonoBehaviour
{
    public float speed = 5f; // Adjust the speed as needed  
    private Rigidbody2D rb;
    public float upwardForce = 50f;

    public float delayDuration = 0.1f; // Adjust this to set the delay duration
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (!GameManager.gameFailed)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            GameManager.gameFailed = true;
            //   LevelFailed_Event.OnEventRaised(this, true);
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
        rb.AddForce(Vector2.right * upwardForce/3, ForceMode2D.Impulse);
    }
}

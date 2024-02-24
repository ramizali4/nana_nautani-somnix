
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public GameObject impact_VFX;
 //   public GameObject shot_VFX;

    private void Start()
    {
       // Instantiate(shot_VFX, transform);
    }
    void Update()
    {
        // Move the bullet in the forward direction
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy the bullet when it hits something
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "bullet")
        {
            Instantiate(impact_VFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

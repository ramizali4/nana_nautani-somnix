using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public string targetTag = "Nana";  // Tag of the target (player) to approach
    private Transform target;          // Target (player) to approach
    public float moveSpeed = 5f;      // Movement speed
    public float stoppingDistance = 2f;  // Distance to stop from the target
    public GameObject hit_VFX;

    public GameObject Retro;
    public GameObject Future;
    private bool isRetro = true;
    private void Start()
    {
        // Find the target by tag when the script starts
        target = GameObject.FindWithTag(targetTag)?.transform;

        if (target == null)
        {
            Debug.LogError("Target with tag " + targetTag + " not found!");
        }
    }
    private void Update()
    {
        if (target != null)
        {
            // Calculate the direction to the target
            Vector3 direction = target.position - transform.position;

            // Normalize the direction vector to ensure consistent speed in all directions
            direction.Normalize();

            // Move towards the target
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            // If the distance to the target is less than or equal to the stopping distance, stop moving
            if (Vector3.Distance(transform.position, target.position) <= stoppingDistance)
            {
                // Optionally, you can add code here to perform an action when the enemy reaches the target
                // For example, attacking the player.
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet" || collision.gameObject.tag == "Player")
        {
            Instantiate(hit_VFX, transform.position, transform.rotation);
            Destroy(this.gameObject);
        } 
    }
    public void Switch(Component component,object data)
    {
        if (isRetro)
        {
            Retro.SetActive(false);
            Future.SetActive(true);
            isRetro = false;
        }
        else
        {
            Retro.SetActive(true);
            Future.SetActive(false);
            isRetro= true;
        }
    }
}

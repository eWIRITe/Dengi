using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;

    void Start()
    {
        // Add a force to the bullet to move it forward
        GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet collides with an object with a Health component
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            // If so, deal damage to the object
            health.TakeDamage(damage);
        }

        // Destroy the bullet on collision
        Destroy(gameObject);
    }
}
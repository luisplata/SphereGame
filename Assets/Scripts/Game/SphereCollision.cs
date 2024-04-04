using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCollision : MonoBehaviour
{
    public Transform otherCollider; // Reference to the other collider

    public Vector2 velocity; // Current velocity of the sphere
    public float speed = 5f; // Initial speed of the sphere

    private void Start()
    {
        // Set the initial velocity
        velocity = new Vector2(speed, speed);
    }

    private void Update()
    {
        // Move the sphere using the velocity
        transform.Translate(velocity * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Get the collision normal
        Vector2 normal = collision.contacts[0].normal.normalized;

        // Calculate the new velocity after the collision
        velocity = Vector2.Reflect(velocity, normal);   
    }
}

using System;
using UnityEngine;

public class PlayerCustom : MonoBehaviour
{
    [SerializeField][Range(1,10)] private float speed = 1;
    private Vector2 velocity;
    private bool cantMove;

    internal void Shoot(Vector2 direction)
    {
        velocity = direction.normalized * speed;
        velocity = velocity *-1;
        cantMove = true;
    }

    private void Update() {
        if(!cantMove) return;
        transform.Translate(velocity * Time.deltaTime);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Get the collision normal
        Vector2 normal = collision.contacts[0].normal.normalized;

        // Calculate the new velocity after the collision
        velocity = Vector2.Reflect(velocity, normal);
    }

    internal void Locate(Vector2 vector)
    {
        transform.position = vector;
    }
}
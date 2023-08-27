using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustom : MonoBehaviour
{
    [SerializeField][Range(1,10)] private float speed = 1;
    [SerializeField] private LineRenderer lineRenderer;
    private Vector2 velocity;
    private bool cantMove;
    private ILogicOfLevel _levelLogic;
    private Queue<float> timeToLive = new();

    public void Shoot(Vector2 direction)
    {
        velocity = direction.normalized * speed;
        velocity = velocity *-1;
        cantMove = true;
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 0;
        timeToLive.Enqueue(5f);
        StartCoroutine(DestroyAfterTime());
    }

    private IEnumerator DestroyAfterTime()
    {
        //Read a queue
        while (timeToLive.Count > 0)
        {
            var time = timeToLive.Dequeue();
            yield return new WaitForSeconds(time);   
        }
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if(!cantMove) return;
        var position = transform.position;
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, position);
    }

    private void Update() {
        if(!cantMove) return;
        transform.Translate(velocity * Time.deltaTime);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<CollisionCustom>(out var baseElement)){
            //Debug.Log($"Layer is {baseElement.GetElement().GetLayer()}");
            if(baseElement.GetElement().GetLayer() != _levelLogic.GetCurrentLayer()){
                return;
            }
        }

        // Get the collision normal
        Vector2 normal = collision.contacts[0].normal.normalized;

        // Calculate the new velocity after the collision
        velocity = Vector2.Reflect(velocity, normal);
        
        //Add more life to the player
        timeToLive.Enqueue(2f);
    }

    internal void Locate(Vector2 vector)
    {
        transform.position = vector;
    }

    public void Config(ILogicOfLevel levelLogic)
    {
        _levelLogic = levelLogic;
    }

    public void Stop()
    {
        cantMove = false;
    }
}
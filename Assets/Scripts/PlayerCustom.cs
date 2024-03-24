using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustom : MonoBehaviour
{
    [SerializeField][Range(1,10)] private float speed = 1;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Vector2 velocity;
    private bool cantMove, _isShoot;
    private ILogicOfLevel _levelLogic;
    private Queue<float> timeToLive = new();

    public void Shoot(Vector2 direction)
    {
        velocity = direction.normalized * speed;
        velocity = velocity *-1;
        cantMove = true;
        _isShoot = true;
        AddPointInLine(transform.position);
        AddPointInLine(transform.position);
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

        ServiceLocator.Instance.GetService<ILogicOfLevel>().LoseGame();
        Destroy(gameObject);
    }

    private void Update() {
        if(!cantMove) return;
        transform.Translate(velocity * Time.deltaTime);
        ServiceLocator.Instance.GetService<ILineRendererController>().SetPositionToLastPoint(_levelLogic.GetCurrentLayer(), transform.position);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<CollisionCustom>(out var baseElement)){
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
        
        AddPointInLine(transform.position);
    }

    private void AddPointInLine(Vector3 position)
    {
        ServiceLocator.Instance.GetService<ILineRendererController>().AddPoint(_levelLogic.GetCurrentLayer(), position);
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

    public void AddTime(float time)
    {
        timeToLive.Enqueue(time);
    }

    public bool CanMove()
    {
        return cantMove;
    }
    
    public bool IsShoot()
    {
        return _isShoot;
    }

    public void DestroyGameObject()
    {
        spriteRenderer.enabled = false;
        Destroy(gameObject, 1);
    }
}
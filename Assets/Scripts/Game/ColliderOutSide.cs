using UnityEngine;

public class ColliderOutSide : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            ServiceLocator.Instance.GetService<ILogicOfLevel>().LoseGame();
            Destroy(other.gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            ServiceLocator.Instance.GetService<ILogicOfLevel>().LoseGame();
            Destroy(other.gameObject);
        }
    }
}
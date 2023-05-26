using UnityEngine;

public class CollisionCustom : MonoBehaviour
{
    [SerializeField] private BaseElementInScene element;

    public BaseElementInScene GetElement(){
        return element;
    }
}

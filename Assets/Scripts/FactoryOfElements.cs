using UnityEngine;

public class FactoryOfElements : MonoBehaviour, IFactory
{
    [SerializeField] BaseElementInScene bounce, pointToStart;
    public BaseElementInScene GetElementWithOutInstantate(string name)
    {
        switch(name){
            case "RebotadorDebil":
                return bounce;
            case "PointToStart":
                return pointToStart;
            default:
                throw new System.Exception("Element not found");
        }
    }
}
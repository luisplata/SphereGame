using UnityEngine;

public class FactoryOfElements : MonoBehaviour, IFactory
{
    [SerializeField] BaseElementInScene bounce, bounceWaek, pointToStart, motionSensor, wall, wallSmall, teleportBeging, teleportEnd, boosterLeft, boosterRight, booster, teleportLayerBeging, teleportLayerEnd;
    public BaseElementInScene GetElementWithOutInstantate(string name)
    {
        switch(name){
            case "Bounce":
                return bounce;
            case "PointToStart":
                return pointToStart;
            case "MotionSensor":
                return motionSensor;
            case "Wall":
                return wall;
            case "WallSmall":
                return wallSmall;
            case "TeleportBeging":
                return teleportBeging;
            case "TeleportEnd":
                return teleportEnd;
            case "BoosterLeft":
                return boosterLeft;
            case "BoosterRight":
                return boosterRight;
            case "BounceWeak":
                return bounceWaek;
            case "Booster":
                return booster;
            case "TeleportLayerBeging":
                return teleportLayerBeging;
            case "TeleportLayerEnd":
                return teleportLayerEnd;
            default:
                throw new System.Exception("Element not found");
        }
    }
}
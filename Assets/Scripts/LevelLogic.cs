using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelLogic : MonoBehaviour, ILogicOfLevel {

    public Transform PositionInLevel => positionInLevel;
    public Action onStartShoot;
    [SerializeField] private GameObject panelGanaste;
    [SerializeField] private string mapToLoad;
    [SerializeField] private FactoryOfElements factory;
    [SerializeField] private PlayerCustom player;
    [SerializeField] private InputFacade input;
    private PlayerCustom playerInstantiate;

    private int pointSuccess;
    private Transform positionInLevel;
    private List<CheckPoint> checkPoints;
    private GameObject[] positionsToCheckPoints;
    private ReadingFile reding;
    [SerializeField]private int currentLayer;

    private void Start() {

        onStartShoot = ()=>{
            panelGanaste.SetActive(false);
            pointSuccess = 0;
        };
        
        input.onRelease = ShootPlayer;
        input.onFirstPosition = LocatePlayer;

        checkPoints = new List<CheckPoint>();
        GetElements(mapToLoad);
    }

    private void SuccessPoint()
    {
        pointSuccess++;
        Debug.Log("Success");
        if(pointSuccess >= checkPoints.Count){
            Debug.Log("All Success");
            panelGanaste.SetActive(true);
        }
    }
    
    private void LocatePlayer(Vector2 vector)
    {
        playerInstantiate = Instantiate(player);
        playerInstantiate?.Locate(vector);
        playerInstantiate?.Config(this);
        onStartShoot?.Invoke();
    }

    private void ShootPlayer(Vector2 direction)
    {
        playerInstantiate?.Shoot(direction);
        Destroy(playerInstantiate.gameObject, 20);
    }

    public int GetCurrentLayer()
    {
        return currentLayer;
    }

    public List<GameObject> GetElements(string data)
    {
        List<GameObject> listOfElements = new List<GameObject>();
        reding = new ReadingFile(mapToLoad);
        foreach(var element in reding.GetElements()){
            var elementInstantiate = Instantiate(factory.GetElementWithOutInstantate(element.Element));
            if(elementInstantiate.GetType() == typeof(Bounce)){
                var casting = (Bounce) elementInstantiate;
                casting.Config(element, SuccessPoint, this);
                checkPoints.Add(casting.CheckPoint);
                listOfElements.Add(casting.gameObject);
            }else if(elementInstantiate.GetType() == typeof(PointToStart)){
                var casting = (PointToStart) elementInstantiate;
                casting.Config(element);
                positionInLevel = casting.transform;
                listOfElements.Add(casting.gameObject);
            }else if(elementInstantiate.GetType() == typeof(MotionSensor)){
                var casting = (MotionSensor) elementInstantiate;
                casting.Config(element, this);
                listOfElements.Add(casting.gameObject);
            }else if(elementInstantiate.GetType() == typeof(TeleportBeging)){
                var casting = (TeleportBeging) elementInstantiate;
                casting.Config(element);
                var redingg = new ReadingFile(element.Data.Replace('\'','\"'));
                Debug.Log(redingg);
                foreach(var elementt in redingg.GetElements()){
                    var elementInstantiatee = Instantiate(factory.GetElementWithOutInstantate(elementt.Element));
                    if(elementInstantiatee.GetType() == typeof(TeleportEnd)){
                        var castingg = (TeleportEnd) elementInstantiatee;
                        castingg.Config(elementt);
                        casting.Config(element, castingg);
                        break;
                    }
                }
                listOfElements.Add(casting.gameObject);
            }else if(elementInstantiate.GetType() == typeof(TeleportEnd)){
                var casting = (TeleportEnd) elementInstantiate;
                casting.Config(element);
                listOfElements.Add(casting.gameObject);
            }else{
                elementInstantiate.Config(element);
            }
        }
        return listOfElements;
    }
}
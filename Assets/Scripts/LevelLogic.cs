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
        reding = new ReadingFile(mapToLoad);
        foreach(var element in reding.GetElements()){
            var elementInstantiate = Instantiate(factory.GetElementWithOutInstantate(element.Element));
            if(elementInstantiate.GetType() == typeof(Bounce)){
                var casting = (Bounce) elementInstantiate;
                casting.Config(element, SuccessPoint, this);
                checkPoints.Add(casting.CheckPoint);
            }else if(elementInstantiate.GetType() == typeof(PointToStart)){
                var casting = (PointToStart) elementInstantiate;
                casting.Config(element);
                positionInLevel = casting.transform;
            }
        }
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
}
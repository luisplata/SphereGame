using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelLogic : MonoBehaviour, ILogicOfLevel {

    public Transform PositionInLevel => positionInLevel;
    [SerializeField] private GameObject panelGanaste;
    [SerializeField] private string mapToLoad;
    [SerializeField] private FactoryOfElements factory;
    [SerializeField] private PlayerCustom player;
    [SerializeField] private InputFacade input;
    private PlayerCustom playerInstantiate;
    private Transform positionInLevel;
    private GameObject[] positionsToCheckPoints;
    private List<GameObject> listOfElements;
    private ReadingFile reding;
    [SerializeField]private int currentLayer;

    private void Start() {
        
        listOfElements = new List<GameObject>();
        GetElements(mapToLoad);
    }
    
    private void LocatePlayer(Vector2 vector)
    {
        playerInstantiate = Instantiate(player);
        playerInstantiate?.Locate(vector);
        playerInstantiate?.Config(this);
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
        input.onRelease = ShootPlayer;
        input.onFirstPosition = LocatePlayer;
        foreach(var eleme in listOfElements){
            Destroy(eleme.gameObject);
        }
        listOfElements = new List<GameObject>();
        reding = new ReadingFile(mapToLoad);
        foreach(var element in reding.GetElements()){
            var elementInstantiate = Instantiate(factory.GetElementWithOutInstantate(element.Element));
            elementInstantiate.Config(element, this);
            listOfElements.Add(elementInstantiate.gameObject);
            if(elementInstantiate.GetType() == typeof(Bounce)){
                var casting = (Bounce) elementInstantiate;
            }else if(elementInstantiate.GetType() == typeof(BounceWeak)){
                var casting = (BounceWeak) elementInstantiate;
            }else if(elementInstantiate.GetType() == typeof(PointToStart)){
                var casting = (PointToStart) elementInstantiate;
                positionInLevel = casting.transform;
                input.onRelease += (dir)=>{
                    casting.ShowEndWay();
                };
                input.onFirstPosition += (dir)=>{
                    casting.ResetAll();
                };
                casting.onWin = OnWin;
            }else if(elementInstantiate.GetType() == typeof(MotionSensor)){
                var casting = (MotionSensor) elementInstantiate;
            }else if(elementInstantiate.GetType() == typeof(TeleportBeging)){
                var casting = (TeleportBeging) elementInstantiate;
                var redingg = new ReadingFile(element.Data.Replace('\'','\"'));
                foreach(var elementt in redingg.GetElements()){
                    var elementInstantiatee = Instantiate(factory.GetElementWithOutInstantate(elementt.Element));
                    if(elementInstantiatee.GetType() == typeof(TeleportEnd)){
                        var castingg = (TeleportEnd) elementInstantiatee;
                        castingg.Config(elementt, this);
                        casting.Config(element, this, castingg);
                        listOfElements.Add(elementInstantiatee.gameObject);
                        break;
                    }
                }
            }else if(elementInstantiate.GetType() == typeof(TeleportEnd)){
                var casting = (TeleportEnd) elementInstantiate;
            }
        }
        return listOfElements;
    }

    private void OnWin()
    {
        Debug.Log("Win in Logic");
        input.onRelease = null;
        input.onFirstPosition = null;
        GetElements(mapToLoad);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using ServiceLocatorPath;
using UnityEngine;

public class LevelLogic : MonoBehaviour, ILogicOfLevel {

    public Transform PositionInLevel => positionInLevel;
    [SerializeField] private GameObject panelGanaste;
    [SerializeField] private string mapToLoad;
    [SerializeField] private FactoryOfElements factory;
    [SerializeField] private PlayerCustom player;
    [SerializeField] private InputFacade input;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float timeToWaitLineInScreen;
    [SerializeField] private LineRendererController lineRendererController;
    private PlayerCustom playerInstantiate;
    private Transform positionInLevel;
    private GameObject[] positionsToCheckPoints;
    private List<BaseElementInScene> listOfElements;
    private ReadingFile reding;
    [SerializeField]private int currentLayer;
    [SerializeField] private float timeToWaitBeforeStartTransparent;
    private List<BaseElementInScene> _listOfElementsWithLayer;

    private void Start() {
        
        listOfElements = new List<BaseElementInScene>();
        _listOfElementsWithLayer = GetElements(mapToLoad);
        ServiceLocator.Instance.GetService<ISoundSfxService>().PlaySound("bg", true);
        ChangeLayer(currentLayer);
        CreateLinesWithDataFromMap(_listOfElementsWithLayer);
    }

    private void CreateLinesWithDataFromMap(List<BaseElementInScene> list)
    {
        lineRenderer.positionCount = list.Count;
        var lastPosition = Vector3.zero;
        var index = 0;
        foreach (var baseElementInScene in list)
        {
            if (baseElementInScene.GetType() == typeof(Wall))
            {
                lineRenderer.positionCount--;
                continue;
            }
            if(baseElementInScene.GetType() == typeof(PointToStart)){
                lastPosition = baseElementInScene.GetPosition();
                lineRenderer.positionCount++;
            }
            lineRenderer.SetPosition(index, baseElementInScene.GetPosition());
            index++;
        }
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, lastPosition);
        StartCoroutine(StartTransparentToMaterialIntoLineRender(timeToWaitLineInScreen));
    }

    private IEnumerator StartTransparentToMaterialIntoLineRender(float timeToWait )
    {
        var material = lineRenderer.material;
        var color = material.color;
        var alpha = 1f;
        //Reset alpha
        color.a = alpha;
        material.color = color;
        yield return new WaitForSeconds(timeToWaitBeforeStartTransparent);
        //change alpha  1 to 0 in timeToWait
        while(alpha > 0){
            //Debug.Log("Alpha: " + alpha);
            alpha -= Time.deltaTime / timeToWait;
            color.a = alpha;
            material.color = color;
            //Debug.Log("Alpha: " + alpha);
            yield return null;
        }
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
        lineRendererController?.ResetLine();
    }

    public int GetCurrentLayer()
    {
        return currentLayer;
    }

    public void ChangeLayer(int layerDestiny)
    {
        currentLayer = layerDestiny;
        foreach(var element in _listOfElementsWithLayer)
        {
            element.ChangeLayer(element.GetLayer() == currentLayer);
            if(element is PointToStart pointToStart)
            {
                pointToStart.GetEndPoint().SetLayer(currentLayer);
            }
        }
    }

    public List<BaseElementInScene> GetElements(string data)
    {
        input.onRelease = ShootPlayer;
        input.onFirstPosition = LocatePlayer;
        foreach(var eleme in listOfElements){
            Destroy(eleme.gameObject);
        }
        listOfElements = new List<BaseElementInScene>();
        reding = new ReadingFile(data);
        foreach(var element in reding.GetElements()){
            var elementInstantiate = Instantiate(factory.GetElementWithOutInstantate(element.Element));
            elementInstantiate.Config(element, this);
            listOfElements.Add(elementInstantiate);
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
                var redingg = new ReadingFile(element.Data.Replace('\'','\"'));
                var elementsFromSensor = redingg.GetElements();
                var newList = new List<BaseElementInScene>();
                foreach(var elementt in elementsFromSensor){
                    var elementInstantiatee = Instantiate(factory.GetElementWithOutInstantate(elementt.Element));
                    elementInstantiatee.Config(elementt, this);
                    listOfElements.Add(elementInstantiatee);
                    newList.Add(elementInstantiatee);
                }
                casting.Config(element, this, newList);
            }else if(elementInstantiate.GetType() == typeof(TeleportLayerBeging)){
                var casting = (TeleportLayerBeging) elementInstantiate;
                var redingg = new ReadingFile(element.Data.Replace('\'','\"'));
                foreach(var elementt in redingg.GetElements()){
                    var elementInstantiatee = Instantiate(factory.GetElementWithOutInstantate(elementt.Element));
                    if(elementInstantiatee.GetType() == typeof(TeleportLayerEnd)){
                        var castingg = (TeleportLayerEnd) elementInstantiatee;
                        castingg.Config(elementt, this, elementt.LayerDestiny);
                        casting.Config(element, this, castingg, elementt.LayerDestiny);
                        listOfElements.Add(elementInstantiatee);
                        break;
                    }
                }
            }else if(elementInstantiate.GetType() == typeof(TeleportBeging)){
                var casting = (TeleportBeging) elementInstantiate;
                var redingg = new ReadingFile(element.Data.Replace('\'','\"'));
                foreach(var elementt in redingg.GetElements()){
                    var elementInstantiatee = Instantiate(factory.GetElementWithOutInstantate(elementt.Element));
                    if(elementInstantiatee.GetType() == typeof(TeleportEnd)){
                        var castingg = (TeleportEnd) elementInstantiatee;
                        castingg.Config(elementt, this);
                        casting.Config(element, this, castingg);
                        listOfElements.Add(elementInstantiatee);
                        break;
                    }
                }
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
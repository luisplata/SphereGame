using System;
using System.Collections;
using System.Collections.Generic;
using ServiceLocatorPath;
using UnityEngine;

public class LevelLogic : MonoBehaviour, ILogicOfLevel {

    public Transform PositionInLevel => positionInLevel;
    [SerializeField] private string mapToLoad;
    [SerializeField] private FactoryOfElements factory;
    [SerializeField] private PlayerCustom player;
    [SerializeField] private InputFacade input;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float timeToWaitLineInScreen;
    [SerializeField] private LineRendererController lineRendererController;
    [SerializeField] private UIController uiController;
    private PlayerCustom playerInstantiate;
    private Transform positionInLevel;
    private GameObject[] positionsToCheckPoints;
    private List<BaseElementInScene> listOfElements;
    private ReadingFile reding;
    [SerializeField]private int currentLayer;
    [SerializeField] private float timeToWaitBeforeStartTransparent;
    private List<BaseElementInScene> _listOfElementsWithLayer;
    private Action _onWin;
    public PointToEnd _endPoint;

    private void Start() {
        
        listOfElements = new List<BaseElementInScene>();
        _listOfElementsWithLayer = GetElements(mapToLoad);
        ServiceLocator.Instance.GetService<ISoundSfxService>().PlaySound("bg", true);
        ChangeLayer(currentLayer);
        CreateLinesWithDataFromMap(_listOfElementsWithLayer);
        uiController.Configure(this);
        input.CanRead(true);
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
        if (playerInstantiate == null)
        {
            playerInstantiate = Instantiate(player);
            playerInstantiate?.Locate(vector);
            playerInstantiate?.Config(this);
            positionInLevel = playerInstantiate.transform;
            input.CanRead(true);
        }
    }

    private void ShootPlayer(Vector2 direction)
    {
        if (playerInstantiate != null)
        {
            playerInstantiate?.Shoot(direction);
            lineRendererController?.ResetLine();
            input.CanRead(false);
            if (!playerInstantiate.IsShoot())
            {
                StartCoroutine(StartToShow());   
            }
        }
    }
    
    private IEnumerator StartToShow()
    {
        Debug.Log("Start to show");
        yield return new WaitForSeconds(1);
        _endPoint.gameObject.SetActive(true);
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
        }
        if (currentLayer == 0)
        {
            ServiceLocator.Instance.GetService<ILineRendererController>().ResetLines(currentLayer);
        }
        else
        {
            ServiceLocator.Instance.GetService<ILineRendererController>().CreateLine(currentLayer);
        }
    }

    public void SetCurrentEnd(PointToEnd pointToEnd)
    {
        _endPoint = pointToEnd;
        _endPoint.OnWin = OnWin;
    }

    public void ResetGame()
    {
        ChangeLayer(0);
        _listOfElementsWithLayer = GetElements(mapToLoad);
        CreateLinesWithDataFromMap(_listOfElementsWithLayer);
        //_endPoint.SetLayer(currentLayer);
        input.CanRead(true);
    }

    public void LoseGame()
    {
        input.CanRead(false);
        uiController.ShowPanelLose();
    }

    public void CanRead(bool canread)
    {
        input.CanRead(canread);
    }

    private List<BaseElementInScene> GetElements(string data)
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
                input.onRelease += _=>{
                    casting.ShowEndWay(playerInstantiate.IsShoot());
                };
                input.onFirstPosition += _=>{
                    if (!playerInstantiate.IsShoot())
                    {
                        casting.ResetAll();
                    }
                };
                ServiceLocator.Instance.GetService<ILogicOfLevel>().SetCurrentEnd(casting.GetEndPoint());
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
                        castingg.Config(elementt, this, elementt.LayerDestiny, input);
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
        _endPoint = null;
        playerInstantiate.Stop();
        playerInstantiate.DestroyGameObject();
        playerInstantiate = null;
        uiController.ShowPanelWin();
        input.CanRead(false);
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelLogic : MonoBehaviour {

    public Transform PositionInLevel => positionInLevel;
    [SerializeField] private LogicForMainMechanic mechanic;
    [SerializeField] private Transform positionInLevel;
    [SerializeField] private GameObject[] positionsToCheckPoints;
    [SerializeField] private CheckPoint checkPrefab;
    [SerializeField] private GameObject panelGanaste;

    private int pointSuccess;
    
    private List<CheckPoint> checkPoints;

    private void Start() {
        mechanic.onStartShoot = ()=>{
            panelGanaste.SetActive(false);
            pointSuccess = 0;
        };
        checkPoints = new List<CheckPoint>();
        foreach(var point in positionsToCheckPoints){
            var check = (CheckPoint) Instantiate(checkPrefab);
            check.transform.position = point.transform.position;
            check.onSuccessPoint += SuccessPoint;
            checkPoints.Add(check);
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
}
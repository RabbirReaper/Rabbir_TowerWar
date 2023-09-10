using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class buildManager : MonoBehaviour{
    public static buildManager main;

    [Header("References")]
    [SerializeField] private Tower[] towers;
    [SerializeField] GameObject towerPoint;
    int SelectTower = 0;
    float pointY;
    private void Awake() {
        main=this;
    }
    private void Start() {
        pointY = towerPoint.transform.position.y;
    }
    public Tower GetSelectTower(){
        return towers[SelectTower];
    }

    public void SetSelectedTower(int _SetSelectedTower){
        towerPoint.transform.position = new Vector3(towerPoint.transform.position.x,pointY + _SetSelectedTower*(-70),0);
        SelectTower = _SetSelectedTower;
    }
    public int GetSelectTowerIdx(){
        return SelectTower;
    }
}

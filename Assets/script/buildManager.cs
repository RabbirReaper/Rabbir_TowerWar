using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildManager : MonoBehaviour{
    public static buildManager main;

    [Header("References")]
    // [SerializeField] GameObject[] towerPrefabs;
    [SerializeField] private Tower[] towers;
    int SelectTower = 0;
    private void Awake() {
        main=this;
    }
    public Tower GetSelectTower(){
        return towers[SelectTower];
    }

    public void SetSelectedTower(int _SetSelectedTower){
        SelectTower = _SetSelectedTower;
    }
}

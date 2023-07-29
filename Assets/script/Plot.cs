using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour{
    
    [SerializeField] SpriteRenderer SR; 
    [SerializeField] Color hoverColor;
    GameObject towerObj;
    Turret turret;
    Color startColor;
    private void Start() {
        startColor=SR.color;
    }
    private void OnMouseEnter() {
        SR.color = hoverColor;
    }
    private void OnMouseExit() {
        SR.color=startColor;
    }
    private void OnMouseDown() {
        if(UIManager.main.IsHoveringUI()) return;

        if(towerObj != null) {
            turret.OpenUpgradeUI();
            return;
        }
        Tower tempTower = buildManager.main.GetSelectTower();
        if(tempTower.cost > LevelManager_script.main.currency){
            Debug.Log("you not have enough money!");
            return;
        }
        LevelManager_script.main.SpendCurrency(tempTower.cost);
        towerObj = Instantiate(tempTower.towerPrefab,transform.position,Quaternion.identity);
        turret = towerObj.GetComponent<Turret>(); 
        towerObj.transform.SetParent(transform);
    }
    public void TowerUpdate(GameObject levelUpTower){
        towerObj = Instantiate(levelUpTower,transform.position,Quaternion.identity);
        towerObj.transform.SetParent(transform);
        turret = towerObj.GetComponent<Turret>(); 
    }
}

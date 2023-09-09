using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour{
    
    [SerializeField] SpriteRenderer SR; 
    [SerializeField] Color hoverColor;
    GameObject towerObj;
    UpGradeUpdateTower UpGrade_Script;
    Color startColor;
    private void Start() {
        startColor=SR.color;
    }
    private void OnMouseEnter() {
        if(UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;
        SR.color = hoverColor;
    }
    private void OnMouseExit() {
        SR.color=startColor;
    }
    private void OnMouseDown() {
        if(UIManager.main.IsHoveringUI() || UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() || LevelManager_script.main.isEnd) return;
        if(towerObj != null) {
            UpGrade_Script.OpenUpgradeUI();
            Debug.Log("OpenUpgradeUI");
            return;
        }
        Tower tempTower = buildManager.main.GetSelectTower();
        if(tempTower.cost > LevelManager_script.main.Gold){
            Debug.Log("you not have enough money!");
            return;
        }
        if(LevelManager_script.main.GetTowerLimit() <= 0) return;
        LevelManager_script.main.SpendCurrency(tempTower.cost);
        towerObj = Instantiate(tempTower.towerPrefab,transform.position,Quaternion.identity);
        LevelManager_script.main.TowerLimitAdd(-1);
        UpGrade_Script = towerObj.GetComponent<UpGradeUpdateTower>(); //
        towerObj.transform.SetParent(transform);
    }
    public void TowerUpdate(GameObject levelUpTower){
        towerObj = Instantiate(levelUpTower,transform.position,Quaternion.identity);
        towerObj.transform.SetParent(transform);
        UpGrade_Script = towerObj.GetComponent<UpGradeUpdateTower>(); 
    }
}

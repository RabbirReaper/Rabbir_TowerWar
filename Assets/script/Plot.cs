using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour{
    
    [SerializeField] SpriteRenderer SR; 
    [SerializeField] Color hoverColor;
    GameObject tower;
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
        if(tower != null) return;
        Tower tempTower = buildManager.main.GetSelectTower();
        if(tempTower.cost > LevelManager_script.main.currency){
            Debug.Log("you not have enough money!");
            return;
        }
        LevelManager_script.main.SpendCurrency(tempTower.cost);
        tower = Instantiate(tempTower.towerPrefab,transform.position,Quaternion.identity);
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager_script : MonoBehaviour{
    public static LevelManager_script main;
    public Transform[] WayPoints_list;
    public float currency;
    private void Awake() {
        main = this;    
    }
    private void Start() {
        currency = 200;
    }
    public void IncreaseCurrency(float amount){
        currency+=amount;
    }
    public bool SpendCurrency(float amount){
        if(amount <= currency){
            currency-=amount;
            return true;
        }else{
            Debug.Log("You don't have enough money!");
            return false;
        }
    }
}

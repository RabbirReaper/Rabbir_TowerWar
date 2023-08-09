using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager_script : MonoBehaviour{
    public static LevelManager_script main;
    public Transform[] WayPoints_list;
    public float Interval=10;
    public float Income=50;
    public float Gold=100;
    public float Next_Income=0;
    public int Hp = 20;
    private void Awake() {
        main = this;    
    }
    private void Update() {
        Next_Income+=Time.deltaTime;
        if(Next_Income > Interval+0.1){
            Gold+=Income;
            Next_Income=0;
        }
    }
    public void IncreaseCurrency(float amount){
        Income+=amount;
    } 
    public bool SpendCurrency(float amount){
        if(amount <= Gold){
            Gold-=amount;
            return true;
        }else{
            Debug.Log("You don't have enough money!");
            return false;
        }
    }
}

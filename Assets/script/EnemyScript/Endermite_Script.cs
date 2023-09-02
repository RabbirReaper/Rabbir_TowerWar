using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endermite_Script : MonoBehaviour{
    [SerializeField] float teleportationSpeed;
    private Enemy_Script thisScript;

    private float hp;
    float temp_hp;
    float tempSpeed;
    float timer = 10000;
    float waitTime = 1;
    Color tempColor;
    private void Start() {
        thisScript = this.GetComponent<Enemy_Script>();
        tempColor = this.GetComponent<Renderer>().material.color;
        hp = thisScript.Health;
    }
    private void Update() {
        temp_hp = thisScript.GetNowHealth();
        waitTime -= Time.deltaTime;
        // Debug.Log(temp_hp);
        if(waitTime < 0 && hp > temp_hp){
            hp = temp_hp;
            waitTime = 1;
            Teleportation();
        }
        timer -= Time.deltaTime;
        if(timer < 0){
            this.GetComponent<Renderer>().material.color = tempColor;
            thisScript.SetNowSpeed(tempSpeed);
            timer = 10000;
        }
    }
    public void Teleportation(){
        Debug.Log("Teleportation");
        Color _tempColor = this.GetComponent<Renderer>().material.color;
        _tempColor.a = 0;
        this.GetComponent<Renderer>().material.color = _tempColor;
        tempSpeed = thisScript.GetNowSpeed();
        thisScript.SetNowSpeed(teleportationSpeed);
        timer = 0.1f;
    }
}

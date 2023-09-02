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
    private void Start() {
        thisScript = this.GetComponent<Enemy_Script>();
        hp = thisScript.Health;
    }
    private void Update() {
        temp_hp = thisScript.GetNowHealth();
        Debug.Log(temp_hp);
        if(hp > temp_hp){
            hp = temp_hp;
            Teleportation();
        }
        timer -= Time.deltaTime;
        if(timer < 0){
            Color tempColor = this.GetComponent<Renderer>().material.color;
            tempColor.a = 255;
            this.GetComponent<Renderer>().material.color = tempColor;
            thisScript.SetNowSpeed(tempSpeed);
            timer = 10000;
        }
    }
    public void Teleportation(){
        Debug.Log("Teleportation");
        Color tempColor = this.GetComponent<Renderer>().material.color;
        tempColor.a = 0;
        this.GetComponent<Renderer>().material.color = tempColor;
        tempSpeed = thisScript.GetNowSpeed();
        thisScript.SetNowSpeed(teleportationSpeed);
        timer = 0.2f;
    }
}

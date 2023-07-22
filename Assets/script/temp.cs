using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : creat_floor{
    [SerializeField] int t;
    private void OnDrawGizmosSelected() {
        if(t==1){
            Debug.Log("Hey");
            t=0;
            Spawn();
        }
    } 

}

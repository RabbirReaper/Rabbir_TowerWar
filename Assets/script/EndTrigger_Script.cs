using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger_Script : MonoBehaviour{
    private void OnTriggerExit2D(Collider2D other) {
        if(other != null){
            LevelManager_script.main.HpUpdate(-1);
            other.transform.position = new Vector3(-22f,Random.Range(7f, 13f),0f);
        }
        
    }
}

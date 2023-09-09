using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger_2_Script : MonoBehaviour{
    private void OnTriggerExit2D(Collider2D other) {
        if(other == null ||  other.GetComponent<Enemy_Script>().isDestory) return;
        if(other.gameObject.layer == LayerMask.GetMask("Ghost","Rider")) return ;
        other.GetComponent<Enemy_Script>().isDestory = true;
        other.GetComponent<Enemy_Script>().CorrectDied();
        LevelManager_script.main.HpUpdate(-1,other.GetComponent<Enemy_Script>().ownPlayer);
    }
}

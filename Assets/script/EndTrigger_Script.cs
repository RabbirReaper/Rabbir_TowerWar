using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class EndTrigger_Script : MonoBehaviour{
    private void OnTriggerExit2D(Collider2D other) {
        if(other == null ||  other.GetComponent<Enemy_Script>().isDestory) return;
        if(other.gameObject.layer == LayerMask.NameToLayer("Ghost")){
            other.transform.position = new Vector3(-22f,Random.Range(7f, 13f),0f);
            Player _player = other.GetComponent<Enemy_Script>().ownPlayer;
            LevelManager_script.main.UpdateEnemyStreet(_player,4,-1);
            LevelManager_script.main.UpdateEnemyStreet(_player,0,1);
        }else{
            other.GetComponent<Enemy_Script>().CorrectDied();
        }
        LevelManager_script.main.HpUpdate(-1,other.GetComponent<Enemy_Script>().ownPlayer);
    }
}

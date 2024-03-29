using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class EndTrigger_Script : MonoBehaviour{
    bool check=true;
    private void OnTriggerExit2D(Collider2D other) {
        if(other == null ||  other.GetComponent<Enemy_Script>().isDestory || LevelManager_script.main.isEnd || check) return;
        if(other.gameObject.layer == LayerMask.NameToLayer("Ghost") || other.gameObject.layer == LayerMask.NameToLayer("Rider")){
            other.transform.position = new Vector3(-22f,Random.Range(7f, 13f),0f);
            Player _player = other.GetComponent<Enemy_Script>().ownPlayer;
            LevelManager_script.main.UpdateEnemyStreet(_player,4,-1);
            LevelManager_script.main.UpdateEnemyStreet(_player,0,1);
            other.GetComponent<Enemy_Script>().SetinEndTrigger(false);
        }else{
        other.GetComponent<Enemy_Script>().isDestory = true;
            other.GetComponent<Enemy_Script>().CorrectDied();
        }
        LevelManager_script.main.HpUpdate(-1,other.GetComponent<Enemy_Script>().ownPlayer);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other == null || other.GetComponent<Enemy_Script>().GetinEndTrigger()) return;
        other.GetComponent<Enemy_Script>().SetinEndTrigger(true);
        check = false;
    }
}

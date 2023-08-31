using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class EndTrigger_Script : MonoBehaviour{
    const int INF = 2147483647;
    private void OnTriggerExit2D(Collider2D other) {
        if(other != null){
            LevelManager_script.main.HpUpdate(-1,other.GetComponent<Enemy_Script>().ownPlayer);
            if(other.gameObject.layer == LayerMask.NameToLayer("Ghost")){
                other.transform.position = new Vector3(-22f,Random.Range(7f, 13f),0f);
                Player _player = other.GetComponent<Enemy_Script>().ownPlayer;
                LevelManager_script.main.UpdateEnemyStreet(_player,4,-1);
                LevelManager_script.main.UpdateEnemyStreet(_player,0,1);
            }else{
                LevelManager_script.main.UpdateEnemyStreet(other.GetComponent<Enemy_Script>().ownPlayer,4,-1);
                Destroy(other);
            }
        }
        
    }
}

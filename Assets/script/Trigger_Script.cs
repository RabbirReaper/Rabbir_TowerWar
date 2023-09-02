using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class Trigger_Script : MonoBehaviour{
    [SerializeField] int rotation;
    [SerializeField] int streetNumber;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            other.GetComponent<Enemy_Script>().UpdateMoveRotation(rotation);
            if(streetNumber != -1 && streetNumber != other.GetComponent<Enemy_Script>().nowStreet){
                other.GetComponent<Enemy_Script>().nowStreet = streetNumber;
                // Debug.Log("temp  " + streetNumber );
                Player _player = other.GetComponent<Enemy_Script>().ownPlayer;
                LevelManager_script.main.UpdateEnemyStreet(_player,streetNumber,+1);
                LevelManager_script.main.UpdateEnemyStreet(_player,streetNumber-1,-1);
            }
        }
    }
}

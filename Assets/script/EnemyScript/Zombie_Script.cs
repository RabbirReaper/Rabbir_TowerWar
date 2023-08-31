using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Script : MonoBehaviour{
    GameObject EnemyParent;
    [SerializeField] GameObject smellZombie;
    private void Start() {
        EnemyParent = LevelManager_script.main.GetComponent<EnemySpawn>().EnemyParent;
    }
    private void Update() {
        if(this.GetComponent<Enemy_Script>().isDestory){
            Debug.Log("I died!!!!");
            for(int i=0;i<3;i++){
                GameObject temp = Instantiate(smellZombie,transform.position,Quaternion.identity);
                temp.transform.SetParent(EnemyParent.transform);
                Enemy_Script tempEnemy_Script = this.GetComponent<Enemy_Script>();
                temp.GetComponent<Enemy_Script>().ownPlayer = tempEnemy_Script.ownPlayer;
                LevelManager_script.main.UpdateEnemyStreet(tempEnemy_Script.ownPlayer,tempEnemy_Script.nowStreet,1);
            }
        }
    }
}

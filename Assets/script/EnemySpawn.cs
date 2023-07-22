using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawn : MonoBehaviour{
    [SerializeField] GameObject[] Enemy_list;
    [SerializeField] int enemy_number;
    int EnemiesAlive=0,EnemiesDied=0;
    private void Update() {
        if(enemy_number!=0){
            Instantiate(Enemy_list[0],LevelManager_script.main.WayPoints_list[0].position,Quaternion.identity);
            enemy_number--;
            EnemiesAlive++;
        }
    }
    public static UnityEvent onEnemyDestory = new UnityEvent();
    private void Awake() {
        onEnemyDestory.AddListener(EnemyDestoryed);
    }

    void EnemyDestoryed(){
        EnemiesAlive--;
        EnemiesDied++;
    }

}

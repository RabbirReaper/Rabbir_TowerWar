using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawn : MonoBehaviour{
    [SerializeField] GameObject[] Enemy_list;
    int EnemiesAlive=0,EnemiesDied=0;
    
    public static UnityEvent onEnemyDestory = new UnityEvent();
    private void Awake() {
        onEnemyDestory.AddListener(EnemyDestoryed);
    }

    void EnemyDestoryed(){
        EnemiesAlive--;
        EnemiesDied++;
    }
    public void _EnemySpawn(int x){
        Instantiate(Enemy_list[0],LevelManager_script.main.WayPoints_list[0].position,Quaternion.identity);
        EnemiesAlive++;
    }
}

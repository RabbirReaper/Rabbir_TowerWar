using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Script : MonoBehaviour{
    GameObject EnemyParent;
    [SerializeField] GameObject smellZombie;
    private void Start() {
        EnemyParent = LevelManager_script.main.GetComponent<EnemySpawn>().EnemyParent;
    }
    
    private void OnDestroy() {
        for(int i=0;i<3;i++){
            if(LevelManager_script.main.isEnd) return;
            GameObject smellZombieOBJ = Instantiate(smellZombie,new Vector3(Random.Range(transform.position.x-1,transform.position.x+1),Random.Range(transform.position.y-1,transform.position.y+1),0 ),Quaternion.identity);
            smellZombieOBJ.transform.SetParent(EnemyParent.transform);
            Enemy_Script thisEnemy_Script = this.GetComponent<Enemy_Script>();
            Enemy_Script _smellZombie = smellZombieOBJ.GetComponent<Enemy_Script>();
            _smellZombie.ownPlayer = thisEnemy_Script.ownPlayer;
            _smellZombie.moveRotation = thisEnemy_Script.moveRotation;
            _smellZombie.nowStreet = thisEnemy_Script.nowStreet;
            _smellZombie.SetEnemyId(0);
            LevelManager_script.main.UpdateEnemyStreet(thisEnemy_Script.ownPlayer,thisEnemy_Script.nowStreet,1);
        }
    }
}

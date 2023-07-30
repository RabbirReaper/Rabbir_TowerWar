using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour{
    [SerializeField] float HealthPoint;
    [SerializeField] float currencyWorth;
    bool isDestory = false;
    public void TakeDamage(float dmg){
        HealthPoint -= dmg;
        if(HealthPoint <= 0 && !isDestory){
            isDestory=true;
            LevelManager_script.main.IncreaseCurrency(currencyWorth);
            EnemySpawn.onEnemyDestory.Invoke();
            Destroy(gameObject);
        }
    }
}

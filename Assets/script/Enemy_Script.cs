using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour{//
    [Header("Enemy References")]
    [SerializeField] float health;
    [SerializeField] int currencyWorth;
    [SerializeField] float speed;
    bool isDestory = false;
    public bool isSlowed = false;
    Transform target;
    int WayPointidx=0;
    float nowSpeed;
    private void Start() {
        target = LevelManager_script.main.WayPoints_list[0];
        nowSpeed=speed;
    }
    void Update(){
        transform.position=Vector2.MoveTowards(transform.position,target.position,nowSpeed*Time.deltaTime);
        if(Vector2.Distance(transform.position,target.position) < 0.01f){
            if(WayPointidx<LevelManager_script.main.WayPoints_list.Length-1){
                target=LevelManager_script.main.WayPoints_list[++WayPointidx];
            }else{
                WayPointidx=0;
                transform.position=LevelManager_script.main.WayPoints_list[0].position;
                target=LevelManager_script.main.WayPoints_list[WayPointidx];
            }
        }
        if(isSlowed){
            timer-=Time.deltaTime;
            if(timer<=0) ResetSpeed();
        }
    }
    public void TakeDamage(float dmg){
        health -= dmg;
        if(!isDestory && health <= 0){
            isDestory=true;
            LevelManager_script.main.IncreaseCurrency(currencyWorth);
            EnemySpawn.onEnemyDestory.Invoke();
            Destroy(gameObject);
        }
    }
    float timer;
    public void UpdateSpeed(float x,float waitTime){
        x= 1 - x/100;
        nowSpeed = speed*x;
        timer=waitTime;
        isSlowed = true;
    }
    public void ResetSpeed(){
        Debug.Log(nowSpeed);
        nowSpeed = speed;
        isSlowed = false;
    }
}

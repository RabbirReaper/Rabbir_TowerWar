using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour{//
    [Header("Enemy References")]
    [SerializeField] float Health;
    [SerializeField] float defence;
    [SerializeField] int currencyWorth;
    [SerializeField] float speed;
    bool isDestory = false;
    public bool isSlowed = false;
    public bool isFire = false;

    float fireInterval = 0.5f ; //Important
    float timer_slow;
    float timer_fire;
    float fireRate=0;
    float TimerTemp=0;
    Transform target;
    int WayPointidx=0;
    float nowSpeed;
    float nowHealth;
    private void Start() {
        target = LevelManager_script.main.WayPoints_list[0];
        nowSpeed=speed;
        nowHealth=Health;
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
            timer_slow-=Time.deltaTime;
            if(timer_slow < 0) ResetSpeed();
        }
        if(isFire){
            timer_fire-=Time.deltaTime;
            TimerTemp+=Time.deltaTime;
            if(TimerTemp >= fireInterval){
                FireDamage(fireRate);
                TimerTemp=0;
            }
            if(timer_fire < 0) isFire=false;
        }
    }
    void FireDamage(float _fireDmg){
        nowHealth-=Health*_fireDmg;
        if(!isDestory && nowHealth <= 0){
            isDestory=true;
            LevelManager_script.main.IncreaseCurrency(currencyWorth);
            EnemySpawn.onEnemyDestory.Invoke();
            Destroy(gameObject);
        }
    }
    public void TakeDamage(float dmg){
        if(dmg > defence){
            nowHealth -= (dmg-defence);
        }else nowHealth--;
        if(!isDestory && nowHealth <= 0){
            isDestory=true;
            LevelManager_script.main.IncreaseCurrency(currencyWorth);
            EnemySpawn.onEnemyDestory.Invoke();
            Destroy(gameObject);
        }
    }
    public void UpdateSpeed(float x,float waitTime){
        x= 1 - x/100;
        nowSpeed = speed*x;
        timer_slow=waitTime;
        isSlowed = true;
    }
    public void ResetSpeed(){
        Debug.Log(nowSpeed);
        nowSpeed = speed;
        isSlowed = false;
    }
    public void UpdateFire(float _fireRate,float continuedFiretime){
        if(continuedFiretime == 0) return;
        fireRate = _fireRate;
        timer_fire = continuedFiretime;
        isFire = true;
    }
}

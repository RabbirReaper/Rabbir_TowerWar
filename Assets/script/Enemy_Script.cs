using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Enemy_Script : MonoBehaviour{//
    [Header("Enemy References")]
    [SerializeField] float Health;
    [SerializeField] float Defence;
    [SerializeField] int currencyWorth;
    [SerializeField] float speed;
    [SerializeField] TextMeshProUGUI hpUI;
    [SerializeField] TextMeshProUGUI defenceUI;
    bool isDestory = false;
    public bool isSlowed = false;
    public bool isFire = false;
    public bool isWeak = false;

    float fireInterval = 0.5f ; //Important
    float timer_slow;
    float timer_fire;
    float timer_weak;
    float fireRate=0;
    float TimerTemp=0;
    Transform target;
    int WayPointidx=0;
    float nowSpeed;
    float nowHealth;
    float nowDefence;
    private void Start() {
        target = LevelManager_script.main.WayPoints_list[0];
        nowSpeed=speed;
        nowHealth=Health;
        nowDefence=Defence;
        hpUI.text = ((int)Health).ToString() + "/" +((int)Health).ToString();
        defenceUI.text = ((int)Defence).ToString() + "/" +((int)Defence).ToString();
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
        if(isWeak){
            timer_weak-=Time.deltaTime;
            if(timer_weak < 0){
                nowDefence=Defence;
                isWeak = false;
            }
            defenceUI.text = ((int)nowDefence).ToString() + "/" +((int)Defence).ToString();
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
        hpUI.text = ((int)nowHealth).ToString() + "/" +((int)Health).ToString();
    }
    public void TakeDamage(float dmg){
        if(dmg > nowDefence){
            nowHealth -= (dmg-nowDefence);
        }else nowHealth--;
        if(!isDestory && nowHealth <= 0){
            isDestory=true;
            LevelManager_script.main.IncreaseCurrency(currencyWorth);
            EnemySpawn.onEnemyDestory.Invoke();
            Destroy(gameObject);
        }
        hpUI.text = ((int)nowHealth).ToString() + "/" +((int)Health).ToString();
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
    public void UpdateWeak(float _weakRate,float continueTime){
        timer_weak=continueTime;
        nowDefence=(1 - _weakRate)*Defence;
        isWeak = true;
    }
}

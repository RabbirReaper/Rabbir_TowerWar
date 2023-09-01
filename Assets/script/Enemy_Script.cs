using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using System;

public class Enemy_Script : MonoBehaviour{//
    [Header("Enemy References")]
    [SerializeField] float Health;
    [SerializeField] float Defence;
    public int cost; 
    [SerializeField] int currencyWorth;
    [SerializeField] float speed;
    [SerializeField] TextMeshProUGUI hpUI;
    [SerializeField] TextMeshProUGUI defenceUI;


    public bool isDestory = false;
    public bool isSlowed = false;
    public bool isFire = false;
    public bool isWeak = false;

    float fireInterval = 1f ; //Important
    float timer_slow;
    float timer_slow_all=0;
    float timer_fire;
    float timer_weak;
    float fireRate=0;
    float TimerTemp=0;
    // Transform target;
    // int WayPointidx=0;
    float nowSpeed;
    float nowHealth;
    float nowDefence;
    public int moveRotation=1;
    public Player ownPlayer;
    public int nowStreet = 0;
    List<(float,float)> slowSchedule = new();
    private void Start() {
        // target = LevelManager_script.main.WayPoints_list[1];
        isDestory = false;
        nowSpeed=speed;
        nowHealth=Health;
        nowDefence=Defence;
        hpUI.text = ((int)Health).ToString() + "/" +((int)Health).ToString();
        defenceUI.text = ((int)Defence).ToString() + "/" +((int)Defence).ToString();
    }
    void Update(){
        // transform.position=Vector2.MoveTowards(transform.position,target.position,nowSpeed*Time.deltaTime);
        if(moveRotation == 1) transform.position = new Vector3(transform.position.x+nowSpeed*Time.deltaTime,transform.position.y,transform.position.z);
        else if(moveRotation == 2) transform.position = new Vector3(transform.position.x,transform.position.y-nowSpeed*Time.deltaTime,transform.position.z);
        else if(moveRotation == 3) transform.position = new Vector3(transform.position.x-nowSpeed*Time.deltaTime,transform.position.y,transform.position.z);
        else if(moveRotation == 4) transform.position = new Vector3(transform.position.x,transform.position.y+nowSpeed*Time.deltaTime,transform.position.z);
        // if(Vector2.Distance(transform.position,target.position) < 2.5f){
        //     if(WayPointidx<LevelManager_script.main.WayPoints_list.Length-1){
        //         target=LevelManager_script.main.WayPoints_list[++WayPointidx];
        //     }else{
        //         WayPointidx=0;
        //         LevelManager_script.main.HpUpdate(-1);
        //         transform.position=LevelManager_script.main.WayPoints_list[0].position;
        //         target=LevelManager_script.main.WayPoints_list[WayPointidx];
        //     }
        // }
        if(isSlowed){
            timer_slow_all += Time.deltaTime;
            if(timer_slow_all >= timer_slow) ResetSpeed();
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

    public void UpdateMoveRotation(int x){
        moveRotation = x;
    }
    void FireDamage(float _fireDmg){
        nowHealth-=Health*_fireDmg;
        if(!isDestory && nowHealth <= 0){
            isDestory=true;
            LevelManager_script.main.IncreaseIncome(currencyWorth);
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
            LevelManager_script.main.IncreaseIncome(currencyWorth);
            EnemySpawn.onEnemyDestory.Invoke();
            LevelManager_script.main.UpdateEnemyStreet(ownPlayer,nowStreet,-1);
            Destroy(gameObject);
        }
        hpUI.text = ((int)nowHealth).ToString() + "/" +((int)Health).ToString();
    }

    public void UpdateSpeed(float x,float waitTime){
        slowSchedule.Add((x,waitTime+timer_slow_all));
        slowSchedule.Sort(Cmp);
        nowSpeed = speed*(1 - slowSchedule[0].Item1/100);
        timer_slow=slowSchedule[0].Item2;
        isSlowed = true;
        Debug.Log(nowSpeed);
    }
    public void ResetSpeed(){
        slowSchedule.RemoveAt(0);
        if(slowSchedule.Count == 0){
            nowSpeed = speed;
            isSlowed = false;
            timer_slow_all = 0;
        }else{
            nowSpeed = speed *(1 - slowSchedule[0].Item1/100);
            timer_slow = slowSchedule[0].Item2;
        }
        Debug.Log(nowSpeed);
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

    static int Cmp((float, float) x, (float, float) y)    {
        if (x.Item1 != y.Item1){
            return x.Item1 > y.Item1 ? -1 : 1;
        }
        else{
            return x.Item2 < y.Item2 ? -1 : 1;
        }
    }
}

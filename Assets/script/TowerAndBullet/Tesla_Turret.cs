using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tesla_Turret : MonoBehaviour{
    [SerializeField] GameObject lightning;
    [SerializeField] float Bullet_speed;
    public float Bullet_Damage;

    LayerMask EnemyMask;
    LayerMask shieldMask;

    
    public float AttackRange;
    public float reload;
    public int sellValue;
    Transform target;
    float timeUntilFire=0;
    GameObject nowLightning;
    float damageBuff=1;
    float damageBuffRate=1.15f;
    float damageBuffLimit = 2;


    private void Start() {
        EnemyMask = LayerMask.GetMask("Enemy","Ghost");
        shieldMask = LayerMask.GetMask("Shield");
        nowLightning = Instantiate(lightning,this.transform);
    }
    private void Update() {
        timeUntilFire -= Time.deltaTime;
        if(target == null){
            damageBuff = 1;
            FindTarget();
            nowLightning.transform.position=Vector2.MoveTowards(nowLightning.transform.position,this.transform.position,Bullet_speed*Time.deltaTime);
            return;
        }
        if(!CheckTargetinRange()){
            target=null;
        }else{
            if(timeUntilFire <= 0){
                Shoot();
                damageBuff*=damageBuffRate;
                if(damageBuff > damageBuffLimit) damageBuff = damageBuffLimit;
                if(ShieldInRange()){
                    timeUntilFire= reload*0.9f;
                }else timeUntilFire=reload;
                Debug.Log(timeUntilFire);
            }
        }
    }
    private void FixedUpdate() {
        if(target == null) return;
        nowLightning.transform.position=Vector2.MoveTowards(nowLightning.transform.position,target.position,Bullet_speed*Time.deltaTime);
    }

    void Shoot(){
        target.gameObject.GetComponent<Enemy_Script>().TakeDamage(Bullet_Damage);
    }

    bool CheckTargetinRange(){
        return Vector2.Distance(target.position,transform.position) <= AttackRange;
    }

    void FindTarget(){
        RaycastHit2D hits = Physics2D.CircleCast(transform.position,AttackRange,(Vector2)transform.position,0f,EnemyMask);
        target = hits.transform;
    }

    

    public void SellingTower(){
        LevelManager_script.main.IncreaseGold(sellValue);
        UIManager.main.SetHoveringStatie(false);
        Destroy(this.gameObject);
    }

    bool ShieldInRange(){
        Collider2D[] inRange = Physics2D.OverlapCircleAll(transform.position,10,shieldMask);
        Debug.Log("hey");
        foreach (var item in inRange){
            if(item.GetComponent<Villager_Turret>().level == 2) return true;
            Debug.Log(item.GetComponent<Villager_Turret>().level + "!!!");
        }
        return false;
    } 
    // private void OnDrawGizmosSelected() {
    //     Handles.color=Color.blue;
    //     Handles.DrawWireDisc(transform.position,transform.forward,AttackRange);
    // }

}

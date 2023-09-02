using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    float damageBuffRate=1.05f;
    [SerializeField] float damageBuffLimit;
    [SerializeField] GameObject slowImage;

    int slowCount = 0;
    float slowRate;
    [SerializeField] GameObject brokenImage;
    int brokenCount = 0;

    private void Start() {
        EnemyMask = LayerMask.GetMask("Enemy","Ghost");
        shieldMask = LayerMask.GetMask("Shield");
        nowLightning = Instantiate(lightning,this.transform);
    }
    private void Update() {
        timeUntilFire -= Time.deltaTime;
        if(target == null){
            nowLightning.transform.position=Vector2.MoveTowards(nowLightning.transform.position,transform.position,Bullet_speed*Time.deltaTime);
            damageBuff = 1;
            FindTarget();
            return;
        }//這裡很重要 以前的bug原因:他被判定在攻擊範圍內 可是判斷不在範圍內
        if(!CheckTargetinRange()){
            target=null;
        }else{
            nowLightning.transform.position=Vector2.MoveTowards(nowLightning.transform.position,target.position,Bullet_speed*Time.deltaTime);
            if(timeUntilFire <= 0){
                if(brokenCount == 0){
                    Shoot();
                    damageBuff*=damageBuffRate;
                    if(damageBuff > damageBuffLimit) damageBuff = damageBuffLimit;
                }else{
                    Debug.Log("Is Broken");
                    brokenCount--;
                    if(brokenCount == 0) brokenImage.SetActive(false);
                } 
                if(ShieldL2InRange()){
                    timeUntilFire= reload*0.9f;
                }else if(slowCount != 0){
                    slowCount--;
                    timeUntilFire = reload*(1 + slowRate);
                    if(slowCount == 0) slowImage.SetActive(false);
                }else timeUntilFire=reload;
                Debug.Log(timeUntilFire);
            }
        }
    }

    void Shoot(){
        target.gameObject.GetComponent<Enemy_Script>().TakeDamage(Bullet_Damage * damageBuff);
        Debug.Log(Bullet_Damage * damageBuff);
    }

    bool CheckTargetinRange(){
        return Vector2.Distance(target.position,transform.position) < AttackRange;
    }

    void FindTarget(){
        RaycastHit2D hits = Physics2D.CircleCast(transform.position,AttackRange,transform.position,0f,EnemyMask);
        target = hits.transform;
    }

    

    public void SellingTower(){
        LevelManager_script.main.IncreaseGold(sellValue);
        UIManager.main.SetHoveringStatie(false);
        Destroy(this.gameObject);
    }

    bool ShieldL2InRange(){
        Collider2D[] inRange = Physics2D.OverlapCircleAll(transform.position,10,shieldMask);
        foreach (var item in inRange){
            if(item.GetComponent<Villager_Turret>().level == 2) return true;
        }
        return false;
    } 
    bool ShieldL1InRange(){
        Collider2D inRange = Physics2D.OverlapCircle(transform.position,5,shieldMask);
        if(inRange == null) return false;
        return true;
    }
    public void SlowTurret(float _slowRate,int _slowCount){
        if(ShieldL1InRange() || ShieldL2InRange()) return;
        slowImage.SetActive(true);
        slowCount = _slowCount;
        slowRate = _slowRate;
    }

    public void UpdateIsborken(int _brokenCount){
        if(ShieldL2InRange()) return;
        brokenImage.SetActive(true);
        brokenCount = _brokenCount;
    }
    // private void OnDrawGizmosSelected() {
    //     Handles.color=Color.blue;
    //     Handles.DrawWireDisc(transform.position,transform.forward,AttackRange);
    // }

}

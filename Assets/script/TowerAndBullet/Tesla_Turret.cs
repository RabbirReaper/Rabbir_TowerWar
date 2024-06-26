using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class Tesla_Turret : MonoBehaviour{
    [SerializeField] ParticleSystem boomParticle;
    [SerializeField] GameObject lightning;
    float Bullet_speed;
    [SerializeField] bool keepBuff;
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
    const float damageBuffRate=1.25f;
    [SerializeField] float damageBuffLimit;
    [SerializeField] GameObject slowImage;
    [SerializeField] GameObject attackRangeImage;

    int slowCount = 0;
    float slowRate;
    [SerializeField] GameObject brokenImage;
    int brokenCount = 0;

    private void Start() {
        EnemyMask = LayerMask.GetMask("Enemy","Ghost","Rider");
        shieldMask = LayerMask.GetMask("Shield");
        nowLightning = Instantiate(lightning,this.transform);
        float temp = AttackRange*2;
        attackRangeImage.transform.localScale = new Vector3(temp,temp,temp);
    }
    private void Update() {
        timeUntilFire -= Time.deltaTime;
        if(target == null){
            nowLightning.transform.position=Vector2.MoveTowards(nowLightning.transform.position,transform.position,12*Time.deltaTime);
            if(!keepBuff) damageBuff = 1;
            FindTarget();
            return;
        }//這裡很重要 以前的bug原因:他被判定在攻擊範圍內 可是判斷不在範圍內
        if(!CheckTargetinRange()){
            target=null;
        }else{
            UpdateBulletSpeed();
            nowLightning.transform.position=Vector2.MoveTowards(nowLightning.transform.position,target.position,Bullet_speed*Time.deltaTime);
            if(timeUntilFire <= 0){
                if(brokenCount == 0){
                    Shoot();
                    damageBuff*=damageBuffRate;
                    if(damageBuff > damageBuffLimit) damageBuff = damageBuffLimit;
                }else{
                    // Debug.Log("Is Broken");
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
                // Debug.Log(timeUntilFire);
            }
        }
    }

    void Shoot(){
        target.gameObject.GetComponent<Enemy_Script>().TakeDamage(Bullet_Damage * damageBuff);
        Instantiate(boomParticle,target.transform.position,boomParticle.transform.rotation);
        Debug.Log(Bullet_Damage * damageBuff);
    }

    bool CheckTargetinRange(){
        return Vector2.Distance(target.position,transform.position) < AttackRange;
    }

    void FindTarget(){
        // RaycastHit2D hits = Physics2D.CircleCast(transform.position,AttackRange,transform.position,0f,EnemyMask);
        // target = hits.transform;
        Collider2D[] inRange = Physics2D.OverlapCircleAll(transform.position,AttackRange,EnemyMask);
        if(inRange.Length != 0) target = inRange[0].transform;
        for(int i=1;i<(int)inRange.Length;i++){
            if(inRange[i] == null) continue;
            if(target.GetComponent<Enemy_Script>().GetMoveDistance() > inRange[i].GetComponent<Enemy_Script>().GetMoveDistance()){
                target = inRange[i].transform;
            }
        }
    }

    

    public void SellingTower(){
        LevelManager_script.main.IncreaseGold(sellValue);
        UIManager.main.SetHoveringStatie(false);
        LevelManager_script.main.TowerLimitAdd(1);
        LevelManager_script.main.UpdateOpponentBuildCount(5,-1);
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

    void UpdateBulletSpeed(){
        float temp = timeUntilFire;
        if(temp <= 0) temp = 0.1f;
        Bullet_speed = Vector2.Distance(target.position,transform.position)/temp;
        if(Bullet_speed < 12) Bullet_speed = 12;
    }
    // private void OnDrawGizmosSelected() {
    //     Handles.color=Color.blue;
    //     Handles.DrawWireDisc(transform.position,transform.forward,AttackRange);
    // }

}

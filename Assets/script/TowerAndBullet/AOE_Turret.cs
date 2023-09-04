using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
public class AOE_Turret : MonoBehaviour{
    [SerializeField] GameObject barrel;
    public GameObject bulletPrefab;
    
    
    [SerializeField] Transform firingPoint;
    public float AttackRange;
    [SerializeField] float RotationSpeed;
    public float reload;
    public int sellValue;

    Transform target;
    LayerMask EnemyMask;
    LayerMask shieldMask;
    [SerializeField] GameObject brokenImage;
    int brokenCount = 0;
    [SerializeField] GameObject slowImage;
    [SerializeField] GameObject attackRangeImage;

    int slowCount = 0;
    float slowRate;
    

    float timeUntilFire;
    
    private void Start() {
        EnemyMask = LayerMask.GetMask("Enemy","Ghost","Rider");
        shieldMask = LayerMask.GetMask("Shield");
        float temp = AttackRange*2;
        attackRangeImage.transform.localScale = new Vector3(temp,temp,temp);
    }
    private void Update() {
        timeUntilFire -= Time.deltaTime;
        if(target == null){
            FindTarget();
            return;
        }
        RotateTowards();
        if(!CheckTargetinRange()){
            target=null;
        }else{
            if(timeUntilFire <= 0){
                if(brokenCount == 0) Shoot();
                else{
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
        GameObject bulletobj = Instantiate(bulletPrefab,firingPoint.position,Quaternion.identity);
        AOE_Bullet bulletScript = bulletobj.GetComponent<AOE_Bullet>();
        bulletScript.SetTarget(target);
    }

    bool CheckTargetinRange(){
        return Vector2.Distance(target.position,transform.position) <= AttackRange;
    }


    void FindTarget(){
        RaycastHit2D hits = Physics2D.CircleCast(transform.position,AttackRange,(Vector2)transform.position,0f,EnemyMask);
        target = hits.transform;
    }

    void RotateTowards(){
        float angle = Mathf.Atan2(target.position.y - transform.position.y,target.position.x - transform.position.x)*Mathf.Rad2Deg-90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f,0f,angle));
        barrel.transform.rotation = Quaternion.RotateTowards(barrel.transform.rotation,targetRotation,RotationSpeed*Time.deltaTime);
    }

    public void SellingTower(){
        LevelManager_script.main.IncreaseGold(sellValue);
        UIManager.main.SetHoveringStatie(false);
        LevelManager_script.main.towerCountLimit++;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
public class Witch_Turret : MonoBehaviour{

    [SerializeField] GameObject barrel;
    public GameObject bulletPrefab;
    LayerMask EnemyMask;
    
    [SerializeField] Transform firingPoint;
    public float AttackRange;
    [SerializeField] float RotationSpeed;
    public float reload;
    public int sellValue;
    LayerMask shieldMask;
    [SerializeField] GameObject slowImage;
    int slowCount = 0;
    float slowRate;
    [SerializeField] GameObject brokenImage;
    int brokenCount = 0;

    Transform target;
    float timeUntilFire=0;
    private void Start() {
        EnemyMask = LayerMask.GetMask("Enemy","Ghost");
        shieldMask = LayerMask.GetMask("shield");
    }
    private void Update() {
        timeUntilFire -= Time.deltaTime;
        // FindTarget();
        // if(target == null) return;
        // RotateTowards();
        // if(!CheckTargetinRange()){
        //     target=null;
        // }else{
        //     if(timeUntilFire < 0){
        //         Shoot();
        //         timeUntilFire=reload;
        //     }
        // }
        if(target != null){
            RotateTowards();
            if(!CheckTargetinRange()) target = null;
        } 
        if(timeUntilFire < 0){
            FindTarget();
            if(target == null) return;
            // if(!CheckTargetinRange()){
            //     target=null;
            //     return;
            // }
            if(brokenCount == 0) Shoot();
                else{
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

    void Shoot(){
        GameObject bulletobj = Instantiate(bulletPrefab,firingPoint.position,Quaternion.identity);
        Witch_Bullet bulletScript = bulletobj.GetComponent<Witch_Bullet>();
        bulletScript.SetTarget(target);
    }

    bool CheckTargetinRange(){
        return Vector2.Distance(target.position,transform.position) <= AttackRange;
    }

    void FindTarget(){
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position,AttackRange,(Vector2)transform.position,0f,EnemyMask);
        if(hits.Length != 0) target = hits[0].transform;
        for(int i=0;i<hits.Length;i++){
            if(!hits[i].transform.GetComponent<Enemy_Script>().isWeak){
                target = hits[i].transform;
                break;
            }
        }
    }

    void RotateTowards(){
        float angle = Mathf.Atan2(target.position.y - barrel.transform.position.y,target.position.x - barrel.transform.position.x)*Mathf.Rad2Deg-90f;
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

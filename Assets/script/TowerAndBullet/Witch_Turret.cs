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


    Transform target;
    float timeUntilFire=0;
    private void Start() {
        EnemyMask = LayerMask.GetMask("Enemy");
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
            Shoot();
            if(ShieldInRange()){
                timeUntilFire= reload*0.9f;
            }else timeUntilFire=reload;
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
        for(int i=1;i<hits.Length;i++){
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
        Destroy(this.gameObject);
    }

    bool ShieldInRange(){
        Collider2D[] inRange = Physics2D.OverlapCircleAll(transform.position,10,shieldMask);
        foreach (var item in inRange){
            if(item.GetComponent<Villager_Turret>().level == 2) return true;
        }
        return false;
    } 
    // private void OnDrawGizmosSelected() {
    //     Handles.color=Color.blue;
    //     Handles.DrawWireDisc(transform.position,transform.forward,AttackRange);
    // }

}

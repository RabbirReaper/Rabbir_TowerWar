using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
public class Slime_Turret : MonoBehaviour{
    [SerializeField] GameObject barrel;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] LayerMask EnemyMask;
    
    [SerializeField] Transform firingPoint;
    [SerializeField] float AttackRange;
    [SerializeField] float RotationSpeed;
    [SerializeField] float reload;
    Transform target;
    
    float timeUntilFire;
    private void Update() {
        FindTarget();
        if(target == null) return;
        timeUntilFire -= Time.deltaTime;
        RotateTowards();
        if(!CheckTargetinRange()){
            target=null;
        }else{
            if(timeUntilFire <= 0){
                Shoot();
                timeUntilFire=reload;
            }
        }
    }

    void Shoot(){
        GameObject bulletobj = Instantiate(bulletPrefab,firingPoint.position,Quaternion.identity);
        Slime_Bullet bulletScript = bulletobj.GetComponent<Slime_Bullet>();
        bulletScript.SetTarget(target);
    }

    bool CheckTargetinRange(){
        return Vector2.Distance(target.position,transform.position) <= AttackRange;
    }


    void FindTarget(){
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position,AttackRange,(Vector2)transform.position,0f,EnemyMask);
        bool nerverIsSlowed = true;
        for(int i=0;i<(int)hits.Length;i++){
            if(hits[i].transform.GetComponent<Enemy_Script>().isSlowed == false){
                target = hits[i].transform;
                nerverIsSlowed = false;
                break;
            }
        }
        if(nerverIsSlowed && hits.Length > 0) target = hits[0].transform;
    }

    void RotateTowards(){
        float angle = Mathf.Atan2(target.position.y - transform.position.y,target.position.x - transform.position.x)*Mathf.Rad2Deg-90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f,0f,angle));
        barrel.transform.rotation = Quaternion.RotateTowards(barrel.transform.rotation,targetRotation,RotationSpeed*Time.deltaTime);
    }
    private void OnDrawGizmosSelected() {
        Handles.color=Color.blue;
        Handles.DrawWireDisc(transform.position,transform.forward,AttackRange);
    }
    
}

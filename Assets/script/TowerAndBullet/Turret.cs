using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
public class Turret : MonoBehaviour{

    [SerializeField] GameObject barrel;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] LayerMask EnemyMask;
    
    [SerializeField] Transform firingPoint;
    [SerializeField] float AttackRange;
    [SerializeField] float RotationSpeed;
    [SerializeField] float reload;
    Transform target;
    float timeUntilFire=0;
   
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
                Shoot();
                timeUntilFire=reload;
            }
        }
    }

    void Shoot(){
        GameObject bulletobj = Instantiate(bulletPrefab,firingPoint.position,Quaternion.identity);
        Bullet bulletScript = bulletobj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

    bool CheckTargetinRange(){
        return Vector2.Distance(target.position,transform.position) <= AttackRange;
    }

    void FindTarget(){
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position,AttackRange,(Vector2)transform.position,0f,EnemyMask);
        if(hits.Length > 0){
            target = hits[0].transform;
        }
    }

    void RotateTowards(){
        float angle = Mathf.Atan2(target.position.y - barrel.transform.position.y,target.position.x - barrel.transform.position.x)*Mathf.Rad2Deg-90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f,0f,angle));
        barrel.transform.rotation = Quaternion.RotateTowards(barrel.transform.rotation,targetRotation,RotationSpeed*Time.deltaTime);
    }

    private void OnDrawGizmosSelected() {
        Handles.color=Color.blue;
        Handles.DrawWireDisc(transform.position,transform.forward,AttackRange);
    }

}

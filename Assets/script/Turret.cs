using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Turret : MonoBehaviour{
    [SerializeField] float AttackRange;
    [SerializeField] LayerMask EnemyMask;
    [SerializeField] float RotationSpeed;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firingPoint;
    [SerializeField] float bps; // Bullets per second
    Transform target;
    float timeUntilFire;
    private void Update() {
        if(target == null){
            FindTarget();
            return;
        }
        RotateTowards();
        if(!CheckTargetinRange()){
            target=null;
        }else{
            timeUntilFire += Time.deltaTime;
            if(timeUntilFire >= 1f/bps){
                Shoot();
                timeUntilFire=0f;
            }
        }
    }

    void Shoot(){
        GameObject bulletobj = Instantiate(bulletPrefab,firingPoint.position,Quaternion.identity);
        Bullet_Normal bulletScript = bulletobj.GetComponent<Bullet_Normal>();
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
        float angle = Mathf.Atan2(target.position.y - transform.position.y,target.position.x - transform.position.x)*Mathf.Rad2Deg-90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f,0f,angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation,targetRotation,RotationSpeed*Time.deltaTime);
    }

    private void OnDrawGizmosSelected() {
        Handles.color=Color.blue;
        Handles.DrawWireDisc(transform.position,transform.forward,AttackRange);
    }

}

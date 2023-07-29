using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
public class Turret : MonoBehaviour{

    [SerializeField] GameObject barrel;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject upgradeUI;
    [SerializeField] GameObject nextLevelTower;
    [SerializeField] Button upgradeButton;
    [SerializeField] LayerMask EnemyMask;
    
    [SerializeField] Transform firingPoint;
    [SerializeField] float AttackRange;
    [SerializeField] float RotationSpeed;
    [SerializeField] float bps; // Bullets per second
    [SerializeField] int level;
    [SerializeField] float baseUpGradeCost;
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
        float angle = Mathf.Atan2(target.position.y - barrel.transform.position.y,target.position.x - barrel.transform.position.x)*Mathf.Rad2Deg-90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f,0f,angle));
        barrel.transform.rotation = Quaternion.RotateTowards(barrel.transform.rotation,targetRotation,RotationSpeed*Time.deltaTime);
    }

    public void OpenUpgradeUI(){
        upgradeUI.SetActive(true);
    }
    public void CloseUpgradeUI(){
        upgradeUI.SetActive(false);
    }

    public void Upgrade(){
        if(level >= 3) return;
        if(baseUpGradeCost > LevelManager_script.main.Gold) return;
        LevelManager_script.main.SpendCurrency(baseUpGradeCost);
        GetComponentInParent<Plot>().TowerUpdate(nextLevelTower);
        UIManager.main.SetHoveringStatie(false);
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected() {
        Handles.color=Color.blue;
        Handles.DrawWireDisc(transform.position,transform.forward,AttackRange);
    }

}

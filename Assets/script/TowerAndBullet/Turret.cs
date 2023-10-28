using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class Turret : MonoBehaviour{

    [SerializeField] GameObject barrel;
    public GameObject bulletPrefab;
    LayerMask EnemyMask;

    LayerMask shieldMask;

    
    [SerializeField] Transform firingPoint;
    public float AttackRange;
    [SerializeField] float RotationSpeed;
    public float reload;
    public int sellValue;
    Transform target;
    Vector2 bulletDirection;
    float timeUntilFire=0;
    [SerializeField] GameObject slowImage;
    [SerializeField] GameObject brokenImage;
    [SerializeField] GameObject attackRangeImage;
    int slowCount = 0;
    float slowRate;
    int brokenCount = 0;
    [SerializeField] ParticleSystem shootParticle;

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
                    if(slowCount != 0){
                        slowCount = 0;
                        slowImage.SetActive(false);
                    }
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
        Bullet bulletScript = bulletobj.GetComponent<Bullet>();
        bulletScript.SetTarget(target,bulletDirection);
        StartCoroutine(BeginShootParticle());
    }
    IEnumerator BeginShootParticle(){
        shootParticle.Play();
        yield return new WaitForSeconds(0.2f);
        shootParticle.Stop();
    }

    bool CheckTargetinRange(){
        return Vector2.Distance(target.position,transform.position) <= AttackRange;
    }

    void FindTarget(){
        RaycastHit2D hits = Physics2D.CircleCast(transform.position,AttackRange,(Vector2)transform.position,0f,EnemyMask);
        target = hits.transform;
        if(target!=null) bulletDirection = (target.position - transform.position).normalized;
    }

    void RotateTowards(){
        float angle = Mathf.Atan2(target.position.y - barrel.transform.position.y,target.position.x - barrel.transform.position.x)*Mathf.Rad2Deg-90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f,0f,angle));
        barrel.transform.rotation = Quaternion.RotateTowards(barrel.transform.rotation,targetRotation,RotationSpeed*Time.deltaTime);
    }

    public void SellingTower(){
        LevelManager_script.main.IncreaseGold(sellValue);
        UIManager.main.SetHoveringStatie(false);
        LevelManager_script.main.TowerLimitAdd(1);
        LevelManager_script.main.UpdateOpponentBuildCount(0,-1);
        Destroy(this.gameObject);
    }

    bool ShieldL2InRange(){
        Collider2D[] inRange = Physics2D.OverlapCircleAll(transform.position,10,shieldMask);
        foreach (var item in inRange){
            if(item.GetComponent<Villager_Turret>().level == 2) return true;
            Debug.Log(item.GetComponent<Villager_Turret>().level + "!!!");
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
        Debug.Log(" I slow");
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

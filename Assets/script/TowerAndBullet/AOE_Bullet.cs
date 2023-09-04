using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE_Bullet : MonoBehaviour{
    [SerializeField] Rigidbody2D Rb;
    LayerMask EnemyMask;
    [SerializeField] float Bullet_speed;
    public float Bullet_Damage;
    [SerializeField] float fireRate;
    [SerializeField] float fireTime;
    public float splashRange;
    Transform Target;
    [SerializeField] bool stun;
    bool isDestory = false;
    private void Start() {
        EnemyMask = LayerMask.GetMask("Enemy","Ghost","Rider");
    }
    public void SetTarget(Transform _Target){
        Target =_Target;
    }
    private void FixedUpdate() {
        if(!Target) return;
        Vector2 direction = (Target.position - transform.position).normalized;
        Rb.velocity = direction * Bullet_speed; 
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(!isDestory){
            isDestory=true;
            gameObject.SetActive(false);
            // other.gameObject.GetComponent<Enemy_Script>().TakeDamage(Bullet_Damage);
            Collider2D[] inRange = Physics2D.OverlapCircleAll(transform.position,splashRange,EnemyMask);
            for(int i=0;i<(int)inRange.Length;i++){
                if(inRange[i] == null) continue;
                inRange[i].gameObject.GetComponent<Enemy_Script>().TakeDamage(Bullet_Damage);
                if(stun && Random.Range(1,100) <= 3){
                    inRange[i].gameObject.GetComponent<Enemy_Script>().UpdateSpeed(100,100);
                }
                if(fireTime != 0) inRange[i].gameObject.GetComponent<Enemy_Script>().UpdateFire(fireRate,fireTime);
            }
            Destroy(gameObject);
        }
        
    }
    
}

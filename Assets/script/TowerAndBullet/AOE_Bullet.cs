using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE_Bullet : MonoBehaviour{
    [SerializeField] Rigidbody2D Rb;
    [SerializeField] LayerMask EnemyMask;
    [SerializeField] float Bullet_speed;
    [SerializeField] float Bullet_Damage;
    [SerializeField] float fireRate;
    [SerializeField] float fireTime;
    [SerializeField] float splashRange;
    Transform Target;
    bool isDestory = false;
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
                // Enemy_Script em=inRange[i].gameObject.GetComponent<Enemy_Script>();
                // em.UpdateSpeed(fireRate,splashRange);
                inRange[i].gameObject.GetComponent<Enemy_Script>().TakeDamage(Bullet_Damage);
                inRange[i].gameObject.GetComponent<Enemy_Script>().UpdateFire(fireRate,fireTime);
            }
            Destroy(gameObject);
        }
        
    }
    
}

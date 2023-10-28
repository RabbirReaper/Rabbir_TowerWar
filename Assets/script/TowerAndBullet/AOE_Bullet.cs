using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE_Bullet : MonoBehaviour{
    [SerializeField] ParticleSystem boomParticle;
    [SerializeField] Rigidbody2D Rb;
    LayerMask EnemyMask;
    [SerializeField] float Bullet_speed;
    public float Bullet_Damage;
    [SerializeField] float fireRate;
    [SerializeField] float fireTime;
    public float splashRange;
    Transform Target;
    Vector2 direction;
    [SerializeField] bool stun;
    bool isDestory = false;
    private void Start() {
        EnemyMask = LayerMask.GetMask("Enemy","Ghost","Rider");
    }
    public void SetTarget(Transform _Target,Vector2 _direction){
        Target =_Target;
        Rb.velocity = _direction * Bullet_speed; 
    }
    private void FixedUpdate() {
        if(!Target) return;
        direction = (Target.position - transform.position).normalized;
        Rb.velocity = direction * Bullet_speed; 
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(!isDestory){
            isDestory=true;
            gameObject.SetActive(false);
            Instantiate(boomParticle,transform.position,boomParticle.transform.rotation);
            // other.gameObject.GetComponent<Enemy_Script>().TakeDamage(Bullet_Damage);
            Collider2D[] inRange = Physics2D.OverlapCircleAll(transform.position,splashRange,EnemyMask);
            for(int i=0;i<inRange.Length;i++){
                if(inRange[i] == null) continue;
                inRange[i].gameObject.GetComponent<Enemy_Script>().TakeDamage(Bullet_Damage);
                if(stun && other.gameObject.GetComponent<Enemy_Script>().GetNowSpeed()!=0  &&Random.Range(1,100) <= 1){
                    inRange[i].gameObject.GetComponent<Enemy_Script>().UpdateSpeed(100,30);
                }
                if(fireTime != 0) inRange[i].gameObject.GetComponent<Enemy_Script>().UpdateFire(fireRate,fireTime);
            }
            Destroy(gameObject);
        }
        
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{

    [SerializeField] Rigidbody2D Rb;
    [SerializeField] ParticleSystem boomParticle;
    public float Bullet_speed;
    public float Bullet_Damage;
    Vector2 direction;
    Transform Target;
    bool isDestory = false;

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
            if(other == null) Destroy(gameObject);
            Instantiate(boomParticle,transform.position,boomParticle.transform.rotation);
            other.gameObject.GetComponent<Enemy_Script>().TakeDamage(Bullet_Damage);
            Destroy(gameObject);
        }
        
    }
}

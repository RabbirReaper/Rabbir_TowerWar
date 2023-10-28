using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch_Bullet : MonoBehaviour{
    [SerializeField] Rigidbody2D Rb;
    [SerializeField] float Bullet_speed;
    public float Bullet_Damage;
    public float weakRate;
    public float weakTime;
    Transform Target;
    Vector2 direction;
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
            Enemy_Script em=other.gameObject.GetComponent<Enemy_Script>();
            if(other == null) Destroy(gameObject);
            em.TakeDamage(Bullet_Damage);
            em.UpdateWeak(weakRate,weakTime);
            Destroy(gameObject);
        }
        
    }
}

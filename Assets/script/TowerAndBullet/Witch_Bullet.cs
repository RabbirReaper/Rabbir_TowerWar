using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch_Bullet : MonoBehaviour{
    [SerializeField] Rigidbody2D Rb;
    [SerializeField] float Bullet_speed;
    [SerializeField] float Bullet_Damage;
    [SerializeField] float weakRate;
    [SerializeField] float weakTime;
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
            Enemy_Script em=other.gameObject.GetComponent<Enemy_Script>();
            em.TakeDamage(Bullet_Damage);
            em.UpdateWeak(weakRate,weakTime);
            Destroy(gameObject);
        }
        
    }
}

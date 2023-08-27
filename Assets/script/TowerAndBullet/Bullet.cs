using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{

    [SerializeField] Rigidbody2D Rb;
    [SerializeField] float Bullet_speed;
    public float Bullet_Damage;
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
            other.gameObject.GetComponent<Enemy_Script>().TakeDamage(Bullet_Damage);
            Destroy(gameObject);
        }
        
    }
}

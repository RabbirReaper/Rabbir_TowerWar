using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Bullet : MonoBehaviour{
    [SerializeField] Rigidbody2D Rb;
    LayerMask EnemyMask;
    [SerializeField] float Bullet_speed;
    public float Bullet_Damage;
    public float slowRate;
    public float slowTime;
    public float slowRange;
    Transform Target;
    bool isDestory = false;
    private void Start() {
        EnemyMask = LayerMask.GetMask("Enemy");
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
            other.gameObject.GetComponent<Enemy_Script>().TakeDamage(Bullet_Damage);
            Collider2D[] inRange = Physics2D.OverlapCircleAll(transform.position,slowRange,EnemyMask);
            for(int i=0;i<(int)inRange.Length;i++){
                if(inRange[i] == null) continue;
                Enemy_Script em=inRange[i].gameObject.GetComponent<Enemy_Script>();
                em.UpdateSpeed(slowRate,slowTime);
            }
            // RaycastHit2D[] inRange = Physics2D.CircleCastAll(transform.position,slowRange,(Vector2)transform.position,0f,EnemyMask);
            // for(int i=0;i<inRange.Length;i++){
            //     Enemy_Script em=inRange[i].transform.GetComponent<Enemy_Script>();
            //     Debug.Log("!!!");
            //     em.UpdateSpeed(slowRate);
            //     StartCoroutine(ReseEnemySpeed(em));
            // }
            Destroy(gameObject);
        }
        
    }
    
}

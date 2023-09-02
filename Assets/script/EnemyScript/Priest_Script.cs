using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest_Script : MonoBehaviour{
    [SerializeField] float treatRange;
    [SerializeField] float treatRate;
    [SerializeField] float treatReload;
    float timer=0;

    LayerMask EnemyMask;
    private void Start() {
        EnemyMask = LayerMask.GetMask("Enemy","Ghost");
    }
    private void Update() {
        timer -= Time.deltaTime;
        if(timer < 0){
            timer = treatReload;
            TreatInRange();
        }
    }

    private void TreatInRange(){
        Collider2D[] inRange = Physics2D.OverlapCircleAll(transform.position,treatRange,EnemyMask);
        for(int i=0;i<(int)inRange.Length;i++){
            if(inRange[i] == null) continue;
            Enemy_Script em=inRange[i].gameObject.GetComponent<Enemy_Script>();
            float temp = em.Health*treatRate;
            em.Treat(em.Health*treatRate);
        }
    }
}

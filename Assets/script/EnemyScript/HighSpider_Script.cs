using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HighSpider_Script : MonoBehaviour{
    [SerializeField] float slowRange;
    [SerializeField] int slowCount;
    [SerializeField] float slowRate;
    LayerMask layerMask;
    float reload = 0.5f;
    float timer=0;
    private void Start() {
        layerMask = LayerMask.GetMask("Turret");
    }
    private void Update(){
        timer -= Time.deltaTime;
        if(timer < 0){
            InRangeSlow();
            timer = reload;
        }
    }

    void InRangeSlow(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, slowRange, layerMask);
        foreach (Collider2D collider in colliders) {
            if(collider.GetComponent<Turret>() != null){
                collider.GetComponent<Turret>().SlowTurret(slowRate,slowCount);
                continue;
            }
            if(collider.GetComponent<AOE_Turret>() != null){
                collider.GetComponent<AOE_Turret>().SlowTurret(slowRate,slowCount);
                continue;
            }
            if(collider.GetComponent<Slime_Turret>() != null){
                collider.GetComponent<Slime_Turret>().SlowTurret(slowRate,slowCount);
                continue;
            }
            if(collider.GetComponent<Tesla_Turret>() != null){
                collider.GetComponent<Tesla_Turret>().SlowTurret(slowRate,slowCount);
                continue;
            }
            if(collider.GetComponent<Witch_Turret>() != null){
                collider.GetComponent<Witch_Turret>().SlowTurret(slowRate,slowCount);
                continue;
            }
        }
    }
    // private void OnDrawGizmosSelected() {
    //     Handles.color=Color.blue;
    //     Handles.DrawWireDisc(transform.position,transform.forward,slowRange);
    // }
}

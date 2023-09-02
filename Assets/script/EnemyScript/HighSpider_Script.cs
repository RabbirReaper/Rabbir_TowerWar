using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HighSpider_Script : MonoBehaviour{
    [SerializeField] float slowRange;
    [SerializeField] int slowCount;
    [SerializeField] float slowRate;
    LayerMask layerMask;
    Component[] components = new Component[5];
    float reload = 0.5f;
    float timer=0;
    private void Start() {
        components[0] = GetComponent<Turret>();
        components[1] = GetComponent<AOE_Turret>();
        components[2] = GetComponent<Slime_Turret>();
        components[3] = GetComponent<Tesla_Turret>();
        components[4] = GetComponent<Witch_Turret>();

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
    private void OnDrawGizmosSelected() {
        Handles.color=Color.blue;
        Handles.DrawWireDisc(transform.position,transform.forward,slowRange);
    }
}

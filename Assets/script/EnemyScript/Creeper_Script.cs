using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creeper_Script : MonoBehaviour{
    [SerializeField] float explosionRange;
    [SerializeField] int brokenCount;

    LayerMask layerMask;
    private void Start() {
        layerMask = LayerMask.GetMask("Turret");
    }

    private void OnDestroy() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRange, layerMask);
        foreach (Collider2D collider in colliders) {
            if(collider.GetComponent<Turret>() != null){
                collider.GetComponent<Turret>().UpdateIsborken(brokenCount);
                continue;
            }
            // if(collider.GetComponent<AOE_Turret>() != null){
            //     collider.GetComponent<AOE_Turret>().SlowTurret(slowRate,slowCount);
            //     continue;
            // }
            // if(collider.GetComponent<Slime_Turret>() != null){
            //     collider.GetComponent<Slime_Turret>().SlowTurret(slowRate,slowCount);
            //     continue;
            // }
            // if(collider.GetComponent<Tesla_Turret>() != null){
            //     collider.GetComponent<Tesla_Turret>().SlowTurret(slowRate,slowCount);
            //     continue;
            // }
            // if(collider.GetComponent<Witch_Turret>() != null){
            //     collider.GetComponent<Witch_Turret>().SlowTurret(slowRate,slowCount);
            //     continue;
            // }
        }
    }
}

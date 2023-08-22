using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Script : MonoBehaviour{
    [SerializeField] int rotation;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            other.GetComponent<Enemy_Script>().UpdateMoveRotation(rotation);
        }
    }
}

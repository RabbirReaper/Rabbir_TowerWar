using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary_Script : MonoBehaviour{
    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(other.gameObject);
    }
}

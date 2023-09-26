using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDdeactivate : MonoBehaviour{
    [SerializeField] bool destroyGameObject;
    [SerializeField] float lifetime;
    WaitForSeconds waitLifetime;
    private void Awake() {
        waitLifetime = new WaitForSeconds(lifetime);
    }
    private void OnEnable() {
        StartCoroutine(Deactivate());
    }
    IEnumerator Deactivate(){
        yield return waitLifetime;
        if(destroyGameObject) Destroy(gameObject);
        else gameObject.SetActive(false);
    }
}

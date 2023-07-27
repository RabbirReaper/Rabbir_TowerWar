using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 
public class UpGradeUIHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public bool mouse_over = false;
    private void Update() {
        if(mouse_over) Debug.Log("!+!");
    }   

    public void OnPointerEnter(PointerEventData eventData){
        mouse_over=true;
    }
    public void OnPointerExit(PointerEventData eventData){
        mouse_over=false;
    }
}

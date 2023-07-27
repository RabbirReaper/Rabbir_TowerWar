using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 
public class UpGradeUIHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public bool mouse_over = false;
    
    
    public void OnPointerEnter(PointerEventData eventData){
        mouse_over=true;
        UIManager.main.SetHoveringStatie(true);
    }
    
    public void OnPointerExit(PointerEventData eventData){
        mouse_over=false;
        UIManager.main.SetHoveringStatie(false);
        gameObject.SetActive(false);
    }

}

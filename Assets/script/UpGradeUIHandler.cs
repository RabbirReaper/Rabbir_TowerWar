using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 
public class UpGradeUIHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private bool mouse_over = false;
    [SerializeField] GameObject attackRangeImage;
    
    public void OnPointerEnter(PointerEventData eventData){
        mouse_over=true;
        UIManager.main.SetHoveringStatie(true);
        attackRangeImage.SetActive(true);
    }
    
    public void OnPointerExit(PointerEventData eventData){
        mouse_over=false;
        UIManager.main.SetHoveringStatie(false);
        attackRangeImage.SetActive(false);
        gameObject.SetActive(false);
    }
    

}

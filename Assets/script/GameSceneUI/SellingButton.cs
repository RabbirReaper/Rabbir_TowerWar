using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SellingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    [SerializeField] string _name;
    [SerializeField] int  sellValue;
    string str;
    private void Start() {
        str = "\n " + _name + "\n\n\n\n Selling: " + sellValue + "\n";
    }
    public void OnPointerEnter(PointerEventData eventData){
        TooltipScreen.main.SetActive(true);
        TooltipScreen.main.SetText(str);
    }

    public void OnPointerExit(PointerEventData eventData){
        TooltipScreen.main.SetActive(false);
    }
}

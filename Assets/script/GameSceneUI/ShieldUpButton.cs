using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShieldUpbutton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    [SerializeField] string _name;
    [SerializeField] float range;
    [SerializeField] int cost;
    private string str;

    private void Start() {
        str = "\n " + _name +"\n\n\n\n Range:  " + range + "\n\n\n\n Cost: " + cost;
    }
    public void OnPointerEnter(PointerEventData eventData){
        TooltipScreen.main.SetActive(true);
        TooltipScreen.main.SetText(str);
    }

    public void OnPointerExit(PointerEventData eventData){
        TooltipScreen.main.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NormalTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    [SerializeField] string str;

    public void OnPointerEnter(PointerEventData eventData){
        TooltipScreen.main.SetActive(true);
        TooltipScreen.main.SetText(str);
    }

    public void OnPointerExit(PointerEventData eventData){
        TooltipScreen.main.SetActive(false);
    }

}

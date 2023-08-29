using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TeslaUpButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    [SerializeField] string _name;
    [SerializeField] GameObject tower;
    [SerializeField] int cost;
    private string str;
    private void Start() {
        Tesla_Turret towerScript = tower.GetComponent<Tesla_Turret>();
        str ="\n " + _name +"\n\n\n\n Damage:  " + towerScript.Bullet_Damage +"\n\n Reload:  " + towerScript.reload + "\n\n AttachRange:  " + towerScript.AttackRange +"\n\n\n\n Cost: " + cost;
    }
    public void OnPointerEnter(PointerEventData eventData){
        TooltipScreen.main.SetActive(true);
        TooltipScreen.main.SetText(str);
    }

    public void OnPointerExit(PointerEventData eventData){
        TooltipScreen.main.SetActive(false);
    }
}

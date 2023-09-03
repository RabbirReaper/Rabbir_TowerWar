using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MagmaUpButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    [SerializeField] string _name;
    [SerializeField] GameObject tower;
    [SerializeField] int cost;
    [SerializeField] string special;
    private string str;

    private void Start() {
        AOE_Turret towerScript = tower.GetComponent<AOE_Turret>();
        str = "\n " + _name +"\n\n\n\n Damage:  " + towerScript.bulletPrefab.GetComponent<AOE_Bullet>().Bullet_Damage +"\n\n Reload:  " + towerScript.reload + "\n\n AttachRange:  " + towerScript.AttackRange + "\n\n Splash: " + towerScript.bulletPrefab.GetComponent<AOE_Bullet>().splashRange ;
        if(special != "null") str+="\n\n\n\n special: " + special;
        str +="\n\n\n\n Cost: " + cost;
    }
    public void OnPointerEnter(PointerEventData eventData){
        TooltipScreen.main.SetActive(true);
        TooltipScreen.main.SetText(str);
    }

    public void OnPointerExit(PointerEventData eventData){
        TooltipScreen.main.SetActive(false);
    }
}

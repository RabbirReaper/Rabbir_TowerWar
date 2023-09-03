using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlimeUpButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    [SerializeField] string _name;
    [SerializeField] GameObject tower;
    [SerializeField] int cost;
    [SerializeField] string special;
    private string str;
    private void Start() {
        Slime_Turret towerScript = tower.GetComponent<Slime_Turret>();
        str ="\n " + _name +"\n\n\n\n Damage:  " + towerScript.bulletPrefab.GetComponent<Slime_Bullet>().Bullet_Damage +"\n\n Reload:  " + towerScript.reload + "\n\n AttachRange:  " + towerScript.AttackRange + "\n\n Splash: " +towerScript.bulletPrefab.GetComponent<Slime_Bullet>().slowRange ;
        if(special != "null") str+="\n\n\n\n special: " + special;
        str += "\n\n\n\n Cost: " + cost;
    }
    public void OnPointerEnter(PointerEventData eventData){
        TooltipScreen.main.SetActive(true);
        TooltipScreen.main.SetText(str);
    }

    public void OnPointerExit(PointerEventData eventData){
        TooltipScreen.main.SetActive(false);
    }
}

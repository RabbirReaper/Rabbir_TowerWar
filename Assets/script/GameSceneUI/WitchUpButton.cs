using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WitchUpButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    [SerializeField] string _name;
    [SerializeField] GameObject tower;
    [SerializeField] int cost;
    private string str;

    private void Start() {
        Witch_Turret towerScript = tower.GetComponent<Witch_Turret>();
        str = "\n " + _name +"\n\n\n\n Damage:  " + towerScript.bulletPrefab.GetComponent<Witch_Bullet>().Bullet_Damage +"\n\n Reload:  " + towerScript.reload + "\n\n AttachRange:  " + towerScript.AttackRange + "\n\n Weakness: " + towerScript.bulletPrefab.GetComponent<Witch_Bullet>().weakRate*100 + "%\n\n Weakness Time: "+ towerScript.bulletPrefab.GetComponent<Witch_Bullet>().weakTime + "\n\n\n\n Cost: " + cost;
    }
    public void OnPointerEnter(PointerEventData eventData){
        TooltipScreen.main.SetActive(true);
        TooltipScreen.main.SetText(str);
    }

    public void OnPointerExit(PointerEventData eventData){
        TooltipScreen.main.SetActive(false);
    }
}

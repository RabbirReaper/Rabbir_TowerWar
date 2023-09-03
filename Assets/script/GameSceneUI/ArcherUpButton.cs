using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArcherUpButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    [SerializeField] string _name;
    [SerializeField] GameObject tower;
    [SerializeField] int cost;
    [SerializeField] string special;
    private string str;

    private void Start() {
        Turret towerScript = tower.GetComponent<Turret>();
        str = "\n " + _name +"\n\n\n\n Damage:  " + towerScript.bulletPrefab.GetComponent<Bullet>().Bullet_Damage.ToString() +"\n\n Reload:  " + towerScript.reload + "\n\n AttachRange:  " + towerScript.AttackRange + "\n\n\n\n Special:  "+ special +"\n\n\n\n Cost: " + cost;
    }
    public void OnPointerEnter(PointerEventData eventData){
        TooltipScreen.main.SetActive(true);
        TooltipScreen.main.SetText(str);
    }

    public void OnPointerExit(PointerEventData eventData){
        TooltipScreen.main.SetActive(false);
    }
}

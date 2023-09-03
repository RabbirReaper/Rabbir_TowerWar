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
        if(towerScript == null){
            Sniper sniper = tower.GetComponent<Sniper>();
            str = "\n " + _name +"\n\n\n\n Damage:  " + sniper.bulletPrefab.GetComponent<Bullet>().Bullet_Damage.ToString() +"\n\n Reload:  " + sniper.reload + "\n\n AttachRange:  " + sniper.AttackRange ;
            if(special != "null") str +="\n\n\n\n Special:  "+ special ;
            str +="\n\n\n\n Cost: " + cost;
        }else{
            str = "\n " + _name +"\n\n\n\n Damage:  " + towerScript.bulletPrefab.GetComponent<Bullet>().Bullet_Damage.ToString() +"\n\n Reload:  " + towerScript.reload + "\n\n AttachRange:  " + towerScript.AttackRange ;
            if(special != "null") str +="\n\n\n\n Special:  "+ special ;
            str +="\n\n\n\n Cost: " + cost;
        }
    }
    public void OnPointerEnter(PointerEventData eventData){
        TooltipScreen.main.SetActive(true);
        TooltipScreen.main.SetText(str);
    }

    public void OnPointerExit(PointerEventData eventData){
        TooltipScreen.main.SetActive(false);
    }
}

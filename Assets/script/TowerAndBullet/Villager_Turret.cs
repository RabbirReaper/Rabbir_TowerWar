using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager_Turret : MonoBehaviour{
    public float protectRange;
    public int level;
    public int sellValue;

    public void SellingTower(){
        LevelManager_script.main.IncreaseGold(sellValue);
        UIManager.main.SetHoveringStatie(false);
        LevelManager_script.main.towerCountLimit++;
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGradeUpdateTower : MonoBehaviour{
    [SerializeField] GameObject nextLevelTower;
    [SerializeField] GameObject nextLevelTower2;
    [SerializeField] GameObject upgradeUI;
    [SerializeField] int level;
    [SerializeField] int baseUpGradeCost;
    public void OpenUpgradeUI(){
        upgradeUI.SetActive(true);
    }
    public void CloseUpgradeUI(){
        upgradeUI.SetActive(false);
    }
    public void Upgrade(){
        if(level >= 4) return;
        if(baseUpGradeCost > LevelManager_script.main.Gold) return;
        LevelManager_script.main.SpendCurrency(baseUpGradeCost);
        GetComponentInParent<Plot>().TowerUpdate(nextLevelTower);
        UIManager.main.SetHoveringStatie(false);
        Destroy(gameObject);
    }
    public void Upgrade2(){
        if(level >= 4) return;
        if(baseUpGradeCost > LevelManager_script.main.Gold) return;
        LevelManager_script.main.SpendCurrency(baseUpGradeCost);
        GetComponentInParent<Plot>().TowerUpdate(nextLevelTower2);
        UIManager.main.SetHoveringStatie(false);
        Destroy(gameObject);
    }
    
}

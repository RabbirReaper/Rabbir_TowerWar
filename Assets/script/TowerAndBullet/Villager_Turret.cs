using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager_Turret : MonoBehaviour{
    public float protectRange;
    public int level;
    public int sellValue;
    [SerializeField] GameObject attackRangeImage;
    private void Start() {
        float temp = level*5*2;
        attackRangeImage.transform.localScale = new Vector3(temp,temp,temp);
    }

    public void SellingTower(){
        LevelManager_script.main.IncreaseGold(sellValue);
        UIManager.main.SetHoveringStatie(false);
        LevelManager_script.main.TowerLimitAdd(1);
        LevelManager_script.main.UpdateOpponentBuildCount(4,-1);
        Destroy(this.gameObject);
    }
}

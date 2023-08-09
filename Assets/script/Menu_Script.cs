using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu_Script : MonoBehaviour{
    [SerializeField] TextMeshProUGUI GoldUI;
    [SerializeField] TextMeshProUGUI IncomeGI;
    [SerializeField] TextMeshProUGUI Next_IncomeUI;
    [SerializeField] GridLayoutGroup Tower_Shop;  //UnityEngine.UI
    [SerializeField] GridLayoutGroup Enemy_Shop;  //UnityEngine.UI
    [SerializeField] Button towerButton;
    [SerializeField] Button enemyButton;
    [SerializeField] Animator anim;
    [SerializeField] TextMeshProUGUI red_Hp;
    [SerializeField] TextMeshProUGUI blue_Hp;
    
    bool isMenuOpen = true;
    
    public void ToggleMenu(){
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen",isMenuOpen);
    }
    public void SwitchTowerAndEnemyShop(bool t){
        Tower_Shop.gameObject.SetActive(t);
        Enemy_Shop.gameObject.SetActive(!t);
        towerButton.gameObject.SetActive(!t);
        enemyButton.gameObject.SetActive(t);
    }
    private void OnGUI() {
        IncomeGI.text = LevelManager_script.main.Income.ToString();
        GoldUI.text = LevelManager_script.main.Gold.ToString();
        Next_IncomeUI.text =(10-(int)LevelManager_script.main.Next_Income).ToString(); // maybe can performance optimization
    }
    public void HpUpdate(){

    }
    
}

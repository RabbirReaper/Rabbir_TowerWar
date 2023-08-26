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
    [SerializeField] GameObject[] EnemyUI;
    [SerializeField] Animator anim;
    private int nowIncomeLevel=0;
    private int[] incomeLevel = new int[29];
    bool isMenuOpen = true;
    private void Start() {
        int j=0;
        for(int i=0;i<EnemyUI.Length;i+=2){
            incomeLevel[j++]=EnemyUI[i].GetComponent<EnemyButtonUI>().unlock;
        }
        incomeLevel[j] = 2147483647;
    }
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
        GoldUI.text = LevelManager_script.main.Gold.ToString();
        Next_IncomeUI.text =(10-(int)LevelManager_script.main.Next_Income).ToString(); // maybe can performance optimization
        IncomeGI.text = LevelManager_script.main.Income.ToString();
        if(LevelManager_script.main.Income >= incomeLevel[nowIncomeLevel]){  
            EnemyUI[nowIncomeLevel*2].SetActive(false);
            EnemyUI[nowIncomeLevel*2+1].SetActive(true);
            nowIncomeLevel++;
        }
    }
    
}

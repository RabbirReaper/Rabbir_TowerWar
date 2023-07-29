using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu_Script : MonoBehaviour{
    [SerializeField] TextMeshProUGUI GoldUI;
    [SerializeField] TextMeshProUGUI IncomeGI;
    [SerializeField] TextMeshProUGUI Next_IncomeUI;

    [SerializeField] Animator anim;
    
    bool isMenuOpen = true;
    
    

    public void ToggleMenu(){
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen",isMenuOpen);
    }
    private void OnGUI() {
        IncomeGI.text = LevelManager_script.main.Income.ToString();
        GoldUI.text = LevelManager_script.main.Gold.ToString();
        Next_IncomeUI.text =((int)LevelManager_script.main.Next_Income).ToString(); // maybe can performance optimization
    }
    
}

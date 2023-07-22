using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu_Script : MonoBehaviour{
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] Animator anim;
    bool isMenuOpen = true;

    public void ToggleMenu(){
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen",isMenuOpen);
    }
    private void OnGUI() {
        currencyUI.text = LevelManager_script.main.currency.ToString();
    }
    public void SetSelected(){
        
    }
}

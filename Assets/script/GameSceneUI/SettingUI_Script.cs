using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingUI_Script : MonoBehaviour{
    [SerializeField] GameObject settingUI;
    bool isSettingUIOpen = false;
    public void TurnSettingUI(){
        isSettingUIOpen = !isSettingUIOpen;
        settingUI.SetActive(isSettingUIOpen);
    }
}

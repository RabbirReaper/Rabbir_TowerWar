using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TooltipScreen : MonoBehaviour{
    private RectTransform backGround;
    private TextMeshProUGUI textMeshPro;
    private RectTransform rectTransform;
    private void Awake() {
        backGround = transform.Find("BackGround").GetComponent<RectTransform>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        rectTransform = transform.GetComponent<RectTransform>();
        SetText("Hello world"); 
    }

    private void Update() {
        rectTransform.position = Input.mousePosition;
    }

    private void SetText(string text){
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 paddingSize = new Vector2(8,8);
        backGround.sizeDelta = textSize + paddingSize;
    }
}

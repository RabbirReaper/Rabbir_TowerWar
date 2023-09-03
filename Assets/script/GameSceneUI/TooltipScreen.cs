using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class TooltipScreen : MonoBehaviour,IPointerExitHandler{
    [SerializeField] RectTransform canvasRectTransform;
    public static TooltipScreen main;
    private RectTransform backGround;
    private TextMeshProUGUI textMeshPro;
    private RectTransform rectTransform;
    private void Awake() {
        main = this;
        backGround = transform.Find("BackGround").GetComponent<RectTransform>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        rectTransform = transform.GetComponent<RectTransform>();
        // SetText("Hello world");
        gameObject.SetActive(false);
    }

    private void Update() {
        rectTransform.anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;
        rectTransform.transform.position = new Vector3(rectTransform.transform.position.x + 20f,rectTransform.transform.position.y,0);
        if(!UIManager.main.IsHoveringUI()) SetActive(false);
    }

    public void SetText(string text){
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 paddingSize = new Vector2(30,20);
        backGround.sizeDelta = textSize + paddingSize;
    }
    public void SetActive(bool temp){
        rectTransform.transform.position = new Vector3(2000,2000,0);
        gameObject.SetActive(temp);
    }

    public void OnPointerExit(PointerEventData eventData){
        SetActive(false);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class EnemyButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Button button;
    [SerializeField] Image imagePrefab; // 需要在 Inspector 中設定預製體
    private Image spawnedImage;
    private TMP_Text text;
    // private RectTransform uiRectTransform;

    private void Start(){
        // uiRectTransform = GetComponent<RectTransform>();
    }

    private void Update(){
        if (spawnedImage != null){
            spawnedImage.transform.position = new Vector3(Input.mousePosition.x + spawnedImage.rectTransform.rect.width*0.5f + 0.1f,Input.mousePosition.y - spawnedImage.rectTransform.rect.height*0.5f-0.1f,0);

        }
    }

    public void OnPointerEnter(PointerEventData eventData){
        // 創建並顯示 UI
        spawnedImage = Instantiate(imagePrefab,new Vector3(Input.mousePosition.x + imagePrefab.rectTransform.rect.width*0.5f + 0.1f,Input.mousePosition.y - imagePrefab.rectTransform.rect.height*0.5f-0.1f,0),Quaternion.identity);
        text = spawnedImage.GetComponentInChildren<TMP_Text>();
        spawnedImage.transform.SetParent(transform.parent); // 設定父物件
        // spawnedImage.rectTransform.sizeDelta = new Vector2(50, 50); // 設定大小
        text.text = "Hellow World";
    }

    public void OnPointerExit(PointerEventData eventData){
        // 刪除 UI
        if (spawnedImage != null){
            Destroy(spawnedImage.gameObject);
            spawnedImage = null;
        }
    }
}

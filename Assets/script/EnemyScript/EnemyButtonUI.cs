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
    [SerializeField] float enemyHp;
    [SerializeField] float enemySpeed;
    [SerializeField] int cost;
    [SerializeField] int income;
    public int unlock;

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
        spawnedImage = Instantiate(imagePrefab,new Vector3(0,0,0),Quaternion.identity);
        text = spawnedImage.GetComponentInChildren<TMP_Text>();
        spawnedImage.transform.SetParent(transform.parent.parent.parent); // 設定父物件
        // spawnedImage.rectTransform.sizeDelta = new Vector2(50, 50); // 設定大小
        text.text = button.name +"\n\nHealth:  " + enemyHp +"\nSpeed:  " + enemySpeed + "\n\nCost:  " + cost + "\nIncome:  +" + income + "\n\nReach " + unlock + " income to unlock";
    }

    public void OnPointerExit(PointerEventData eventData){
        // 刪除 UI
        if (spawnedImage != null){
            Destroy(spawnedImage.gameObject);
            spawnedImage = null;
        }
    }
}

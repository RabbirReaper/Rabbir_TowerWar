using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShieldTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    [SerializeField] Image imagePrefab; // 需要在 Inspector 中設定預製體
    [SerializeField] GameObject tower;
    [SerializeField] int cost;

    private Image spawnedImage;
    private TMP_Text text;

    private void Update(){
        if (spawnedImage != null){
            spawnedImage.transform.position = new Vector3(Input.mousePosition.x + spawnedImage.rectTransform.rect.width*0.5f + 0.1f,Input.mousePosition.y - spawnedImage.rectTransform.rect.height*0.5f-0.1f,0);
        }
    }

    public void OnPointerEnter(PointerEventData eventData){
        spawnedImage = Instantiate(imagePrefab);
        text = spawnedImage.GetComponentInChildren<TMP_Text>();
        spawnedImage.transform.SetParent(transform.parent.parent); // 設定父物件
        spawnedImage.rectTransform.sizeDelta = new Vector2(150, 150); // 設定大小
        Villager_Turret towerScript = tower.GetComponent<Villager_Turret>();
        text.text = tower.name +"\n\nRange:  " + towerScript.protectRange + "\n\nCost: " + cost;
    }

    public void OnPointerExit(PointerEventData eventData){
        // 刪除 UI
        if (spawnedImage != null){
            Destroy(spawnedImage.gameObject);
            spawnedImage = null;
        }
    }
}

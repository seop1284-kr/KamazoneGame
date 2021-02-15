using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemCell : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image bg;
    [SerializeField] private Image heroPicture;
    

    public static System.Action<ItemCell> OnClickItem;

    private ItemInfo itemInfo;
    public ItemInfo ItemInfo => itemInfo;  

    public void SetInfo(ItemInfo itemInfo) {
        if (itemInfo == null) {
            nameText.text = "";
            bg.color = Color.grey;
            heroPicture.color = Color.grey;
        } else {
            this.itemInfo = itemInfo;

            nameText.text = this.itemInfo.characterInfo.name;
            bg.color = Color.white;
            heroPicture.color = Color.white;
        }

    }

    public void OnClick() {
        OnClickItem?.Invoke(this);
    }
}
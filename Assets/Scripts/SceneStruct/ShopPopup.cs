using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPopup : PopupBase {
    [SerializeField] private BelongingsControl belongingsCotrol;
    [SerializeField] private ItemBoardControl itemBoardControl;

    public override void Enter(object param) {
        
        Init();

    }

    public void Init() {
        belongingsCotrol.ShowBelongings();
        itemBoardControl.InitItemBoard();
    }

    public void OnClickBuyBtn() {
        ItemBoardControl.Instance.BuyItem();
        Init();
    }

}
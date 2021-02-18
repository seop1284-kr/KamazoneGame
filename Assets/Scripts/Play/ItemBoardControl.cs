using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;


public class ItemBoardControl : MonoSingleton<ItemBoardControl>
{
    [SerializeField] private ItemCell[] itemCells;
    [SerializeField] private TextMeshProUGUI statText;
    //[SerializeField] private Image ProfilePicture;

    [SerializeField] private TextMeshProUGUI buyBtnText;

    private ItemCell SelectedItemCell;
    
    public void InitItemBoard() {
        // 아이템 셀 초기화
        SelectedItemCell = null;
        buyBtnText.text = "";
        statText.text = "";

        foreach (var itemCell in itemCells) {
            int price = 0;
            itemCell.SetInfo(null);
        }

        ItemCell.OnClickItem = OnClickItemProfile;

        int curStageIdx = GameData.Instance.playerInfo.stageIdx;
        int curLevelIdx = GameData.Instance.playerInfo.levelIdx;

        // 플레이어 저장 데이터에 현재 상점 정보가 없으면 새로 저장, 아이템 셀에 입력
        if (!GameData.Instance.playerInfo.shopInfo.Any()) {
            for (int i = 0; i < GameData.Instance.stages[curStageIdx].levels[curLevelIdx].monsters.Length; ++i) {
                CharacterInfo characterInfo = GameData.Instance.CharacterInfos[GameData.Instance.stages[curStageIdx].levels[curLevelIdx].monsters[i]];
                int price = GameData.Instance.stages[curStageIdx].levels[curLevelIdx].monstersPos[i];
                ItemInfo itemInfo = new ItemInfo(characterInfo, price, i);
                itemCells[i].SetInfo(itemInfo);
                
                GameData.Instance.playerInfo.shopInfo.Add(itemInfo);
            }
        } else {
            // 아이템 셀에 입력
            for (int i = 0; i < GameData.Instance.playerInfo.shopInfo.Count; ++i) {
                itemCells[i].SetInfo(GameData.Instance.playerInfo.shopInfo[i]);
            }

        }

        // 저장
        GameData.Instance.SavePlayerData();
    }

    public void OnClickItemProfile(ItemCell itemCell) {
        if (itemCell.sold) {
            return;
        }
        statText.text = itemCell.ItemInfo.characterInfo.name + "\nhp: " + itemCell.ItemInfo.characterInfo.hp.ToString() + "\nstr: " + itemCell.ItemInfo.characterInfo.str.ToString();
        buyBtnText.text = itemCell.ItemInfo.price.ToString() + "G";
        SelectedItemCell = itemCell;
    }
    
    public void BuyItem() {

        if (SelectedItemCell != null) {
            if (GameData.Instance.playerInfo.money >= SelectedItemCell.ItemInfo.price) {
                if (GameData.Instance.playerInfo.heros.Count < 10) {
                    GameData.Instance.playerInfo.money -= SelectedItemCell.ItemInfo.price;
                    Character character = new Character(SelectedItemCell.ItemInfo.characterInfo);
                    character.index = GameData.Instance.playerInfo.heros.Count;
                    GameData.Instance.playerInfo.heros.Add(character);
                    //GameData.Instance.playerInfo.shopInfo.RemoveAt(SelectedItemCell.ItemInfo.index);
                    GameData.Instance.playerInfo.shopInfo[SelectedItemCell.ItemInfo.index] = null;
                    GameData.Instance.SavePlayerData();
                } else {
                    Debug.Log("10명 다모았어여");
                }
            } else {
                Debug.Log("비싸여");    
            }
        } else {
            Debug.Log("선택된 아이템 없음");
        }
    }

}

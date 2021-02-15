using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProfileControl : MonoBehaviour
{
    [SerializeField] private ProfileCell[] heroProfileCells;

    [SerializeField] private TextMeshProUGUI heroExplain;
    [SerializeField] private TextMeshProUGUI addButtonText;

    private ProfileCell selectedHero;

    public void Init() {
        foreach (var heroProfileCell in heroProfileCells) {
            heroProfileCell.SetInfo(null);
        }

        ProfileCell.OnClickHero = OnClickProfile;
        
        for (int i = 0; i < GameData.Instance.playerInfo.heros.Count; ++i) {
            heroProfileCells[i].SetInfo(GameData.Instance.playerInfo.heros[i], i);
        }
        
        OnClickProfile(heroProfileCells[0]);
    }

    public void OnClickProfile(ProfileCell profileCell) {
        if (profileCell.HeroInfo == null) return;
        
        heroExplain.text = GameData.Instance.CharacterInfos[profileCell.HeroInfo.type].name;
        selectedHero = profileCell;
        addButtonText.text = profileCell.IsOn ? "Remove" : "Add";
    }

    public void OnClickAdd() {
        if (selectedHero == null) return;

        if (!selectedHero.IsOn) {
            BoardControl.Instance.AddHero(selectedHero);
        } else {
            BoardControl.Instance.RemoveHero(selectedHero);
        }
        
        addButtonText.text = selectedHero.IsOn ? "Remove" : "Add";
    }
}

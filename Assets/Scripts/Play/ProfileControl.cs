using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProfileControl : MonoBehaviour
{
    [SerializeField] private ProfileCell[] heroProfileCells;

    [SerializeField] private TextMeshProUGUI heroExplain;
    [SerializeField] private TextMeshProUGUI addButtonText;

    private HeroInfo selectedHero;

    public void Init() {
        foreach (var heroProfileCell in heroProfileCells) {
            heroProfileCell.SetInfo(null);
        }

        ProfileCell.OnClickHero = OnClickProfile;
        
        for (int i = 0; i < GameData.Instance.playerInfo.heros.Length; ++i) {
            heroProfileCells[i].SetInfo(GameData.Instance.playerInfo.heros[i]);
        }
        
        OnClickProfile(heroProfileCells[0].HeroInfo);
    }

    public void OnClickProfile(HeroInfo heroinfo) {
        heroExplain.text = heroinfo.name;
        selectedHero = heroinfo;
        addButtonText.text = heroinfo.isOn ? "Remove" : "Add";
    }

    public void OnClickAdd() {
        BoardControl.Instance.AddHero(selectedHero);
        
        // add 된 경우,
        if (selectedHero.isOn) {
            addButtonText.text = selectedHero.isOn ? "Remove" : "Add";
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardControl : MonoSingleton<BoardControl> {
    [SerializeField] private DeckCell[] heroDeckCells;
    [SerializeField] private DeckCell[] enemyDeckCells;

    private const int BOARD_SIZE = 5;
    private const int MAX_HERO = 6;

    private DeckCell selectedCell;
    private DeckCell currentCell;

    private void Start() {
        for (int i = 0; i < BOARD_SIZE * BOARD_SIZE; ++i) {
            heroDeckCells[i].SetCoord(i);
        }
        for (int i = 0; i < BOARD_SIZE * BOARD_SIZE; ++i) {
            enemyDeckCells[i].SetCoord(i);
        }
    }

    public void InitBoard(Level level) {
        foreach (var enemyDeckCell in enemyDeckCells) {
            enemyDeckCell.Init();
        }
        
        // TODO: current hp가 반영된, character 정보로 셋팅하도록 수정
        for (int i = 0; i < level.monstersPos.Length; ++i) {
            var characterInfo = GameData.Instance.CharacterInfos[level.monsters[i]];
            enemyDeckCells[level.monstersPos[i]].SetInfo(new Character(characterInfo));
        }
        
        foreach (var heroDeckCell in heroDeckCells) {
            heroDeckCell.Init();
        }

        currentCell = null;
        selectedCell = null;
    }

    public void AddHero(ProfileCell profileCell) {
        for (int i = 0; i < MAX_HERO; ++i) {
            if (heroDeckCells[i].CellCharacter == null) {
                profileCell.IsOn = true;
                heroDeckCells[i].SetInfo(profileCell.HeroInfo);
                break;
            }
        }
    }
    
    public void RemoveHero(ProfileCell profileCell) {
        var deckCell = Array.Find(heroDeckCells, cell => cell.CellCharacter?.index == profileCell.HeroInfo.index);
        deckCell.Init();
        profileCell.IsOn = false;
    }
    
    public void BeginDrag(Vector2Int coord) {
        if (this[coord].CellCharacter == null) return;
        
        selectedCell = this[coord];
        currentCell = this[coord];
        
        this[coord].SetHighlight(true);
    }

    public void FinishDrag(Vector2Int coord) {
        if (selectedCell == null) return;

        var newCoord = currentCell.Coord;
        this[newCoord].SetHighlight(false);
        if (this[newCoord].CellCharacter == null) {
            // move
            this[newCoord].SetInfo(selectedCell.CellCharacter);
            selectedCell.Init();
        } else if(newCoord != selectedCell.Coord && newCoord != selectedCell.Coord) {
            // change
            var changeCell = this[newCoord].CellCharacter;
            
            this[newCoord].SetInfo(selectedCell.CellCharacter);
            selectedCell.SetInfo(changeCell);
        }
        
        selectedCell = null;
        currentCell = null;
    }
    
    public void ExitDrag(Vector2Int coord) {
        // this[coord].SetSelect(true);
        if (currentCell != null) {
            this[currentCell.Coord].SetHighlight(false);
        }
    }

    public void EnterCell(Vector2Int coord) {
        currentCell = this[coord];
        if (selectedCell != null) {
            // highlight
            this[coord].SetHighlight(true);
        }
    }
    
    public DeckCell this[Vector2Int coord] => heroDeckCells[coord.x * BOARD_SIZE + coord.y];
}

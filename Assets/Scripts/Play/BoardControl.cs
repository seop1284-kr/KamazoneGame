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
        
        for (int i = 0; i < level.monstersPos.Length; ++i) {
            enemyDeckCells[level.monstersPos[i]].SetInfo(GameData.Instance.monsters[level.monsters[i]]);
        }
        
        foreach (var heroDeckCell in heroDeckCells) {
            heroDeckCell.Init();
        }

        currentCell = null;
        selectedCell = null;
    }

    public void AddHero(HeroInfo heroInfo) {
        if (heroInfo.isOn) {
            var deckCell = Array.Find(heroDeckCells, cell => cell.CellInfo?.index == heroInfo.index);
            deckCell.Init();
            heroInfo.isOn = false;
            return;
        }
        
        for (int i = 0; i < MAX_HERO; ++i) {
            if (heroDeckCells[i].CellInfo == null) {
                heroInfo.isOn = true;
                heroDeckCells[i].SetInfo(heroInfo);
                break;
            }
        }
    }
    
    public void BeginDrag(Vector2Int coord) {
        if (this[coord].CellInfo == null) return;
        
        selectedCell = this[coord];
        currentCell = this[coord];
        
        this[coord].SetHighlight(true);
    }

    public void FinishDrag(Vector2Int coord) {
        if (selectedCell == null) return;

        var newCoord = currentCell.Coord;
        this[newCoord].SetHighlight(false);
        if (this[newCoord].CellInfo == null) {
            // move
            this[newCoord].SetSell(selectedCell.CellInfo);
            selectedCell.Init();
        } else if(newCoord != selectedCell.Coord && newCoord != selectedCell.Coord) {
            // change
            var changeCell = this[newCoord].CellInfo;
            
            this[newCoord].SetSell(selectedCell.CellInfo);
            selectedCell.SetSell(changeCell);
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

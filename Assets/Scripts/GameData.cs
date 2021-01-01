using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : Singleton<GameData> {
    public class Profile {
        
    }

    private Stage[] stages;


}


public enum Type {
    BATTLE,
    QUESTION,
    SHOP,
    START,
    BOSS
}
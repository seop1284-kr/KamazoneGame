using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoryControl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI storyText;    

    public void ShowStory(Stage stage) {
        
        storyText.text = "<" + stage.name + ">\n" + stage.story;

    }
}
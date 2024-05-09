using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeveItemUI : MonoBehaviour
{
    [SerializeField] private Text textLevel;
    [SerializeField] private Button buttonLevel;

    public void OnInit(string textIndex, Action<string> actionButton)
    {
        textLevel.text = "Level " + textIndex;
        buttonLevel.onClick.AddListener(() =>
        {
            actionButton.Invoke(textIndex);
        });
    }
}

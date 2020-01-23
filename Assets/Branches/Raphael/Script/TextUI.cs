using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextUI : MonoBehaviour{

    private TextMeshProUGUI textMesh;
    private string text;
    public int number { get; private set; }
    private int initialeValue = 0;

    public void Initialize(TextMeshProUGUI textMesh, string text, int initialeValue) {
        this.text = text;
        this.number = initialeValue;
        this.initialeValue = initialeValue;
        this.textMesh = textMesh;
        this.textMesh.text = this.text + this.number;
    }

    public void ChangeStat(int number) {
        this.textMesh.text = text + number;
    }

    public void Reset() {
        this.number = this.initialeValue;

    }

    public void SetActive(bool activate) {
        this.textMesh.gameObject.SetActive(activate);
    }

}

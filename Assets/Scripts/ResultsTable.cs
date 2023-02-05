using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ResultsTable : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    
    private void Start()
    {
        Refresh();
    }

    private void OnEnable()
    {
        Main.OnHealthChanged += Refresh;
    }

    private void OnDisable()
    {
        Main.OnHealthChanged -= Refresh;
    }

    void Refresh()
    {
        if (text == null) return;

        text.text = new string(Data.ResultData.ToArray());
    }
}

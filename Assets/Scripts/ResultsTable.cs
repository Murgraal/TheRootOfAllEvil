using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultsTable : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    public List<char> resultData;

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
        resultData = Data.ResultData;
        text.text = new string(Data.ResultData.ToArray());
    }
}

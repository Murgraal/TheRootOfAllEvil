using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultsTable : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
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
        text.text = Data.ResultData.ToString();
    }
}

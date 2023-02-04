using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image bar;

    private void Awake()
    {
    }

    private void OnEnable()
    {
        Main.OnHealthChanged += Refresh;
    }
    
    void Refresh()
    {
        if (bar == null) return;
        bar.fillAmount = Data.GamePlay.Health / Main.StartHealth * 100f;
    }
}

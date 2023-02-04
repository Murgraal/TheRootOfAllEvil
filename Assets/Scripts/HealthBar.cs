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

    private void OnDisable()
    {
        Main.OnHealthChanged -= Refresh;
    }

    void Refresh()
    {
        if (bar == null) return;
        Debug.Log(Data.GamePlay.Health);
        bar.fillAmount = (float)Data.GamePlay.Health / Main.StartHealth;
    }
}

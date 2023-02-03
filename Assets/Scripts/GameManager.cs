using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<KeyCode> OnKeyPressed;
    
    private void Start()
    {
        
    }

    private void Update()
    {
        foreach (var key in Data.PolledKeycodes)
        {
            if (Input.GetKeyDown(key))
            {
                OnKeyPressed?.Invoke(key);
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheMotherOfAllMonobehaviours : MonoBehaviour
{
    public PrefabContainer PrefabContainer;

    public void Start()
    {
        Main.PrefabContainer = PrefabContainer;
        Main.Init();
        
    }
}

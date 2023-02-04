using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PrefabContainer", fileName = "PrefabContainer")]
public class PrefabContainer : ScriptableObject
{
    public GameManager GameManagerPrefab;
    public Menu MenuPrefab;
    public GameObject UIPrefab;
    public GameObject GameplayGraphicsPrefab;
}

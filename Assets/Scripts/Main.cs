using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public static class Main
{
    public const string GameManagerPath = "Assets/Prefabs/GameManager.prefab";
    public static GameManager GameManager;
    
    [RuntimeInitializeOnLoadMethod]
    public static void Init()
    {
        GameManager = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameManager>(GameManagerPath));
        
        MovingCharacter.OnEnterHitZone += OnEnterHitZone;
        MovingCharacter.OnExitHitZone += OnExitHitZone;
    }

    public static void OnEnterHitZone(MovingCharacterData data)
    {
        Data.GamePlay.MovingCharactersInHitZone.Add(data);
    }

    public static void OnExitHitZone(MovingCharacterData data)
    {
        Data.GamePlay.MovingCharactersInHitZone.Remove(data);
    }
}
using System.Collections;
using System.Collections.Generic;
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
        MovingCharacter.OnEnterHitZone += OnEnterHitZone;
        MovingCharacter.OnExitHitZone += OnExitHitZone;

        Data.GeneratedCharacterData = GenerateCharacterData();
        Data.CharacterQueue = GenerateWordQueue();
        Data.ResultData = new List<char>(Data.GeneratedCharacterData.Count);
        
        GameManager = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameManager>(GameManagerPath));
    }

    public static void OnKeyPressed(KeyCode keyCode)
    {
        foreach (var keyinhitzone in Data.GamePlay.MovingCharactersInHitZone)
        {
            if (keyinhitzone.ValidKey == keyCode)
            {
                Data.ResultData[keyinhitzone.positionInString] = keyinhitzone.Letter;
            }
        }
    }

    public static MovingCharacterData GetLetterFromQueue()
    {
        if (Data.CharacterQueue.Count > 0)
            return Data.CharacterQueue.Dequeue();
        
        return null;
    }

    public static void OnEnterHitZone(MovingCharacterData data)
    {
        Data.GamePlay.MovingCharactersInHitZone.Add(data);
    }

    public static void OnExitHitZone(MovingCharacterData data)
    {
        Data.GamePlay.MovingCharactersInHitZone.Remove(data);
    }
    
    public static List<MovingCharacterData> GenerateCharacterData()
    {
        var result = new List<MovingCharacterData>();
        var array = Data.SourceString.ToCharArray();
        for (int i = 0; i < array.Length; i++)
        {
            var c = array[i];
            result.Add(new MovingCharacterData(c,i));
        }
        return result;
    }

    public static Queue<MovingCharacterData> GenerateWordQueue()
    {
        var result = new List<MovingCharacterData>();
        var charData = Data.GeneratedCharacterData;
        
        while (charData.Count > 0)
        {
            var randomIndex = UnityEngine.Random.Range(0, charData.Count);
            result.Add(charData[randomIndex]);
            charData.RemoveAt(randomIndex);
        }

        return new Queue<MovingCharacterData>(result);
    }
}
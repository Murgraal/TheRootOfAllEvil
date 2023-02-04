using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public static class Main
{
    public const string GameManagerPath = "Assets/Prefabs/GameManager.prefab";
    public const string SourceTextPath = "/Texts/";

    public const int DamagePerMissedLetter = 5;
    public const int StartHealth = 100;

    public static float[] HealthRewardDistanceTresholds = new[]
    {
        0.1f,
        0.2f,
        0.3f
    };
    
    public static int[] HealthRewardsPerTreshold = new[]
    {
        1,
        2,
        3
    };
    
    public static GameManager GameManager;
    
    [RuntimeInitializeOnLoadMethod]
    public static void Init()
    {
        MovingCharacter.OnEnterHitZone += OnEnterHitZone;
        MovingCharacter.OnExitHitZone += OnExitHitZone;

        var path = Application.dataPath + SourceTextPath;
        Debug.Log(path);
        var files = Directory.GetFiles(path);
        
        foreach (var file in files)
        {
            if (file.Contains(".meta")) continue;
            var reader = new StreamReader(file);
            Data.SourceString.Add(reader.ReadToEnd());
        }
        
        foreach (var keys in Data.KeyBoardLayout)
        {
            Data.PolledKeycodes.UnionWith(keys.Keys);
        }

        Data.GeneratedCharacterData = GenerateCharacterData(Data.GamePlay.Level);
        if (Data.GeneratedCharacterData == null) return;
        Data.CharacterQueue = GenerateWordQueue();
        Data.ResultData = new List<char>(Data.GeneratedCharacterData.Count);
        GameManager.OnKeyPressed += OnKeyPressed;
        
        GameManager = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameManager>(GameManagerPath));
    }

    public static void OnKeyPressed(KeyCode keyCode)
    {
        if (Data.GamePlay.MovingCharactersInHitZone.Count == 0)
        {
            Data.GamePlay.Health -= DamagePerMissedLetter;

            if (Data.GamePlay.Health <= 0)
            {
                Debug.Log("You play shit. You loose : - DDDDDDDD");
            }
        }
        foreach (var keyinhitzone in Data.GamePlay.MovingCharactersInHitZone)
        {
            if (keyinhitzone.ValidKey == keyCode)
            {
                Data.ResultData[keyinhitzone.positionInString] = keyinhitzone.Letter;
                Data.GamePlay.Health += GetHealthBasedOnYPos(keyinhitzone.yPos);
            }
        }
    }

    public static MovingCharacterData GetLetterFromQueue()
    {
        if (Data.CharacterQueue.Count > 0)
            return Data.CharacterQueue.Dequeue();
        
        return null; // End Of Level
    }

    public static void OnEnterHitZone(MovingCharacterData data)
    {
        Data.GamePlay.MovingCharactersInHitZone.Add(data);
    }

    public static void OnExitHitZone(MovingCharacterData data)
    {
        Data.GamePlay.MovingCharactersInHitZone.Remove(data);
    }
    
    public static List<MovingCharacterData> GenerateCharacterData(int level)
    {
        if (level >= Data.SourceString.Count) return null;
        var result = new List<MovingCharacterData>();
        var array = Data.SourceString[level].ToCharArray();
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
        var charData = GenerateCharacterData(Data.GamePlay.Level);
        var counter = charData.Count;
        for (int i = charData.Count - 1; i > 0; i--)
        {
            var randomIndex = UnityEngine.Random.Range(0, counter);
            var randomChar = charData[randomIndex];
            result.Add(randomChar);
            charData[i] = randomChar;
            counter--;
        }

        result = result.Where(x => Data.PolledKeycodes.Contains(x.ValidKey)).ToList();
        result.ForEach(x => Debug.Log(x.Letter));
        return new Queue<MovingCharacterData>(result);
    }

    public static int GetHealthBasedOnYPos(float movingCharacterYPos)
    {
        var distance = Mathf.Abs(movingCharacterYPos - GameManager.LineYPos);

        for (int i = 0; i < HealthRewardDistanceTresholds.Length; i++)
        {
            if (distance < HealthRewardDistanceTresholds[i])
            {
                return HealthRewardsPerTreshold[i];
            }
        }

        return 0;
    }

    public static int GetSliderIndexFromKey(KeyCode key)
    {
        for (int i = 0; i < Data.KeyBoardLayout.Count; i++)
        {
            if (Data.KeyBoardLayout[i].ContainsKey(key))
            {
                // TODO check if slider exists
                return i;
            }
        }
        Debug.Log($"Key {key} is not a letter");
        return -1;
    }
}
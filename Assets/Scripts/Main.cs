using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public static class Main
{
    public const string PrefabPath = "Assets/Prefabs/";
    public const string GameManagerPath = PrefabPath + "GameManager.prefab";
    public const string SourceTextPath = "/Texts/";
    public const string UIPrefabPath = PrefabPath + "UI.prefab";
    public const string GameplayGraphicsPath = PrefabPath + "GameplayGraphics.prefab";
    public const string MenuPrefabPath = PrefabPath + "MenuCanvas.prefab";

    public const int DamagePerMissedLetter = 5;
    public const int StartHealth = 100;
    public static event Action OnHealthChanged; 

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
    public static GameObject GameplayUI;
    public static GameObject GameplayGraphics;
    public static Menu Menu;
    
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
        
        for (int i = 0; i < Data.GeneratedCharacterData.Count; i++)
        {
            if (Data.PolledKeycodes.Contains(Data.GeneratedCharacterData[i].ValidKey))
            {
                Data.ResultData.Add(' ');
            }
            else
            {
                Data.ResultData.Add(Data.GeneratedCharacterData[i].Letter);
            }
            
            
        }
        
        GameManager.OnKeyPressed += OnKeyPressed;

        Menu = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<Menu>(MenuPrefabPath));
    }

    public static void StartGame()
    {
        GameplayUI = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(UIPrefabPath));
        GameManager = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameManager>(GameManagerPath));
        GameplayGraphics = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(GameplayGraphicsPath));
        Menu.gameObject.SetActive(false);
        Data.GamePlay.Health = StartHealth;
    }

    public static void OnKeyPressed(KeyCode keyCode)
    {
        if (ShouldDamage(keyCode))
        {
            if (Data.GamePlay.Health <= 0)
            {
                Debug.Log("You play shit. You loose : - DDDDDDDD");
                return;
            }
            Data.GamePlay.Health -= DamagePerMissedLetter;
        }
        foreach (var keyinhitzone in Data.GamePlay.MovingCharactersInHitZone)
        {
            if (keyinhitzone.ValidKey == keyCode)
            {
                var hitCharacter = GameManager.SpawnedCharacters.FirstOrDefault(x => x.data == keyinhitzone);
                if (hitCharacter != null)
                {
                    GameManager.CharacterSpawner.DespawnCharacter(hitCharacter);
                    Data.ResultData[keyinhitzone.positionInString] = keyinhitzone.Letter;
                    Data.GamePlay.Health += GetHealthBasedOnYPos(keyinhitzone.yPos);
                }
            }
        }
        OnHealthChanged?.Invoke();
    }

    public static bool ShouldDamage(KeyCode key)
    {
        foreach (var data in Data.GamePlay.MovingCharactersInHitZone)
        {
            if (key == data.ValidKey)
                return false;
        }
        return true;
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
        //result.ForEach(x => Debug.Log(x.Letter));
        return new Queue<MovingCharacterData>(result);
    }

    public static int GetHealthBasedOnYPos(float movingCharacterYPos)
    {
        var distance = Mathf.Abs(movingCharacterYPos - GameManager.SliderSystem.HitZoneStart);

        for (int i = 0; i < HealthRewardDistanceTresholds.Length; i++)
        {
            if (distance < HealthRewardDistanceTresholds[i])
            {
                return HealthRewardsPerTreshold[i];
            }
        }

        return 0;
    }

    public static SliderIndex GetSliderIndexFromKey(KeyCode key)
    {
        for (int i = 0; i < Data.KeyBoardLayout.Count; i++)
        {
            if (Data.KeyBoardLayout[i].ContainsKey(key))
            {
                // TODO check if slider exists
                return (SliderIndex)i;
            }
        }
        Debug.Log($"Key {key} is not a letter");
        return SliderIndex.InvalidSlider;
    }
}
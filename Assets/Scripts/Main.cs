using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Main
{
    public static string SourceTextPath = "/Texts/";
    
    public const int DamagePerMissedLetter = 5;
    public const int StartHealth = 100;
    
    

    public static PrefabContainer PrefabContainer;
    public static event Action OnHealthChanged;
    public static event Action OnScoreChanged;
    public static event Action OnStreakChanged;
    public static event Action<Vector3> OnSucessfulHit;

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

        var path = Application.streamingAssetsPath + SourceTextPath;
        Debug.Log(path);
        var files = Directory.GetFiles(path);
        
        foreach (var file in files)
        {
            if (!file.Contains(".txt")) continue;
            var reader = new StreamReader(file);
            Data.SourceString.Add(reader.ReadToEnd());
        }
        
        foreach (var keys in Data.KeyBoardLayout)
        {
            Data.PolledKeycodes.UnionWith(keys.Keys);
        }
        
        GameManager.OnKeyPressed += OnKeyPressed;
        LoadScene("MainMenu");
    }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void StartGame()
    {
        Data.GeneratedCharacterData = GenerateCharacterData(Data.GamePlay.Level);
        
        if (Data.GeneratedCharacterData == null) return;
        
        Data.CharacterQueue = GenerateWordQueue();
        Data.ResultData = new List<char>();
        
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
        Data.GamePlay.Health = StartHealth;
        Data.GamePlay.GameEnd = false;
        LoadScene("Gameplay");
    }

    public static void OnKeyPressed(KeyCode keyCode)
    {
        if (ShouldDamage(keyCode))
        {
            if (Data.GamePlay.Health <= 0 && !Data.GamePlay.GameEnd)
            {
                Debug.Log("You play shit. You loose : - DDDDDDDD");
                LoadScene("GameOver");
                Data.GamePlay.GameEnd = true;
                return;
            }
            Data.GamePlay.Streak = 0;
            Data.GamePlay.Health -= DamagePerMissedLetter;
            EndStreak();
            var missedKeyLaneIndex = Main.GetSliderIndexFromKey(keyCode);
            var missedSlider = GameManager.SliderSystem.GetSliderByIndex(missedKeyLaneIndex);
            var missEffectPos = missedSlider.GetPos(GameManager.SliderSystem.HitZoneStart);
            GameManager.EffectSpawner.SpawnMissEffect(missEffectPos);
        }
        foreach (var keyinhitzone in Data.GamePlay.MovingCharactersInHitZone)
        {
            if (keyinhitzone.ValidKey == keyCode)
            {
                var hitCharacter = Data.GamePlay.SpawnedCharacters.FirstOrDefault(x => x.data == keyinhitzone);
                if (hitCharacter != null)
                {
                    CharacterSpawner.DespawnCharacter(hitCharacter);
                    Data.ResultData[keyinhitzone.positionInString] = keyinhitzone.Letter;
                    Data.GamePlay.Health += GetHealthBasedOnYPos(keyinhitzone.yPos);
                    Data.GamePlay.ScoreMultiplier = (float)Math.Min(1.0 + 0.5 * (Data.GamePlay.Streak / 10), 6.66);
                    Data.GamePlay.MultiplierLevel = GetMultiplierLevel();
                    Data.GamePlay.Score += (int)(10 * Data.GamePlay.ScoreMultiplier);
                    OnSucessfulHit?.Invoke(hitCharacter.transform.position);
                    IncrementStreak();
                    OnScoreChanged?.Invoke();
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

    public static void EndStreak()
    {
        Data.GamePlay.Streak = 0;
        OnStreakChanged?.Invoke();
    }
    public static void IncrementStreak()
    {
        Data.GamePlay.Streak++;
        OnStreakChanged?.Invoke();
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
        var charData = GenerateCharacterData(Data.GamePlay.Level).Where(x => Data.PolledKeycodes.Contains(x.ValidKey)).ToList();

        for (int i = charData.Count -1; i >= 0; i--)
        {
            var randomIndex = UnityEngine.Random.Range(0,i + 1);
            var randomChar = charData[randomIndex];
            result.Add(randomChar);
            charData[randomIndex] = charData[i];
            charData[i] = randomChar;
        }
        
        //result.ForEach(x => Debug.Log(x.Letter));
        return new Queue<MovingCharacterData>(result);
    }

    public static int GetHealthBasedOnYPos(float movingCharacterYPos)
    {
        var distance = Mathf.Abs(movingCharacterYPos - Data.GamePlay.HitZoneStart);

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
        //Debug.Log($"Key {key} is not a letter");
        return SliderIndex.InvalidSlider;
    }

    private static Data.MultiplierLevel GetMultiplierLevel()
    {
        var multiplierCount = Mathf.Clamp((int)((Data.GamePlay.ScoreMultiplier / 0.5f) - 2f), 0, (int)Data.MultiplierLevel.Devilish);
        return (Data.MultiplierLevel)multiplierCount;
    }
}
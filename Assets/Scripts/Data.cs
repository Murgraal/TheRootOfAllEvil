using System.Collections.Generic;

public static class Data
{
    public static int Health;
    public static string SourceString;
    public static List<MovingCharacterData> GeneratedCharacterData;

    public static List<MovingCharacterData> GenerateCharacterData()
    {
        var result = new List<MovingCharacterData>();

        return result;
    }
    
    public static Queue<MovingCharacterData> CharacterQueue;
    
    public static class GamePlay
    {
        public static List<MovingCharacterData> MovingCharactersInHitZone;
    }
    
    
}
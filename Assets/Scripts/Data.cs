using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    public static int Health;
    public static string SourceString;

    public static Queue<MovingCharacterData> CharacterQueue;
    
    public static class GamePlay
    {
        public static List<MovingCharacterData> MovingCharactersInHitZone;
    }

    public static List<Dictionary<KeyCode, char>> KeyBoardLayout => _keyboardLayoutTemplate;

    private static List<Dictionary<KeyCode, char>> _keyboardLayoutTemplate = new List<Dictionary<KeyCode, char>>()
    { 
        //Keyboard layout
        new Dictionary<KeyCode, char>
        {
            {KeyCode.Q, 'Q'},
            {KeyCode.A, 'A'},
            {KeyCode.Z, 'Z'},
            {KeyCode.W, 'W'},
            {KeyCode.S, 'S'},
            {KeyCode.X, 'X'},
            {KeyCode.E, 'E'},
            {KeyCode.D, 'D'},
            {KeyCode.C, 'C'}
        },

        new Dictionary<KeyCode, char>
        {
            {KeyCode.R, 'R'},
            {KeyCode.F, 'F'},
            {KeyCode.V, 'V'},
            {KeyCode.T, 'T'},
            {KeyCode.G, 'G'},
            {KeyCode.B, 'B'},
            {KeyCode.Y, 'Y'},
            {KeyCode.H, 'H'},
            {KeyCode.N,'N'}
        },

        new Dictionary<KeyCode, char>
        {
            { KeyCode.U, 'U'},
            { KeyCode.J, 'J'},
            { KeyCode.M, 'M'},
            { KeyCode.I, 'I'},
            { KeyCode.K, 'K'},
            { KeyCode.O, 'O'},
            { KeyCode.L, 'L'},
            { KeyCode.P, 'P'},
        }
    };

}
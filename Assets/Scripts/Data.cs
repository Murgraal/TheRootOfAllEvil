using System;
using System.Collections.Generic;
using UnityEngine;

public static class Data
{
   
    public static List<string> SourceString  = new List<string>();
    public static List<MovingCharacterData> GeneratedCharacterData = new List<MovingCharacterData>();
    public static List<char> ResultData = new List<char>();
    public static HashSet<KeyCode> PolledKeycodes = new HashSet<KeyCode>();
    
    public static Queue<MovingCharacterData> CharacterQueue;
    
    public static class GamePlay
    {
        public static int Level = 0;
        public static int Health = 100;
        public static List<MovingCharacterData> MovingCharactersInHitZone = new List<MovingCharacterData>();
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
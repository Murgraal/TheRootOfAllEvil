using System;
using UnityEngine;

public class MovingCharacterData
{
    public MovingCharacterData(char character, int index)
    {
        Letter = character;
        if (!Enum.TryParse<KeyCode>(Letter.ToString(), true, out ValidKey))
        {
            Debug.LogWarning($"Failed to parse keycode {Letter}");
        }
        positionInString = index;
        sliderIndex = Main.GetSliderIndexFromKey(ValidKey);
    }

    public KeyCode ValidKey;
    public char Letter;
    public int positionInString;
    public float yPos;

    public SliderIndex sliderIndex;
    public float startTime;
}
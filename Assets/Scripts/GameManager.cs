using UnityEngine;

public class GameManager : MonoBehaviour
{

    private CharacterSlider[] _characterSliders;
    public CharacterSlider GetCharacterSlider(int index) => _characterSliders[index];
    private void Update()
    {
    }
}
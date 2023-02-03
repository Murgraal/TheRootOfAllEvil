using System;
using UnityEngine;

public class MovingCharacter : MonoBehaviour
{
    public MovingCharacterData data;
    public static event Action<MovingCharacterData> OnEnterHitZone;
    public static event Action<MovingCharacterData> OnExitHitZone;
    public void Init(MovingCharacterData data)
    {
        this.data = data;
    }

    public void OnTriggerEnter(Collider2D col)
    {
        if (col.CompareTag("Finish"))
        {
            OnEnterHitZone?.Invoke(data);
        }
    }

    public void OnTriggerExit(Collider2D col)
    {
        if (col.CompareTag("Finish"))
        {
            OnExitHitZone?.Invoke(data);
        }
    }
}
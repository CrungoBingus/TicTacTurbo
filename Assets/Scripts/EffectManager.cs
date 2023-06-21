using UnityEngine;

using System.Collections;

public class EffectManager : MonoBehaviour
{
    public Effect_BASE[] gameEffects;


    public static EffectManager Instance;
    private void Awake()
    {
        Instance = this;

        gameEffects = GetComponentsInChildren<Effect_BASE>();
    }

    public int GetNewRandomEffect()
    {
        return Random.Range(0, gameEffects.Length);
    }
}
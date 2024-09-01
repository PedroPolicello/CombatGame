
using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;
    
    public int playerMaxHealth;

    private void Awake()
    {
        Instance = this;
    }
}

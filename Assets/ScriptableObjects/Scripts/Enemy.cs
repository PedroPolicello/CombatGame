using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/New Enemy")]
public class Enemy : ScriptableObject
{
    public int enemyHealth;
    [HideInInspector] public bool inRange;
    [HideInInspector] public bool hasBeenDefeated;
}

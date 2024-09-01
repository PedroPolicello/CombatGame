using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/New Enemy")]
public class Enemy : ScriptableObject
{
    public int enemyHealth;
    public bool inRange;
    public bool hasBeenDefeated;
}

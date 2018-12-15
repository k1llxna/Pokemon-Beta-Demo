using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseAttacks : MonoBehaviour
{
    public string attackName; // Name
    public float attackDamage; // Base Damage
    public int pp; // Cost
    public string moveType; // Type of move
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EncounteredPokemon {

    public string name;
    public int level;
    public float maxHP;
    public float currentHP { get; private set; }
    public Sprite image;
    public PokemonType type;
    public Rarity rarity;
    public float attackStat;
    public float defenceStat;
    public List<BaseAttacks> moveList = new List<BaseAttacks>();
    public Image healthBar;

    public enum PokemonType
    {
        NORMAL,
        GRASS,
        WATER,
        FIRE
    }

    public enum Rarity
    {
        VeryCommon,
        Common,
        SemiRare,
        Rare,
        VeryRare
    }

    public PokemonType enemyType;
    public Rarity enemyRarity;
}


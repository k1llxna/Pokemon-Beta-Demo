using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class BasePokemon : MonoBehaviour {
    public string pokemonName;
    public int level;
    public float maxHP;
    public float currentHP; //{ get; private set; }
    public Sprite image;
    public PokemonType type;
    public Rarity rarity;
    public float attackStat;
    public float defenceStat;
    public List<BaseAttacks> moveList = new List<BaseAttacks>();
    public Image healthBar;
    public Text hp;

    void Start() {
        currentHP = maxHP;
    }
    void Update() {}

    public void AddMember(BasePokemon newMember) {
        this.pokemonName = newMember.pokemonName;
        this.level = newMember.level;
        this.currentHP = newMember.currentHP;
        this.maxHP = newMember.maxHP;
        this.image = newMember.image;
        this.type = newMember.type;
        this.rarity = newMember.rarity;   
        this.attackStat = newMember.attackStat;
        this.defenceStat = newMember.defenceStat;    
    }

   public void TakeDmg(float dmg) {
        currentHP -= dmg;
        if (currentHP <= 0) {
            currentHP = 0;
        }
        healthBar.fillAmount = currentHP / maxHP;
        hp.text = currentHP.ToString();     
    }
}

public enum Rarity {
    VeryCommon,
    Common,
    SemiRare,
    Rare,
    VeryRare
}

public enum PokemonType {
    Fire,
    Water,
    Grass,
    Normal
}

[System.Serializable]
public class PokemonMoves {
    public string moveName;
    public int PP;
    public float power;
}

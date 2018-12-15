using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public GameObject playerCamera;
    public GameObject battleCamera;
    public GameObject player;

    [Space(10)]
    [Header("Wild Pokemon")]
    public List<BasePokemon> allPokemon = new List<BasePokemon>();

    [Space(10)]
    [Header("Other")]
    public Transform defencePodium;
    public Transform attackPodium;
    public GameObject emptyPoke;
    public GameObject startingPokemon;
    public Text encounter;
    public BasePokemon ePoke;
    public GameObject dialogue;
    public Text message;  

    void Start() {
        playerCamera.SetActive(true);
        battleCamera.SetActive(false);
    }
    
    void Update() {}

    // Trigger battle
    public void EnterBattle(Rarity rarity) {
        playerCamera.SetActive(false);
        battleCamera.SetActive(true);
        player.GetComponent<PlayerMovement>().isAllowedToMove = false;

        BasePokemon battlePokemon = GetRandomPokemonFromList(GetPokemonByRarity(rarity));
        Debug.Log(battlePokemon.name);
     
        GameObject dPoke = Instantiate(emptyPoke, defencePodium.transform.position, Quaternion.identity) as GameObject;
       
        dPoke.transform.parent = defencePodium;
        BasePokemon tempPoke = dPoke.AddComponent<BasePokemon>() as BasePokemon;
        tempPoke.AddMember(battlePokemon);

        dPoke.GetComponent<SpriteRenderer>().sprite = battlePokemon.image;
        encounter.text = battlePokemon.name;
        message.text = "A wild " + battlePokemon.name + " appeared!";
        ePoke = battlePokemon;
    }

    //public BasePokemon EcounterPokemon() {
    //    Debug.Log(battlePokemon.name);
    //    return battlePokemon;    
    //}

    public List<BasePokemon> GetPokemonByRarity(Rarity rarity) {
        List<BasePokemon> returnPokemon = new List<BasePokemon>();
        foreach (BasePokemon Pokemon in allPokemon) {
            if (Pokemon.rarity == rarity)
                returnPokemon.Add(Pokemon);
        }
       return returnPokemon;
    }

    public BasePokemon GetRandomPokemonFromList(List<BasePokemon> pokeList) {
        BasePokemon poke = new BasePokemon();
        int pokeIndex = Random.Range(0, pokeList.Count - 1);
        poke = pokeList[pokeIndex];
        return poke;
    }

}

//[System.Serializable]
//public class PokemonMoves {
//    string Name;
//    public MoveType category;
//    public Stat moveStat;
//    public PokemonType moveType;
//    public int PP;
//    public float power;
//    public float accuracy;
//}

//[System.Serializable]
//public class Stat {
//    public float minimum;
//    public float maximum;
//}

//public enum MoveType {
//    Physical,
//    Special,
//    Status
//}
using UnityEngine;
using System.Collections;

public class LongGrass : MonoBehaviour {
    private GameManager gm;

    void Start(){
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update(){}

    void OnTriggerEnter2D(Collider2D col) {
        // Generate a number everytime the user collides with (walks on) the long grass
        if (col.GetComponent<PlayerMovement>()){   
            float vc = 10 / 187.5f;
            float c = 8.5f / 187.5f;
            float sr = 6.75f / 187.5f;
            float r = 3.33f / 187.5f;
            float vr = 1.25f / 187.5f;
            float p = Random.Range(0.0f, 100.0f);

            // Very Rare
            if (p < vr * 100) {
                if (gm != null)
                    gm.EnterBattle(Rarity.VeryRare);
            }

            // Rare
            else if (p < r * 100) {
                if (gm != null)
                    gm.EnterBattle(Rarity.Rare);
            }

            // Semi Rare
            else if (p < sr * 100) {
                if (gm != null)
                    gm.EnterBattle(Rarity.SemiRare);
            }

            // Common
            else if (p < c * 100) {
                if (gm != null)
                    gm.EnterBattle(Rarity.Common);
            }

            // Very Common
            else if (p < vc * 100) {
                if (gm != null)
                    gm.EnterBattle(Rarity.VeryCommon);
            }
        }
    }
}
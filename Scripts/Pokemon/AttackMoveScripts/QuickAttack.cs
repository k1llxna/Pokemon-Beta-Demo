using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickAttack : BaseAttacks {
    public QuickAttack() {
        attackName = "Quick Attack";
        attackDamage = 5f;
        pp = 30;
        moveType = "Normal";
    }
}

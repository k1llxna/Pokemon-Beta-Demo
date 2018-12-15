using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : BaseAttacks {
    public WaterGun()
    {
        attackName = "Water Gun";
        attackDamage = 15f;
        pp = 15;
        moveType = "Water";
    }
}

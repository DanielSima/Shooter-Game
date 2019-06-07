using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : Weapon {

    //!!!!reset the component in inspector when changing those values!!!!
    public void Reset()
    {
        velocity = 10f;
        rateOfFire = 0.05f;
        maxMagazine = 100f;
        currentMagazine = maxMagazine;
        reloadTime = 6.0f;

    }
}

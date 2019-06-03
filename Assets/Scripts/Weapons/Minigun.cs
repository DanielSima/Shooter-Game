using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : Weapon {

    //!!!!reset the component in inspector when changing those values!!!!
    public void Reset()
    {
        velocity = 5f;
        rateOfFire = 0.05f;
        maxMagazine = 500f;
        currentMagazine = maxMagazine;
        reloadTime = 6.0f;

    }
}

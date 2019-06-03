using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon {

    //!!!!reset the component in inspector when changing those values!!!!
    public void Reset()
    {
        velocity = 15f;
        rateOfFire = 0.8f;
        maxMagazine = 6f;
        currentMagazine = maxMagazine;
        reloadTime = 0.5f;
    }
    
    public override void Fire(GameObject selectedWeaponObject, GameObject selectedBullet)
    {
        //protect the fire method when reload coroutine waiting
        if (currentMagazine == 0) return;

        List<GameObject> selectedBulletObjects = new List<GameObject>();
        
        //instantiate three bullets
        for (int i = 0; i< 3; i++)
        {
            int angle = 0;

            if (i == 0) angle = -10;
            if (i == 1) angle = 0;
            if (i == 2) angle = 10;

            GameObject selectedBulletObject = Instantiate(
                   selectedBullet,
                   //the bullet spawn
                   selectedWeaponObject.transform.GetChild(0).position,
                   selectedWeaponObject.transform.GetChild(0).rotation * Quaternion.Euler(0, angle, 0)
                   );
            selectedBulletObjects.Add(selectedBulletObject);
        }

        //ignore collider of weapon
        Physics.IgnoreCollision(selectedBulletObjects[0].GetComponent<Collider>(), selectedWeaponObject.transform.GetChild(1).GetComponent<Collider>());
        Physics.IgnoreCollision(selectedBulletObjects[1].GetComponent<Collider>(), selectedWeaponObject.transform.GetChild(1).GetComponent<Collider>());
        Physics.IgnoreCollision(selectedBulletObjects[2].GetComponent<Collider>(), selectedWeaponObject.transform.GetChild(1).GetComponent<Collider>());
        
        //ignore collision between those bullets (because they spawn at same position)
        Physics.IgnoreCollision(selectedBulletObjects[0].GetComponent<Collider>(), selectedBulletObjects[1].GetComponent<Collider>());
        Physics.IgnoreCollision(selectedBulletObjects[0].GetComponent<Collider>(), selectedBulletObjects[2].GetComponent<Collider>());
        Physics.IgnoreCollision(selectedBulletObjects[1].GetComponent<Collider>(), selectedBulletObjects[2].GetComponent<Collider>());

        foreach (GameObject selectedBulletObject in selectedBulletObjects)
        {
            // Add velocity to the bullet
            selectedBulletObject.GetComponent<Rigidbody>().velocity = selectedBulletObject.transform.forward * velocity;

            //destroy after 5s if no collision
            Destroy(selectedBulletObject, 5);

        }
        currentMagazine--;
    }


    public override void Reload()
    {
        StartCoroutine(ContinuousReload());
    }

    /// <summary>
    /// reloads bullets one by one.
    /// </summary>
    public IEnumerator ContinuousReload()
    {
        while(currentMagazine != maxMagazine && !Input.GetKey(KeyCode.Mouse0))
        {
            currentMagazine++;
            yield return new WaitForSeconds(reloadTime);
        }
    }
}

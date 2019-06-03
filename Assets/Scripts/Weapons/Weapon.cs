using System.Collections;
using UnityEngine;

/// <summary>
/// base class for all weapons.
/// </summary>
public class Weapon : MonoBehaviour
{
    //to be changed for every weapon
    public float rateOfFire = 0;
    public float velocity = 0;
    public float maxMagazine = 0;
    public float currentMagazine = 0;
    public float reloadTime = 0;

    /// <summary>
    /// spawns selected bullet and adds velocity to it.
    /// </summary>
    public virtual void Fire(GameObject selectedWeaponObject, GameObject selectedBullet)
    {
        //protect the fire method when reload coroutine waiting
        if (currentMagazine == 0) return;

        //instantiate the bullet
        GameObject selectedBulletObject = Instantiate(
            selectedBullet,
            //the bullet spawn
            selectedWeaponObject.transform.GetChild(0).position,
            selectedWeaponObject.transform.GetChild(0).rotation
            );

        //ignore collider of weapon
        Physics.IgnoreCollision(selectedBulletObject.GetComponent<Collider>(), selectedWeaponObject.transform.GetChild(1).GetComponent<Collider>());

        // Add velocity to the bullet
        selectedBulletObject.GetComponent<Rigidbody>().velocity = selectedBulletObject.transform.forward * velocity;

        //destroy after 5s if no collision
        Destroy(selectedBulletObject, 5);

        currentMagazine--;
    }

    /// <summary>
    /// Runs every frame. Reloads weapon after specified time when needed.
    /// </summary>
    public IEnumerator ReloadCoroutine()
    {
        while (true)
        {
            if (Input.GetKeyDown("r") || this.currentMagazine == 0)
            {
                yield return new WaitForSeconds(reloadTime);
                Reload();
            }
            yield return null;
        }
    }

    public virtual void Reload()
    {
        currentMagazine = maxMagazine;

    }
}
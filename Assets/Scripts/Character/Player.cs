﻿using UnityEngine;

/// <summary>
/// the character controlled by user.
/// </summary>
public class Player : Character
{

    private new void Start()
    {
        base.Start();

        //default weapons
        selectedBulletPrefab = FireBulletPrefab;
        selectedWeaponPrefab = ShotgunPrefab;

        SpawnWeapon();
        StartCoroutine(weapon.ReloadCoroutine());
    }

    private void Update()
    {
        //movement
        var y = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        body.transform.Rotate(0, y, 0);
        body.MovePosition(transform.position + transform.forward * z);

        //change health and magazine number
        UI.setValues(this);

        //pause or unpause game
        if (Input.GetKeyDown("escape"))
        {
            
            if (Time.timeScale == 1.0f)
            {
                UI.Pause();
            }
            else
            {
                UI.Continue();
            }
        }
        rateOfFireTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    /// <summary>
    /// change current weapon or bullet
    /// </summary>
    public void ChangeSelection(GameObject weapon=null, GameObject bullet=null)
    {
        //destroy previous
        Destroy(weaponGameObject);
        //TODO not working
        StopCoroutine(this.weapon.ReloadCoroutine());

        //update selected
        if (weapon != null)
        {
            selectedWeaponPrefab = weapon;
        }
        if (bullet != null)
        {
            selectedBulletPrefab = bullet;
        }

        //create 
        SpawnWeapon();
        StartCoroutine(this.weapon.ReloadCoroutine());
    }
}
using System;
using System.Diagnostics;
using UnityEngine;

/// <summary>
/// the base class for player and enemies.
/// </summary>
public class Character : MonoBehaviour
{
    [HideInInspector]
    public static float maxHealth = 100;
    public float currentHealth = maxHealth;
    public Rigidbody body;

    public Transform weaponSpawn;

    //the prefabs
    public static GameObject AssaultRiflePrefab;
    public static GameObject ShotgunPrefab;
    public static GameObject MinigunPrefab;
    public static GameObject ClassicBulletPrefab;
    public static GameObject FireBulletPrefab;
    public static GameObject ExplosiveBulletPrefab;

    protected GameObject weaponGameObject; //the instantiated weapon
    protected GameObject selectedWeaponPrefab; //the weapon which user chose (or have been asigned to enemy)
    public Weapon weapon; //Weapon component of instantiated weapon
    protected GameObject selectedBulletPrefab; //the bullet which user chose (or have been asigned to enemy)

    public float moveSpeed = 10.0f;
    public float rotationSpeed = 250.0f;

    protected float rateOfFireTimer = 0f;

    protected void Start()
    {
        body = GetComponent<Rigidbody>();
        weaponSpawn = transform.GetChild(0).transform;
    }

    /// <summary>
    /// spawns the weapon in character's hands.
    /// </summary>
    public void SpawnWeapon()
    {
        weaponGameObject = Instantiate(
            selectedWeaponPrefab,
            weaponSpawn.position,
            weaponSpawn.rotation,
            weaponSpawn.parent
            );

        weapon = weaponGameObject.GetComponent<Weapon>();
    }

    /// <summary>
    /// changes health of character. Accepts positive or negative numbers.
    /// </summary>
    public void ChangeHealth(float change)
    {
        //dont go bigger than the max value.
        if (currentHealth + change >= maxHealth)
        {
            currentHealth = maxHealth;
            return;
        }
        //if going to die.
        else if (currentHealth + change <= 0)
        {
            currentHealth = 0;
            Die();
            return;
        }
        currentHealth += change;
    }

    /// <summary>
    /// respawns the character.
    /// </summary>
    public void Die()
    {
        currentHealth = maxHealth;
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("spawnPoint");
        transform.position = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].transform.position;
    }

    /// <summary>
    /// when character wants to shoot.
    /// </summary>
    protected void Shoot()
    {
        if (rateOfFireTimer >= weapon.rateOfFire)
        {
            rateOfFireTimer = 0;
            weapon.Fire(weaponGameObject, selectedBulletPrefab);
        }
    }
}
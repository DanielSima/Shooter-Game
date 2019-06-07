using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Initializes stuff needed before starting the game
/// </summary>
public class Initialization : MonoBehaviour {

	void Awake ()
    {
        //load prefabs
        Enemy.enemyPrefab = Resources.Load<GameObject>("enemy");
        Character.AssaultRiflePrefab = Resources.Load<GameObject>("assaultRifle");
        Character.ShotgunPrefab = Resources.Load<GameObject>("shotgun");
        Character.MinigunPrefab = Resources.Load<GameObject>("minigun");
        Character.FireBulletPrefab = Resources.Load<GameObject>("fireBullet");
        Character.ExplosiveBulletPrefab = Resources.Load<GameObject>("explosiveBullet");
        Character.ClassicBulletPrefab = Resources.Load<GameObject>("classicBullet");

        //spawn enemies 
        StartCoroutine(EnemySpawnCoroutine());

    }

    /// <summary>
    /// Spanw more enemies after time.
    /// </summary>
    /// <returns></returns>
    public IEnumerator EnemySpawnCoroutine()
    {
        while (GameObject.FindGameObjectsWithTag("character").Length < 10)
        {
            Enemy.Spawn(1);
            yield return new WaitForSeconds(10);
        }
    }
}

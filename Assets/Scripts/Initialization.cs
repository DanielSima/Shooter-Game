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
        //TODO increase difficluty
        Enemy.Spawn(1);

    }
}

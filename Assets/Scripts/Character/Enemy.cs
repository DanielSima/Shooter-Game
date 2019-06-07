using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

//code by ZefanS from https://answers.unity.com/questions/1162762/enemy-ai-for-shooting-game.html, modifed a bit

/// <summary>
/// Non Player Character which attacks the player.
/// </summary>
public class Enemy : Character
{
    public Transform target;
    public static GameObject enemyPrefab;

    Vector3 targetPosition;
    Vector3 thisPosition;

    public float MaximumAttackDistance = 25f;
    public float MinimumDistanceFromPlayer = 5f;

    private new void Start()
    {
        base.Start();

        //select random weapon and bullet
        List<GameObject> bullets = new List<GameObject> { ClassicBulletPrefab, FireBulletPrefab, ExplosiveBulletPrefab};
        List<GameObject> weapons = new List<GameObject> { AssaultRiflePrefab, ShotgunPrefab, MinigunPrefab };
        selectedBulletPrefab = bullets[UnityEngine.Random.Range(0, bullets.Count)]; 
        selectedWeaponPrefab = weapons[UnityEngine.Random.Range(0, weapons.Count)];

        target = GameObject.FindGameObjectWithTag("Player").transform;

        SpawnWeapon();

        StartCoroutine(weapon.ReloadCoroutine());
    }

    private void Update()
    {
        thisPosition = this.GetComponent<Rigidbody>().worldCenterOfMass;
        targetPosition = target.GetComponent<Rigidbody>().worldCenterOfMass;

        float distance = Vector3.Distance(targetPosition, thisPosition);
        RaycastHit hit;
        Vector3 rayDirection = targetPosition - thisPosition;
        NavMeshAgent agent = this.GetComponent<NavMeshAgent>();

        if (Physics.Raycast(thisPosition, rayDirection, out hit))
        {
            UnityEngine.Debug.DrawLine(thisPosition, targetPosition, Color.red);
            //if enemy sees the player
            if (hit.transform == target && distance <= MaximumAttackDistance)
            {
                LookAtTarget();
                Shoot();
                agent.destination = targetPosition;
                agent.isStopped = false;
                if (distance <= MinimumDistanceFromPlayer)
                {
                    agent.isStopped = true;
                }

            }
            //change the randDestination if already there
            else if (agent.remainingDistance < 1)
            {
                //make enemy walk around the map if doesn't see the player
                GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("spawnPoint");
                Vector3 randDestination = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].transform.position;
                agent.destination = randDestination;
            }
        }
        rateOfFireTimer += Time.deltaTime;

    }
    /// <summary>
    /// aim at the player.
    /// </summary>
    private void LookAtTarget()
    {
        //rotate horizontally the enemy
        var dir = target.position - transform.position;
        dir.y = 0;
        var rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

        //rotate weapon vertically
        weaponGameObject.transform.LookAt(targetPosition);
    }

    /// <summary>
    /// spawns specifed amount of enemies.
    /// </summary>
    public static void Spawn(int amount)
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("spawnPoint");
        for (int i = 0; i < amount; i++)
        {
            Vector3 position = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].transform.position;
            Instantiate(enemyPrefab,position,new Quaternion());
        }
    }

    public override void Die()
    {
        target.GetComponent<Player>().score++;
        base.Die();
    }

    /// <summary>
    /// Restart the enemy count
    /// </summary>
    public static void Restart()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("character");

        foreach(GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}
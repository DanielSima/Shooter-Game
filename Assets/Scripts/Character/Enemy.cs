using System;
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

    public float MaximumAttackDistance = 25;
    public float MinimumDistanceFromPlayer = 1f;

    bool haveSeen = false;
    float timer = 0;

    private new void Start()
    {
        base.Start();

        //TODO asign weapon here
        selectedBulletPrefab = FireBulletPrefab;
        selectedWeaponPrefab = AssaultRiflePrefab;

        target = GameObject.FindGameObjectWithTag("Player").transform;

        SpawnWeapon();

    }

    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        RaycastHit hit;
        Vector3 rayDirection = target.position - transform.position;
        NavMeshAgent agent = this.GetComponent<NavMeshAgent>();

        
        if (distance <= MaximumAttackDistance)
        {
            //TODO target position is not in center of mesh
            if (Physics.Raycast(transform.position, rayDirection, out hit))
            {
                //if enemy sees the player
                if (hit.transform == target)
                {
                    LookAtTarget();
                    haveSeen = true;
                    timer = 0;
                    Shoot();
                }

                //if doesn't see but have seen at least 2s ago
                if (haveSeen == true && hit.transform != target)
                {
                    agent.destination = target.position;
                }

                //if haven't seen for 2s
                if (timer > 2.0f && haveSeen == true)
                {
                    haveSeen = false;
                    agent.isStopped = true;
                }
                timer += Time.deltaTime;
            }

            rateOfFireTimer += Time.deltaTime;
        }
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
        weaponGameObject.transform.LookAt(target);
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
}
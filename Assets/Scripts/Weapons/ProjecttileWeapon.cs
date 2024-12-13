using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjecttileWeapon : Weapon
{
    public EemeyDamager damager;
    public Projecttile projecttile;
    private float shotCounter;
    public float weaponRange;
    public LayerMask whatIsEnemy;

    void Start()
    {
        SetStats();
    }

    void Update()
    {
        if (statsUpdated == true)
        {
            statsUpdated = false;
            SetStats();
        }

        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0)
        {
            shotCounter = Stats[weaponLevel].timeBeteenSpawns;

            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, weaponRange * Stats[weaponLevel].range, whatIsEnemy);

            if (enemies.Length > 0)
            {
                for (int i = 0; i < Stats[weaponLevel].amount; i++)
                {
                    // 寻找最近的敌人
                    Vector3 targetPosition = enemies[UnityEngine.Random.Range(0, enemies.Length)].transform.position;
                    Vector3 direction = targetPosition - transform.position;

                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    angle -= 90;
                    projecttile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    Instantiate(projecttile, projecttile.transform.position, projecttile.transform.rotation).gameObject.SetActive(true);
                }

            }
        }
    }

    void SetStats()
    {
        damager.damageAmount = Stats[weaponLevel].damage;
        transform.localScale = Vector3.one * Stats[weaponLevel].range;
        damager.lifeTime = Stats[weaponLevel].durantion;

        shotCounter = 0f;

        projecttile.moveSpeed = Stats[weaponLevel].speed;

    }
}

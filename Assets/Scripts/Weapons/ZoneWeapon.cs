using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneWeapon : Weapon
{
    public EemeyDamager damager;

    private float spawnTime, spawnCounter;
    // Start is called before the first frame update
    void Start()
    {
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (statsUpdated == true)
        {
            statsUpdated = false;
            SetStats();
        }

        spawnCounter -= Time.deltaTime;

        if (spawnCounter <= 0f)
        {
            spawnCounter = spawnTime;
            Instantiate(damager, damager.transform.position, Quaternion.identity, transform).gameObject.SetActive(true);
        }
    }


    void SetStats()
    {
        damager.damageAmount = Stats[weaponLevel].damage;
        damager.lifeTime = Stats[weaponLevel].durantion;

        damager.timeBeteenDamage = Stats[weaponLevel].speed;

        damager.transform.localScale = Vector3.one * Stats[weaponLevel].range;

        spawnTime = Stats[weaponLevel].timeBeteenSpawns;

        spawnCounter = 0f;

    }
}

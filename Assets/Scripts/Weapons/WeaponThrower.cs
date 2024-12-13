using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrower : Weapon
{

    public EemeyDamager damager;

    private float throwCounter;

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

        throwCounter -= Time.deltaTime;
        if (throwCounter <= 0)
        {
            throwCounter = Stats[weaponLevel].timeBeteenSpawns;

            for (int i = 0; i < Stats[weaponLevel].amount; i++)
            {

                Instantiate(damager, damager.transform.position, damager.transform.rotation).gameObject.SetActive(true);
            }

        }
    }

    void SetStats()
    {
        damager.damageAmount = Stats[weaponLevel].damage;
        transform.localScale = Vector3.one * Stats[weaponLevel].range;
        damager.lifeTime = Stats[weaponLevel].durantion;

        throwCounter = 0f;
    }
}

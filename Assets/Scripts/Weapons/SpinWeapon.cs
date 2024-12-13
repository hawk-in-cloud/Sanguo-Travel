using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpinWeapon : Weapon
{
    public float rotationSpeed = 180f;
    public Transform holder, fireballToSpawn;
    public float timeBetweenSpawn = 4f;
    private float spawnCounter;
    public EemeyDamager damager;

    void Start()
    {
        SetStats();
    }

    void Update()
    {
        Rotation();
        KirakiraHandler();

        if (statsUpdated == true)
        {
            statsUpdated = false;
            SetStats();
        }
    }

    void Rotation()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, Time.deltaTime * rotationSpeed * Stats[weaponLevel].speed + holder.rotation.eulerAngles.z);
    }

    // 生成火球逻辑
    void KirakiraHandler()
    {
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = timeBetweenSpawn;
            // 生成数量 以及生成的角度
            for (int i = 0; i < Stats[weaponLevel].amount; i++)
            {
                float rot = (360f / Stats[weaponLevel].amount) * i;
                Instantiate(fireballToSpawn, fireballToSpawn.position, Quaternion.Euler(0f, 0f, rot), holder).gameObject.SetActive(true);
            }
        }
    }

    public void SetStats()
    {
        damager.damageAmount = Stats[weaponLevel].damage;
        transform.localScale = Vector3.one * Stats[weaponLevel].range;

        timeBetweenSpawn = Stats[weaponLevel].timeBeteenSpawns;

        damager.lifeTime = Stats[weaponLevel].durantion;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttackWeapon : Weapon
{
    public EemeyDamager damager;

    private float attackCounter, direction;

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

        attackCounter -= Time.deltaTime;
        if (attackCounter <= 0)
        {
            attackCounter = Stats[weaponLevel].timeBeteenSpawns;

            direction = Input.GetAxisRaw("Horizontal");

            if (direction != 0)
            {
                if (direction > 0)
                {
                    damager.transform.rotation = Quaternion.identity;
                }
                else
                {
                    damager.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                }
            }

            Instantiate(damager, damager.transform.position, damager.transform.rotation, transform).gameObject.SetActive(true);
            // 生成数量 以及生成的角度
            for (int i = 1; i < Stats[weaponLevel].amount; i++)
            {
                float rot = (360f / Stats[weaponLevel].amount) * i;
                Instantiate(damager, damager.transform.position, Quaternion.Euler(0f, 0f, damager.transform.rotation.eulerAngles.z + rot), transform).gameObject.SetActive(true);
            }

        }
    }

    void SetStats()
    {
        damager.damageAmount = Stats[weaponLevel].damage;
        transform.localScale = Vector3.one * Stats[weaponLevel].range;
        damager.lifeTime = Stats[weaponLevel].durantion;

        attackCounter = 0f;
    }
}

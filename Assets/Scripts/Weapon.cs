
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<WeaponStats> Stats;
    public int weaponLevel;

    public Sprite icon;

    [HideInInspector]
    public bool statsUpdated;

    public void LevelUp()
    {
        if (weaponLevel < Stats.Count - 1)
        {
            weaponLevel++;
            statsUpdated = true;

            // 武器升满之后 升级界面不再显示
            if (weaponLevel >= Stats.Count - 1)
            {
                PlayerController.instance.fullyLevelledWeapons.Add(this);
                PlayerController.instance.assignedWeapons.Remove(this);
            }
        }
    }

}


[System.Serializable]
public class WeaponStats
{

    public float speed, damage, range, timeBeteenSpawns, amount, durantion;
    public string upgradeText;

}
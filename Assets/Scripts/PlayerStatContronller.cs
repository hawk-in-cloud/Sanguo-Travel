using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatController : MonoBehaviour
{
    public static PlayerStatController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<PlayerStatValue> moveSpeed, health, pickRange, maxWeapons;
    public int moveSpeedLevelCount, healthLevelCount, pickRangeLevelCount;

    public int moveSpeedLevel, healthLevel, pickRangeLevel, maxWeaponsLevel;

    private void Start()
    {
        for (int i = moveSpeed.Count - 1; i < moveSpeedLevelCount; i++)
        {
            moveSpeed.Add(new PlayerStatValue(moveSpeed[i].cost + moveSpeed[1].cost, moveSpeed[i].value + (moveSpeed[1].value - moveSpeed[0].value)));
        }

        for (int i = health.Count - 1; i < healthLevelCount; i++)
        {
            health.Add(new PlayerStatValue(health[i].cost + health[1].cost, health[i].value + (health[1].value - health[0].value)));
        }

        for (int i = pickRange.Count - 1; i < pickRangeLevelCount; i++)
        {
            pickRange.Add(new PlayerStatValue(pickRange[i].cost + pickRange[1].cost, pickRange[i].value + (pickRange[1].value - pickRange[0].value)));
        }
    }


    private void Update()
    {
        if (UiContronller.instance.levelupPanel.activeSelf == true)
        {
            UpdateDisplay();
        }
    }


    public void UpdateDisplay()
    {
        if (moveSpeedLevel < moveSpeed.Count - 1)
        {
            UiContronller.instance.moveSpeedUpgradeDisaplay.UpdateDisplay(moveSpeed[moveSpeedLevel + 1].cost, moveSpeed[moveSpeedLevel].value, moveSpeed[moveSpeedLevel + 1].value);
        }
        else
        {
            UiContronller.instance.moveSpeedUpgradeDisaplay.ShowMaxLevel();
        }

        if (healthLevel < health.Count - 1)
        {
            UiContronller.instance.healthUpgradeDisaplay.UpdateDisplay(health[healthLevel + 1].cost, health[healthLevel].value, health[healthLevel + 1].value);
        }
        else
        {
            UiContronller.instance.healthUpgradeDisaplay.ShowMaxLevel();
        }

        if (pickRangeLevel < pickRange.Count - 1)
        {
            UiContronller.instance.pickupUpgradeDisaplay.UpdateDisplay(pickRange[pickRangeLevel + 1].cost, pickRange[pickRangeLevel].value, pickRange[pickRangeLevel + 1].value);
        }
        else
        {
            UiContronller.instance.pickupUpgradeDisaplay.ShowMaxLevel();
        }

        if (maxWeaponsLevel < maxWeapons.Count - 1)
        {
            UiContronller.instance.maxWeaponUpgradeDisaplay.UpdateDisplay(maxWeapons[maxWeaponsLevel + 1].cost, maxWeapons[maxWeaponsLevel].value, maxWeapons[maxWeaponsLevel + 1].value);
        }
        else
        {
            UiContronller.instance.maxWeaponUpgradeDisaplay.ShowMaxLevel();
        }
    }

    public void PurchaseMoveSpeed()
    {
        moveSpeedLevel++;
        PlayerController.instance.moveSpeed = moveSpeed[moveSpeedLevel].value;
        UpdateDisplay();
    }
    public void PurchaseHealth()
    {
        healthLevel++;
        PlayerHealthController.instance.maxHealth = health[healthLevel].value;
        PlayerHealthController.instance.currentHealth += health[healthLevel].value - health[healthLevel - 1].value;
        UpdateDisplay();
    }
    public void PurchasePickupRange()
    {
        pickRangeLevel++;
        PlayerController.instance.pickRanger = pickRange[pickRangeLevel].value;
        UpdateDisplay();
    }
    public void PurchaseMaxWeapon()
    {
        maxWeaponsLevel++;
        PlayerController.instance.maxWeapons = (int)maxWeapons[maxWeaponsLevel].value;
        UpdateDisplay();
    }

}

[System.Serializable]
public class PlayerStatValue
{
    public int cost;
    public float value;

    public PlayerStatValue(int newCost, float newValue)
    {
        cost = newCost;
        value += newValue;
    }

}
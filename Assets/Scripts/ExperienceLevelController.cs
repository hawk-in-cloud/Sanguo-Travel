using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExperienceLevelController : MonoBehaviour
{
    public static ExperienceLevelController instance;
    private void Awake()
    {
        instance = this;
    }
    public int currentExperience;
    public ExpPickup pickup;
    public List<int> expLevels;
    public int currentLevel = 1, levelCount = 100;
    public List<Weapon> weapon2Upgrade;

    void Start()
    {
        while (expLevels.Count < levelCount)
        {
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f));
        }
    }

    public void GetExp(int amountToGet)
    {
        currentExperience += amountToGet;
        if (currentExperience >= expLevels[currentLevel])
        {
            LevelUp();
        }
        UiContronller.instance.UpdateExp(currentExperience, expLevels[currentLevel], currentLevel);
    }

    public void SpawnExp(Vector3 position, int expValue)
    {
        Instantiate(pickup, position, Quaternion.identity).expValue = expValue;
    }

    // 升级需要处理的逻辑
    public void LevelUp()
    {
        currentExperience -= expLevels[currentLevel];
        currentLevel++;
        if (currentLevel >= expLevels.Count)
        {
            currentLevel = expLevels.Count - 1;
        }
        UiContronller.instance.levelupPanel.SetActive(true);
        Time.timeScale = 0f;

        weapon2Upgrade.Clear();
        List<Weapon> tempWeapons = new List<Weapon>(PlayerController.instance.assignedWeapons);
        if (tempWeapons.Count > 0)
        {
            int selected = Random.Range(0, tempWeapons.Count);
            weapon2Upgrade.Add(tempWeapons[selected]);
            tempWeapons.RemoveAt(selected);
        }

        if (PlayerController.instance.assignedWeapons.Count + PlayerController.instance.fullyLevelledWeapons.Count < PlayerController.instance.maxWeapons)
        {
            tempWeapons.AddRange(PlayerController.instance.unassignedWeapons);
        }

        for (int i = weapon2Upgrade.Count; i < 3; i++)
        {
            if (tempWeapons.Count > 0)
            {
                int selected = Random.Range(0, tempWeapons.Count);
                weapon2Upgrade.Add(tempWeapons[selected]);
                tempWeapons.RemoveAt(selected);
            }
        }

        for (int i = 0; i < weapon2Upgrade.Count; i++)
        {
            UiContronller.instance.levelUpSelectButtons[i].UpdateButtonDisplay(weapon2Upgrade[i]);
        }

        for (int i = 0; i < UiContronller.instance.levelUpSelectButtons.Length; i++)
        {
            if (i < weapon2Upgrade.Count)
            {
                UiContronller.instance.levelUpSelectButtons[i].gameObject.SetActive(true);
            }
            else
            {
                UiContronller.instance.levelUpSelectButtons[i].gameObject.SetActive(false);
            }
        }


        PlayerStatController.Instance.UpdateDisplay();
    }
}

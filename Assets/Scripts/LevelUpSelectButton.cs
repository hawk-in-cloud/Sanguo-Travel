using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpSelectButton : MonoBehaviour
{
    public TMP_Text upgradeDescText, nameLevelText;
    public Image weaponIcon;

    private Weapon assignedWeapon;

    public void UpdateButtonDisplay(Weapon weapon)
    {
        weaponIcon.sprite = weapon.icon;
        if (weapon.gameObject.activeSelf == true)
        {
            upgradeDescText.text = weapon.Stats[weapon.weaponLevel].upgradeText;
            nameLevelText.text = weapon.name + " Lv." + (weapon.weaponLevel + 1);
        }
        else
        {
            upgradeDescText.text = "解锁 " + weapon.name;
            nameLevelText.text = weapon.name;
        }

        assignedWeapon = weapon;
    }

    public void SelectUpgrade()
    {
        if (assignedWeapon != null)
        {
            if (assignedWeapon.gameObject.activeSelf == true)
            {
                assignedWeapon.LevelUp();

            }
            else
            {
                PlayerController.instance.AddWeapon(assignedWeapon);
            }

            UiContronller.instance.levelupPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}

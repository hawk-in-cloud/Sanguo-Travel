using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiContronller : MonoBehaviour
{
    public static UiContronller instance;
    private void Awake()
    {
        instance = this;
    }
    public Slider explvSlider;
    public TMP_Text expLvText;
    public LevelUpSelectButton[] levelUpSelectButtons;
    public GameObject levelupPanel;

    public TMP_Text coinText;

    public PlayerStatUpgradeDisaplay moveSpeedUpgradeDisaplay, healthUpgradeDisaplay, pickupUpgradeDisaplay, maxWeaponUpgradeDisaplay;

    public TMP_Text timeText;
    public GameObject endScene;
    public TMP_Text endTimeText;

    public string mainMenuName;

    public GameObject pauseScene;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnPause();
        }
    }

    public void UpdateExp(int curExp, int levelExp, int currentLv)
    {
        explvSlider.maxValue = levelExp;
        explvSlider.value = curExp;
        expLvText.text = "Level : " + currentLv;
    }

    public void SkipLevelUp()
    {
        levelupPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void UpdateTimer(float timer)
    {
        float minutes = Mathf.FloorToInt(timer / 60f);
        float seconds = Mathf.FloorToInt(timer % 60f);
        timeText.text = "Time :  " + minutes + " : " + seconds.ToString("00");
    }

    public void UpdateCoin()
    {
        coinText.text = "Coins : " + CoinContronller.Instance.currentCoin; ;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuName);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseUnPause()
    {
        if (pauseScene.activeSelf == false)
        {
            pauseScene.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseScene.SetActive(false);
            if (levelupPanel.activeSelf == false)
            {
                Time.timeScale = 1f;
            }
        }
    }
}

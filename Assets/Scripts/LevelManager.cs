using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public bool gameActice;
    public float timer;

    public float waitToshowEndScene = 1f;

    void Start()
    {

    }


    void Update()
    {
        if (gameActice == true)
        {
            timer += Time.deltaTime;
            UiContronller.instance.UpdateTimer(timer);
        }
    }

    public void EndLevel()
    {
        gameActice = false;

        StartCoroutine(EndLevelCo());
    }

    IEnumerator EndLevelCo()
    {
        yield return new WaitForSeconds(waitToshowEndScene);

        float minutes = Mathf.FloorToInt(timer / 60f);
        float seconds = Mathf.FloorToInt(timer % 60f);

        UiContronller.instance.endTimeText.text = minutes + " 分 " + seconds.ToString("00") + " 秒";
        UiContronller.instance.endScene.SetActive(true);
    }
}

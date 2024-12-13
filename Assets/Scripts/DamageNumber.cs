using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    public TMP_Text damageText;

    public float lifetime;
    private float lifeCounter;


    private float floatSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        lifeCounter = lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeCounter > 0)
        {
            lifeCounter -= Time.deltaTime;

            if (lifeCounter <= 0)
            {
                DamageController.instance.ReturnToPool(this);
            }
        }

        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
    }

    public void SetUp(int damage)
    {
        lifeCounter = lifetime;
        damageText.text = damage.ToString();
    }
}

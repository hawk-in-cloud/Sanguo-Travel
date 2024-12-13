using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{


    public static PlayerHealthController instance;
    // Start is called before the first frame update
    public float currentHealth = 0f;
    public float maxHealth = 25.0f;
    public Slider healthSlider;

    private Transform target;

    public GameObject deathEffect;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        maxHealth = PlayerStatController.Instance.health[0].value;
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        target = PlayerHealthController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void TakeDamage(float damage)
    {

        currentHealth -= damage;
        GetComponent<DamageFlashEffect>().Damage();

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            gameObject.SetActive(false);
            LevelManager.Instance.EndLevel();
            Instantiate(deathEffect, transform.position, transform.rotation);
        }

        healthSlider.value = currentHealth;
    }
}

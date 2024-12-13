using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EemeyDamager : MonoBehaviour
{
    public float damageAmount = 1f;
    public float lifeTime = 3f;
    public float growSpeed = 5f;
    private Vector3 targetSize;
    public bool showKnockback = true;
    public bool destroyParent = true;
    public bool damageOverTime;
    public float timeBeteenDamage;
    private float damageCounter;
    // 检测范围内的敌人
    private List<EnemyContronller> enemiesInRange = new List<EnemyContronller>();

    public bool destroyOnImpact;

    void Start()
    {
        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            targetSize = Vector3.zero;

            if (transform.localScale.x == 0f)
            {
                Destroy(gameObject);

                if (destroyParent)
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }

        RangeDamageHandler();
    }

    private void RangeDamageHandler()
    {
        if (damageOverTime == true)
        {
            damageCounter -= Time.deltaTime;
            if (damageCounter <= 0)
            {
                damageCounter = timeBeteenDamage;
                for (int i = 0; i < enemiesInRange.Count; i++)
                {
                    if (enemiesInRange[i] != null)
                    {
                        enemiesInRange[i].TakeDamage(damageAmount, showKnockback);
                    }
                    else
                    {
                        enemiesInRange.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (damageOverTime == false)
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.GetComponent<EnemyContronller>().TakeDamage(damageAmount, showKnockback);

                if (destroyOnImpact)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            if (other.gameObject.tag == "Enemy")
            {
                enemiesInRange.Add(other.GetComponent<EnemyContronller>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (damageOverTime == true)
        {
            if (other.gameObject.tag == "Enemy")
            {
                enemiesInRange.Remove(other.GetComponent<EnemyContronller>());
            }

        }

    }
}

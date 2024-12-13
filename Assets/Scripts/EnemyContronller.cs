using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContronller : MonoBehaviour
{

    private Transform target;
    private Rigidbody2D rb;
    public float moveSpeed = 1.0f;
    public float damage = 1.5f;
    private bool faceRight = false;
    public float hitWaitTime = 1f;
    private float hitCounter;

    public float health = 5f;

    public float knockBackTime = 0.5f;
    private float knockbackCounter;

    public int expToGive = 1;

    public int coinValue = 1;

    public float coinDropRate = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        target = PlayerHealthController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerController.instance.gameObject.activeSelf == false)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        else
        {
            Move();
            FaceTarget();
            if (hitCounter > 0)
            {
                hitCounter -= Time.deltaTime;
            }
        }
    }

    void Move()
    {

        if (target != null)
        {
            HitedBack();
            rb.velocity = (target.position - transform.position).normalized * moveSpeed;
        }

    }
    void FaceTarget()
    {
        Vector3 dir = target.position - transform.position;
        // 检查是否需要翻转Sprite
        if (dir.x > 0 && !faceRight)
        {
            Flip();
        }
        else if (dir.x < 0 && faceRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        faceRight = !faceRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && hitCounter <= 0f)
        {
            PlayerHealthController.instance.TakeDamage(damage);
            hitCounter = hitWaitTime;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        GetComponent<DamageFlashEffect>().Damage();

        if (health <= 0)
        {
            Destroy(gameObject);
            ExperienceLevelController.instance.SpawnExp(transform.position, expToGive);

            if (Random.value <= coinDropRate)
            {

                CoinContronller.Instance.DropCoin(transform.position, coinValue);
            }
        }

        DamageController.instance.SpawnDamage(damage, transform.position);
    }

    public void TakeDamage(float damage, bool shouldKnockback)
    {
        TakeDamage(damage);

        if (shouldKnockback == true)
        {
            knockbackCounter = knockBackTime;
        }

    }

    void HitedBack()
    {
        if (knockbackCounter > 0)
        {
            knockbackCounter -= Time.deltaTime;
            if (moveSpeed > 0)
            {
                moveSpeed = -moveSpeed * 2f;
            }

            if (knockbackCounter <= 0)
            {
                moveSpeed = Mathf.Abs(moveSpeed) * 0.5f;
            }

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowWeapon : Weapon
{
    private Rigidbody2D rb;
    public float throwForce = 5f;

    public float rotateSpeed = 5f;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Random.Range(-throwForce, throwForce), throwForce);
    }

    void Update()
    {
        rotate();
    }


    void rotate()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + (rotateSpeed * 360f * Time.deltaTime * Mathf.Sign(rb.velocity.x)));
    }
}

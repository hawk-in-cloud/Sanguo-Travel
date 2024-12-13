using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projecttile : Weapon
{

    public float moveSpeed;

    void Start()
    {

    }

    void Update()
    {
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }


    void SetStats()
    {


    }
}

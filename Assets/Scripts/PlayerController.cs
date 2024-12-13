using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private void Awake()
    {
        instance = this;
    }
    public float moveSpeed = .05f;
    private Animator animator;
    private SpriteRenderer sp;
    public float pickRanger = 1.5f;
    public List<Weapon> unassignedWeapons, assignedWeapons;
    public int maxWeapons = 3;

    [HideInInspector]
    public List<Weapon> fullyLevelledWeapons = new List<Weapon>();



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();

        if (assignedWeapons.Count == 0)
        {
            AddWeapon(UnityEngine.Random.Range(0, unassignedWeapons.Count));
        }

        moveSpeed = PlayerStatController.Instance.moveSpeed[0].value;
        pickRanger = PlayerStatController.Instance.pickRange[0].value;
        maxWeapons = (int)PlayerStatController.Instance.maxWeapons[0].value;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    void Move()
    {
        Vector3 moveInput = new Vector3(0f, 0f, 0f)
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = Input.GetAxisRaw("Vertical")
        };


        if (Mathf.Abs(moveInput.x) > 0f || Mathf.Abs(moveInput.y) > 0f)
        {
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }

        if (Mathf.Abs(moveInput.y) > 0f)
        {
            animator.SetBool("isY", true);
        }
        else
        {
            animator.SetBool("isY", false);
        }

        if (moveInput.x > 0f)
        {
            // transform.localScale = new Vector3(-1f, 1f, 1f);
            sp.flipX = true;
        }
        else
        {
            sp.flipX = false;
        }

        animator.SetFloat("Horizontal", moveInput.x);
        animator.SetFloat("Vertical", moveInput.y);

        moveInput.Normalize();

        transform.position += moveInput * moveSpeed * Time.deltaTime;

    }         



    public void AddWeapon(int weaponNumber)
    {
        if (weaponNumber < unassignedWeapons.Count)
        {
            assignedWeapons.Add(unassignedWeapons[weaponNumber]);

            unassignedWeapons[weaponNumber].gameObject.SetActive(true);
            unassignedWeapons.RemoveAt(weaponNumber);
        }
    }

    public void AddWeapon(Weapon weapon2Add)
    {
        weapon2Add.gameObject.SetActive(true);
        assignedWeapons.Add(weapon2Add);
        unassignedWeapons.Remove(weapon2Add);
    }
}

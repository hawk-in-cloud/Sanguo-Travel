using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public static DamageController instance;

    private void Awake()
    {
        instance = this;
    }

    public DamageNumber numberToSpawn;
    public Transform numberCanvas;

    private List<DamageNumber> numberPool = new List<DamageNumber>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnDamage(float damageAmount, Vector3 location)
    {
        int rounded = Mathf.RoundToInt(damageAmount);
        // DamageNumber newDamage = Instantiate(numberToSpawn, location, Quaternion.identity, numberCanvas);


        DamageNumber newDamage = GetFromPool();
        newDamage.SetUp(rounded);
        newDamage.gameObject.SetActive(true);
        newDamage.transform.position = location;
    }

    public DamageNumber GetFromPool()
    {
        DamageNumber numberToOutput = null;

        if (numberPool.Count == 0)
        {
            numberToOutput = Instantiate(numberToSpawn, numberCanvas);
        }
        else
        {
            numberToOutput = numberPool[numberPool.Count - 1];
            numberPool.RemoveAt(numberPool.Count - 1);
            numberToOutput.gameObject.SetActive(true);
        }

        return numberToOutput;

    }
    public void ReturnToPool(DamageNumber numberToReturn)
    {
        numberToReturn.gameObject.SetActive(false);
        numberPool.Add(numberToReturn);
    }
}

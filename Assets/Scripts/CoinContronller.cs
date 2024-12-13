using UnityEngine;

public class CoinContronller : MonoBehaviour
{
    public static CoinContronller Instance;

    private void Awake()
    {
        Instance = this;
    }

    public int currentCoin;
    public CoinPickup coin;


    public void AddCoin(int coin2Get)
    {
        currentCoin += coin2Get;
        UiContronller.instance.UpdateCoin();
    }

    public void DropCoin(Vector3 pos, int Value)
    {
        CoinPickup newCoin = Instantiate(coin, pos + new Vector3(0.2f, 0.1f, 0f), Quaternion.identity);
        newCoin.coin = Value;
        newCoin.gameObject.SetActive(true);
    }
}

using UnityEngine;

public class CoinEventTrigger : MonoBehaviour
{
    public CoinSpawner coinSpawner;
    public float eventDelay = 10f; // Delay before event starts

    void Start()
    {
        Invoke("StartCoinEvent", eventDelay);
    }

    void StartCoinEvent()
    {
        coinSpawner.SpawnCoins();
        Debug.Log("Coin event started!");
    }
}

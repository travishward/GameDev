using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Increase a coin counter on the player (you can add this to your PlayerXP or create a new script)
            // For example:
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
                inventory.AddCoin(coinValue);

            Destroy(gameObject);
        }
    }
}

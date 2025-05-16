using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int coinCount = 0;
    public int coinChallengeThreshold = 10; // Number of coins needed for a free level-up

    // Reference to PlayerXP so we can trigger a level-up
    public PlayerXP playerXP;

    public void AddCoin(int amount)
    {
        coinCount += amount;
        Debug.Log("Coins collected: " + coinCount);

        // Check if we've reached the threshold for a free level-up
        if (coinCount >= coinChallengeThreshold)
        {
            // Reset coin count (optional)
            coinCount = 0;

            // Trigger a "free level-up" directly on the PlayerXP
            if (playerXP != null)
            {
                Debug.Log("Coin threshold reached! Triggering a free level-up.");
                playerXP.TriggerLevelUp();
            }
            else
            {
                Debug.LogWarning("PlayerXP is not assigned in PlayerInventory!");
            }
        }
    }
}

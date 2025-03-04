using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    public int currentXP = 0;
    public int xpThreshold = 100;
    public int level = 1;

    // Reference to your PerkUIManager if you want to show a perk UI
    public PerkUIManager perkUIManager;

    public void AddXP(int amount)
    {
        currentXP += amount;
        if (currentXP >= xpThreshold)
        {
            TriggerLevelUp();
        }
    }

    public void TriggerLevelUp()
    {
        level++;
        currentXP = 0;
        xpThreshold += 50; // or any increment you want

        Debug.Log("Player leveled up to Level " + level);

        // If you have a perk UI, show it here
        if (perkUIManager != null)
        {
            perkUIManager.ShowPerkSelection();
        }
    }
}

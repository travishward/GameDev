using UnityEngine;
using UnityEngine.UI;

public class PerkUIManager : MonoBehaviour
{
    public GameObject perkPanel;
    public Button speedButton;
    public Button damageButton;
    public Button healthButton;

    void Start()
    {
        perkPanel.SetActive(false);

        speedButton.onClick.AddListener(() => ApplyPerk("Speed"));
        damageButton.onClick.AddListener(() => ApplyPerk("Damage"));
        healthButton.onClick.AddListener(() => ApplyPerk("Health"));
    }

    public void ShowPerkSelection()
    {
        Time.timeScale = 0;
        perkPanel.SetActive(true);

        // Unlock cursor to allow clicking
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ApplyPerk(string perkType)
    {
        // Example perk logic:
        Debug.Log("Applied perk: " + perkType);

        perkPanel.SetActive(false);
        Time.timeScale = 1;

        // Re-lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public float regenRate = 5f; // Health per second

    // UI References using TextMeshProUGUI
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI gameOverText;  // For the "Try Again" message

    // Restart button reference
    public Button restartButton;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        // Hide game over UI elements at start
        if (gameOverText != null)
            gameOverText.gameObject.SetActive(false);
        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(false);
            // Add a listener to the button to restart the game
            restartButton.onClick.AddListener(RestartGame);
        }
    }

    void Update()
    {
        // Only regenerate health if player is alive
        if (currentHealth > 0 && currentHealth < maxHealth)
        {
            int regenThisFrame = Mathf.RoundToInt(regenRate * Time.deltaTime);
            currentHealth = Mathf.Min(currentHealth + regenThisFrame, maxHealth);
            UpdateHealthUI();
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
            currentHealth = 0;

        if (damageText != null)
        {
            damageText.text = "Enemy dealt " + amount + " damage!";
        }
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth + "/" + maxHealth;
        }
    }

    void Die()
    {
        Debug.Log("Player died!");

        // Display Game Over message
        if (gameOverText != null)
        {
            gameOverText.text = "Try Again";
            gameOverText.gameObject.SetActive(true);
        }
        // Show restart button
        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(true);
        }
        // Pause the game
        Time.timeScale = 0f;
        // Unlock the cursor for UI interaction
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void RestartGame()
    {
        // Resume time and reload the current scene
        Time.timeScale = 1f;
        // Re-lock the cursor for gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

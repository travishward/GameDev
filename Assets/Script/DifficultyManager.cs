using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public float gameTime = 0f;
    public float scalingFactor = 0.1f;

    void Update()
    {
        gameTime += Time.deltaTime;
    }

    public float GetDifficultyMultiplier()
    {
        return 1 + (gameTime * scalingFactor);
    }
}

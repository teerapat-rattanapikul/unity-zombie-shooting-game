using UnityEngine;

public class BaseStat : MonoBehaviour
{
    public int maxHealth;
    protected int currentHealth;

    public float baseSpeed;
    protected float currentSpeed;

    // control falls speeds
    public float gravity;
    public float jumpHeight;

    public void Awake()
    {
        currentHealth = maxHealth;
        currentSpeed = baseSpeed;
    }

    public void ChangeHealth(int value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);
        if (gameObject.CompareTag("Player"))
        {
            GameManager.Instance.SetHealthBar(currentHealth, maxHealth);
        }
    }

    public void ChangeSpeed(float value)
    {
        currentSpeed += value;
    }

}
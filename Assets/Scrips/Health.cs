using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private Slider healthSlider;
    private float currentHealth;
    public float CurrentHealth => currentHealth; ///* para que no salga error
    private float maxHealth;
    public float MaxHealth{ set { maxHealth = value; }}
    [SerializeField]
    private UnityEvent onReceiveDamage;
    [SerializeField]
    private UnityEvent onDie;
    public void InitializeHealth()
    {
        currentHealth = maxHealth;
        UpdateBar(); ///* cambio antes del parcial}}}}}}
    }
    private void UpdateBar()
    {
        healthSlider.value = currentHealth / maxHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            onDie ?. Invoke();
        }
        else
        {
            onReceiveDamage ?. Invoke();
        }
        UpdateBar();
    }
}
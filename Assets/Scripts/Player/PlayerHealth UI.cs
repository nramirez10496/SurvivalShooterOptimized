using UnityEngine;
using UnityEngine.UI;


public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] Slider healthSlider;
    [SerializeField] Image damageImage;

    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    bool damaged;

    private void OnEnable()
    {
        playerHealth.OnHealthChanged += UpdateHealth;

        UpdateHealth(playerHealth.currentHealth, playerHealth.startingHealth, false);
    }

    private void OnDisable()
    {
        playerHealth.OnHealthChanged -= UpdateHealth;
    }

    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    void UpdateHealth (int currentHealth, int maxHealth, bool wasDamaged)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        if(wasDamaged)
        {

            damaged = true;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    private float _targetHealth; // ค่าเลือดเป้าหมายที่เราต้องการจะไปให้ถึง
    [SerializeField] private float _lerpSpeed = 5f; // ความเร็วในการเลื่อน (ยิ่งเยอะยิ่งเร็ว)

    public void SetMaxHealth(float maxHealth, float currentHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = currentHealth;
        _targetHealth = currentHealth; // เริ่มต้นให้เป้าหมายเท่ากับค่าปัจจุบัน

        if (fill != null) fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float currentHealth)
    {
        _targetHealth = currentHealth;
    }

    void Update()
    {
        if (slider.value != _targetHealth)
        {
            slider.value = Mathf.Lerp(slider.value, _targetHealth, _lerpSpeed * Time.deltaTime);

        }

        // อัปเดตสีตาม Value ของ Slider (Normalize เป็น 0-1)
        if (fill != null)
        {
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }
}
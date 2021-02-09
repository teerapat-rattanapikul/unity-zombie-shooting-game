using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieHealthBar : MonoBehaviour
{
    public Slider slider;
    public GameObject damageText;

    private void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    public void SetSliderValue(int health, int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.minValue = 0;
        slider.value = health;
    }

    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }

    public void Disable()
    {
        slider.gameObject.SetActive(false);
    }

    public void ShowDamageText(int damage, bool critical)
    {
        var cloneDamagedText = Instantiate(damageText, damageText.transform).GetComponent<TextMesh>();
        if (critical) {
            cloneDamagedText.color = Color.red;
        }
        cloneDamagedText.text = damage.ToString();
        cloneDamagedText.transform.SetParent(transform);
        cloneDamagedText.gameObject.SetActive(true);
    }

  
}

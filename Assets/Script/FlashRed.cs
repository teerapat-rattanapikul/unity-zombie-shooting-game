using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashRed : MonoBehaviour
{
    public Image flash;

    bool getDamage;

    public void StartFlash()
    {
        gameObject.SetActive(true);
        getDamage = true;
    }

    private void Update()
    {
        if (getDamage)
        {
            Color Opaque = new Color(1, 0, 0, 1);
            flash.color = Color.Lerp(flash.color, Opaque, 15 * Time.deltaTime);
            if (flash.color.a >= 0.8) //Almost Opaque, close enough
            {
                getDamage = false;
                gameObject.SetActive(false);
            }
        }
        if (!getDamage)
        {
            Color Transparent = new Color(1, 0, 0, 0);
            flash.color = Color.Lerp(flash.color, Transparent, 15 * Time.deltaTime);
        }
    }
}

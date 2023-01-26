using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    #region Variables


    #endregion

    #region Referencias

    public Slider slider;

    #endregion

    public void setMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = slider.maxValue;
    }

    public void SetCurrentHealth(int currentHealth)
    {
        slider.value = currentHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

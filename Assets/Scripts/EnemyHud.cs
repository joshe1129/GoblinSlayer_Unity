using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHud : MonoBehaviour
{
    #region VariableS

    #endregion

    #region Referencias

    public Slider health_slide;
  
  

    #endregion



    public void SetMaxHealth(float health)
    {
        health_slide.maxValue = health;
        health_slide.value = health;
    }
    public void SetLifeValue(float health)
    {
        health_slide.value = health;
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomSlider : MonoBehaviour
{
    [SerializeField] Image fillImage;    // Изображение-заполнитель
    [SerializeField] Text progressText;  // Текст текущего прогресса


    /// <summary>
    /// Установка нового значения слайдера
    /// </summary>
    /// <param name="value">Значение от 0 до 1</param>
    public void SetValue(float value)
    {
        fillImage.fillAmount = value;
        progressText.text = ((int)(value * 100)).ToString() + "%";
    }
}

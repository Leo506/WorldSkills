using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProgressSlider : MonoBehaviour
{
    [SerializeField] Image fill;
    [SerializeField] Text leftTimeText;

    TimeSpan endTime;
    float valuePerSecond;

    int id;


    /// <summary>
    /// Получить текущее значение слайдера
    /// </summary>
    public float GetValue()
    {
        return fill.fillAmount;
    }


    /// <summary>
    /// Настройка слайдера
    /// </summary>
    /// <param name="value">Время заполнения слайдера</param>
    /// <param name="id">Id объекта, к которому принадлежит слайдер</param>
    public void SetUp(DateTime value, int id)
    {
        endTime = value - DateTime.Now;

        leftTimeText.text = TimeText();

        fill.fillAmount = 0;
        valuePerSecond = 1 / (float)endTime.TotalSeconds;

        this.id = id;
    }


    /// <summary>
    /// Обновляет слайдер на 1 секунду по умолчанию
    /// </summary>
    /// <param name="seconds">Количество секунд, на которое надо обновить слайдер</param>
    public void UpdateSlider(int seconds = 1)
    {
        endTime = endTime.Subtract(new TimeSpan(0, 0, 1));
        leftTimeText.text = TimeText();
        fill.fillAmount += valuePerSecond * seconds;
    }


    public void SkipTime()
    {
        if (GameController.currentPlayer.crystalsBalance >= 10)
        {
            foreach (var item in FindObjectsOfType<UsingObject>())
            {
                if (item.id == this.id)
                {
                    item.GetComponent<Bed>()?.Skip();
                    return;
                }
            }
        }
    }


    string TimeText()
    {
        string days = endTime.Days.ToString() + " Days ";
        string hours = endTime.Hours.ToString() + " Hours ";
        string minutes = endTime.Minutes.ToString() + " Min ";
        string seconds = endTime.Seconds.ToString() + " Sec";

        string endString = "";

        // Первый вариант "Дни часы"
        if (endTime.Days != 0)
            endString += days + hours;

        // Второй вариат "Часы минуты"
        else if (endTime.Hours != 0)
            endString += hours + minutes;

        // Третий вариант "Минуты секунды"
        else if (endTime.Minutes != 0)
            endString += minutes + seconds;

        // Последний вариант "Секунды"
        else
            endString += seconds;

        return endString;
    }
}

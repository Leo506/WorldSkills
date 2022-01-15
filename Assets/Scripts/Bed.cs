using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bed : UsingObject
{
    [SerializeField] GameObject plantedSprite, grownSprite;  // Спрайты, отображаюшие все возможные состояния грядки
    [SerializeField] Canvas progressCanvas;
    [SerializeField] ProgressSlider progressSlider;

    ProgressSlider slider;
    GameObject currentState;

    public bool Plant()
    {
        if (GameController.currentPlayer.moneyBalance >= 0)
        {
            isAvailable = false;

            currentState = Instantiate(plantedSprite, transform);

            Vector2 spawnPos = Camera.main.WorldToScreenPoint(this.transform.position);
            slider = Instantiate(progressSlider, progressCanvas.transform);
            slider.transform.position = spawnPos;

            DateTime endTime = DateTime.Now.AddMinutes(1);
            slider.SetUp(endTime, id);
            StartCoroutine(Growing(endTime));

            MoneyController.instance.ChangeMoney(-20);
            
            return true;
        }

        return false;
    }

    public void Skip()
    {
        StopAllCoroutines();
        Destroy(currentState);
        Destroy(slider.gameObject);
        currentState = Instantiate(grownSprite, transform);
    }


    IEnumerator Growing(DateTime date)
    {
        while (DateTime.Now < date)
        {
            slider.UpdateSlider();
            yield return new WaitForSeconds(1);
        }

        Destroy(currentState);
        Destroy(slider.gameObject);
        currentState = Instantiate(grownSprite, transform);
    }

}

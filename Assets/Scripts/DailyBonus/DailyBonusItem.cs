using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyBonusItem : MonoBehaviour
{
    [SerializeField] Button getButton;
    [SerializeField] Image completeImage;

    bool isComplete = false;
    public bool IsCompleted
    {
        get
        {
            return isComplete;
        }

        set
        {
            if (value)
            {
                completeImage.gameObject.SetActive(true);
                getButton.gameObject.SetActive(false);
            }

            isComplete = value;
        }
    }


    bool isEnable = false;
    public bool IsEnable
    {
        get
        {
            return isEnable;
        }

        set
        {
            getButton.enabled = value;
            isEnable = value;
        }
    }


    public void ClaimBonus()
    {
        MoneyController.instance.ChangeCrystals(1);
        GameController.currentPlayer.lastDailyBonus += 1;
        GameController.currentPlayer.lastDailyBonusTime = System.DateTime.Now;
        Debug.Log(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        IsCompleted = true;
    }
}

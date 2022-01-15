using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyController : MonoBehaviour
{
    [SerializeField] Text moneyText, crystalsText;

    public static MoneyController instance { get; private set; }

    private void Awake()
    {
        instance = this;
        moneyText.text = GameController.currentPlayer.moneyBalance.ToString();
        crystalsText.text = GameController.currentPlayer.crystalsBalance.ToString();
    }


    /// <summary>
    /// Изменяет количество денег у игрока
    /// </summary>
    /// <param name="value">Величина изменения</param>
    public void ChangeMoney(int value)
    {
        if (GameController.currentPlayer.moneyBalance + value >= 0)
        {
            GameController.currentPlayer.moneyBalance += value;
            moneyText.text = GameController.currentPlayer.moneyBalance.ToString();
        }
    }


    /// <summary>
    /// Изменяет количество кристалов
    /// </summary>
    /// <param name="value">Величина изменения</param>
    public void ChangeCrystals(int value)
    {
        if (GameController.currentPlayer.crystalsBalance + value >= 0)
        {
            GameController.currentPlayer.crystalsBalance += value;
            crystalsText.text = GameController.currentPlayer.crystalsBalance.ToString();
        }
    }
}

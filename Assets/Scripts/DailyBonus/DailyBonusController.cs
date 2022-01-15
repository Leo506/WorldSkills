using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DailyBonusController : MonoBehaviour
{
    [SerializeField] DailyBonusItem[] items;
    [SerializeField] Canvas dailyBonusCanvas;


    private void Start()
    {
        if (CheckDailyBonus(GameController.currentPlayer))
        {
            dailyBonusCanvas.enabled = true;
            SetUp(GameController.currentPlayer);
        }
    }

    /// <summary>
    /// Проверяет, пришло ли время ежедневного бонуса у конкретного игрока
    /// </summary>
    /// <param name="player">Игрок, которого проверяем</param>
    /// <returns>True, если прошел день или больше с последнего ежедневного бонуса</returns>
    bool CheckDailyBonus(Player player)
    {
        return DateTime.Now >= player.lastDailyBonusTime.AddMinutes(1);
    }


    void SetUp(Player player)
    {
        // Устанавливаем все поля до текущего как собранные
        for (int i = 0; i < player.lastDailyBonus; i++)
        {
            if (items[i] != null)
                items[i].IsCompleted = true;
        }

        // Делаем текущую награду доступной для сбора
        items[player.lastDailyBonus].IsEnable = true;

        // Оставшиеся награды делаем недоступными
        for (int i = player.lastDailyBonus + 1; i < items.Length; i++)
        {
            items[i].IsEnable = false;
        }
    }
}

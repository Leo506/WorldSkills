using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] InputField playerName, login, password;
    [SerializeField] Text outText;

    public void TestConnection()
    {
        DBController.CreateConnection();
        if (DBController.CheckConnection())
            outText.text = "Соединение установлено";
        else
            outText.text = "Соединение не установлено";
    }

    public void TestConnectionStatus()
    {
        if (DBController.CheckConnection())
            outText.text = "Проверка соединения успешна";
        else
            outText.text = "Проверка соединения провалена";
    }

    public void TestRegisterPlayer()
    {
        Player player = new Player(playerName.text, login.text, password.text);
        DBController.RegisterUser(player);
    }

    public void TestShowPlayers()
    {
        outText.text = "";
        foreach (var item in DBController.GetAllPlayers())
        {
            outText.text += $"{item.fullname}, {item.login}, {item.password}\n";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;


/// <summary>
/// Структура, описывающая основные данные об игроке
/// </summary>
public struct Player
{
    public int id;
    public string fullname;
    public string login;
    public string password;
    public int lastDailyBonus;
    public bool educationStatus;
    public int moneyBalance;
    public int crystalsBalance;
    public DateTime lastDailyBonusTime;


    public Player(string fName, string login, string password)
    {
        this.id = -1;
        this.fullname = fName;
        this.login = login;
        this.password = password;
        lastDailyBonus = -1;
        educationStatus = false;
        moneyBalance = 0;
        crystalsBalance = 0;
        lastDailyBonusTime = DateTime.Now;
}

    public Player(MySqlDataReader reader)
    {
        id = (int)reader[0];
        fullname = reader[1].ToString();
        login = reader[2].ToString();
        password = reader[3].ToString();
        lastDailyBonus = (int)reader[4];
        educationStatus = (bool)reader[5];
        moneyBalance = (int)reader[6];
        crystalsBalance = (int)reader[7];

        if (reader[10].ToString() == "")
            lastDailyBonusTime = DateTime.MinValue;
        else
            lastDailyBonusTime = DateTime.Parse(reader[10].ToString());
    }
}

public class DBController
{
    // строка подключения к базе данных
    static string constr = "Server=127.0.0.1;Database=Player;port=3306;User ID=root;Password=26041986";

    public static MySqlConnection connection { get; private set; }


    /// <summary>
    /// Создаёт новое соединение с базой данных или, если оно уже существует, возвращет текущее
    /// </summary>
    /// <returns>Соединение с базой данных</returns>
    public static MySqlConnection CreateConnection()
    {
        if (!CheckConnection())
        {
            connection = new MySqlConnection(constr);
            connection.Open();
        }

        return connection;
    }


    /// <summary>
    /// Проверяет, открыто ли соединение с базой данных
    /// </summary>
    /// <returns>True, если соединение открыто, и false, если нет</returns>
    public static bool CheckConnection()
    {
        if (connection != null && connection.State == System.Data.ConnectionState.Open)
            return true;

        return false;
    }


    /// <summary>
    /// Регистрирует нового пользователя в базу даннных
    /// </summary>
    /// <param name="player">Структура с основными данными игрока</param>
    public static void RegisterUser(Player player)
    {
        if (!CheckConnection())
            return;

        string sql = $"insert into playerinfo (Fullname, Login, UserPassword) values (\"{player.fullname}\", \"{player.login}\", {player.password});";
        MySqlCommand command = new MySqlCommand(sql, connection);

        command.ExecuteNonQuery();
    }


    /// <summary>
    /// Получение списка всех игроков
    /// </summary>
    /// <returns>Список игроков</returns>
    public static List<Player> GetAllPlayers()
    {
        List<Player> players = new List<Player>();

        if (CheckConnection())
        {
            string sql = "select * from playerinfo;";
            MySqlCommand command = new MySqlCommand(sql, connection);

            MySqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                players.Add(new Player(reader));
            }
            reader.Close();
            reader.Dispose();
        }

        return players;
    }


    public static void SaveGame()
    {
        string sql = $"update playerinfo set LastDailyBonus={GameController.currentPlayer.lastDailyBonus}," +
                     $"EducationStatus={GameController.currentPlayer.educationStatus}," +
                     $"MoneyBalance={GameController.currentPlayer.moneyBalance}," +
                     $"CrystalsBalance={GameController.currentPlayer.crystalsBalance}," +
                     $"LastDailyBonusTime=\"{GameController.currentPlayer.lastDailyBonusTime.ToString("yyyy-MM-dd HH:mm:ss")}\" " +
                     $"where Id={GameController.currentPlayer.id};";
        
        MySqlCommand command = new MySqlCommand(sql, connection);
        command.ExecuteNonQuery();
    }
}

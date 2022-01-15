using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AuthorizeController : MonoBehaviour
{
    [SerializeField] InputField login, password;
    [SerializeField] Text alertText;

    public void Authorize()
    {
        foreach (var player in DBController.GetAllPlayers())
        {
            if (player.login == login.text && player.password == password.text)
            {
                GameController.currentPlayer = player;
                SceneManager.LoadScene("FarmScene");
                return;
            }
        }

        alertText.gameObject.SetActive(true);
    }
}

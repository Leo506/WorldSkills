using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegistrationController : MonoBehaviour
{
    [SerializeField] InputField fullname, login, password;
    [SerializeField] Text alertText, successText;
    [SerializeField] Canvas registrationCanvas, authorizeCanvas;


    public void RegistratePlayer()
    {
        Debug.Log("Регистрация пользователя");
        if (ValidPlayer())
        {
            DBController.RegisterUser(new Player(fullname.text, login.text, password.text));
            StartCoroutine(SuccessRegistration());
        }

        else
            alertText.gameObject.SetActive(true);
    }


    bool ValidPlayer()
    {
        foreach (var player in DBController.GetAllPlayers())
        {
            if (player.login == login.text)
                return false;
        }

        return true;
    }


    IEnumerator SuccessRegistration()
    {
        alertText.gameObject.SetActive(false);
        successText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        successText.gameObject.SetActive(false);

        registrationCanvas.enabled = false;
        authorizeCanvas.enabled = true;
    }
}

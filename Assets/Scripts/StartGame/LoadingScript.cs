using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScript : MonoBehaviour
{
    [SerializeField] CustomSlider progressSlider;  // Слайдер прогресса загрузки

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("StartScene");  // Ассинхронно загружаем стартовую сцену
        DBController.CreateConnection();                                       // Создаём соединение с базой данных

        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            // Отображаем прогресс загрузки
            progressSlider.SetValue(asyncLoad.progress);

            if (asyncLoad.progress >= 0.9f && !asyncLoad.allowSceneActivation)
            {
                yield return new WaitForSeconds(1);
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}

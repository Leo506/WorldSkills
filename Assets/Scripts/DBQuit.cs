using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBQuit : MonoBehaviour
{
    private void Awake()
    {
        if (FindObjectsOfType<DBQuit>().Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }


    private void OnApplicationQuit()
    {
        Debug.Log("On Application Quit");
        DBController.SaveGame();
        DBController.connection.Close();
        DBController.connection.Dispose();
    }
}

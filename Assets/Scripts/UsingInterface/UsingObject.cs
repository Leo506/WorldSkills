using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingObject : MonoBehaviour
{
    public GameObject usingPanelPrefab;  // Префаб панели использования
    public Canvas usingCanvas;           // Canvas использования
    public int id;                       // id объекта

    public static GameObject currentUsingPanel;

    public bool isAvailable = true;

    public virtual void OnClick()
    {
        Debug.Log("isAvailable && currentUsingPanel == null " + (isAvailable && currentUsingPanel == null));
        if (isAvailable && currentUsingPanel == null)
        {
            Vector2 spawnPos = Camera.main.WorldToScreenPoint(transform.position);
            currentUsingPanel = Instantiate(usingPanelPrefab, usingCanvas.transform);
            currentUsingPanel.transform.position = spawnPos;
        }
    }
}

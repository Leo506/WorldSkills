using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    public void Plant()
    {
        int neededId = FindObjectOfType<UsingPanel>().lastId;
        foreach (var item in FindObjectsOfType<UsingObject>())
        {
            if (item.id == neededId)
            {
                Debug.Log("needed id: " + neededId);
                Bed bed = item.GetComponent<Bed>();
                if (bed != null && bed.Plant())
                {
                    Destroy(this.gameObject);
                    return;
                }
            }
        }
    }
}

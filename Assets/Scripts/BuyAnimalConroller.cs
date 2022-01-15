using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyAnimalConroller : MonoBehaviour
{
    public void BuyAnimal(string animalName)
    {
        Animals animal = animalName == "Cow" ? Animals.Cow : Animals.Pig;

        int neededId = FindObjectOfType<UsingPanel>().lastId;
        foreach (var item in FindObjectsOfType<UsingObject>())
        {
            if (item.id == neededId)
            {
                Debug.Log("needed id: " + neededId);
                Fence fence = item.GetComponent<Fence>();
                if (fence != null && fence.AddAnimal(animal))
                {
                    Destroy(this.gameObject);
                    return;
                }
            }
        }
    }
}

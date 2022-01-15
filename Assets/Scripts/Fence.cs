using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Animals
{
    Cow,
    Pig
}
public class Fence : UsingObject
{
    [SerializeField] GameObject cowPrefab, pigPrefab;
    GameObject currentAnimal;

    public bool AddAnimal(Animals animal)
    {
        if (currentAnimal == null && GameController.currentPlayer.moneyBalance >= 0)
        {
            switch (animal)
            {
                case Animals.Cow:
                    currentAnimal = Instantiate(cowPrefab, transform);
                    break;
                case Animals.Pig:
                    currentAnimal = Instantiate(pigPrefab, transform);
                    break;
                default:
                    break;
            }

            currentAnimal.transform.position = transform.position;

            return true;
        }

        return false;
    }
}

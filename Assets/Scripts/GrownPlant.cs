using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrownPlant : UsingObject
{
    public override void OnClick()
    {
        transform.parent.gameObject.GetComponent<UsingObject>().isAvailable = true;
        Destroy(this.gameObject);
        // TODO добавление собранного урожая в хранилище
    }
}

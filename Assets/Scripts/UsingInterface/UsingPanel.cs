using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UsingPanel : MonoBehaviour
{
    public int lastId { get; private set; }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (UsingObject.currentUsingPanel != null)
                Destroy(UsingObject.currentUsingPanel);

            Vector2 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D result = Physics2D.Raycast(origin, Vector3.forward);

            if (result)
            {
                UsingObject obj = result.collider.GetComponent<UsingObject>();

                if (obj != null)
                {
                    obj.OnClick();
                    lastId = obj.id;
                    Debug.Log("Last id: " + lastId);
                }
            }
                
        }
    }
}

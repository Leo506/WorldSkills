using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudAnimController : MonoBehaviour
{
    [SerializeField] AnimationClip cloudAnimation;
    [SerializeField] Canvas cloudCanvas, mainCanvas;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAnim());
    }

    IEnumerator WaitAnim()
    {
        yield return new WaitForSeconds(cloudAnimation.length);
        cloudCanvas.enabled = false;
        mainCanvas.enabled = true;
    }
}

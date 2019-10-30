using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Back : MonoBehaviour
{
    void Start()
    {
        // Make sure user is on Android platform
        if (Application.platform == RuntimePlatform.Android)
        {
            StartCoroutine(GoBack());
        }
    }

    private IEnumerator GoBack()
    {
        while (true)
        {
            // Check if Back was pressed this frame
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GetComponent<Button>().onClick.Invoke();
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}

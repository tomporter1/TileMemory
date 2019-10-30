using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadManager : MonoBehaviour
{
    private static List<GameObject> objs = new List<GameObject>();
    public static void AddObj(GameObject newObj)
    {
        objs.Add(newObj);
        DontDestroyOnLoad(newObj);
    }

    public static void RemoveObj(GameObject newObj)
    {
        objs.Remove(newObj);
        Destroy(newObj);
    }
}

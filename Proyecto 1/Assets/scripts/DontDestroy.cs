using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {

    DontDestroy[] array;
    // Use this for initialization
    void Start()
    {
        array = FindObjectsOfType<DontDestroy>();
        DontDestroyOnLoad(this.gameObject);
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].gameObject != this.gameObject && array[i].gameObject.name == this.gameObject.name)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
	


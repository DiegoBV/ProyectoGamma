using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MistakesWereMadeVolII : MonoBehaviour {
    Color c;
    Image img;
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            img = this.gameObject.GetComponent<Image>();
            c = img.color;
            c.a = 0;
            img.color = c;

        }
	}
}

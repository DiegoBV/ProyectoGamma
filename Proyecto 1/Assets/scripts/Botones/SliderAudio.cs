using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SliderAudio : MonoBehaviour {


    [HideInInspector] public Slider sld;
    
    void Awake()
    {
        sld = this.gameObject.GetComponent<Slider>();
        print("Sld=" + sld.value);
        this.gameObject.GetComponent<Slider>().value = GameManager.instance.Volume;
    }
    public void SubmitSliderSetting()
    {
        print("Slideando");
        print("Sld=" + sld.value);
        GameManager.instance.Volume = sld.value;
        GameManager.instance.GetComponent<AudioSource>().volume = sld.value;
        print(GameManager.instance.Volume);
    }       
}

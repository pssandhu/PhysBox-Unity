using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinusoidalSpeedComponents : MonoBehaviour {

    float xSpeed = 0.5f;
    float ySpeed = 0.5f;
    
    void FixedUpdate() {
        float xSpeedOld = xSpeed;
        float ySpeedOld = ySpeed;
        xSpeed = GameObject.Find("HSlider").GetComponent<Slider>().value;
        ySpeed = GameObject.Find("VSlider").GetComponent<Slider>().value;
                
        float xPos = 100 * (float)Math.Sin(2 * Math.PI * xSpeed * Time.timeSinceLevelLoad) - 170;
        float yPos = 100 * (float)Math.Cos(2 * Math.PI * ySpeed * Time.timeSinceLevelLoad);
        
        transform.localPosition = new Vector3(xPos, yPos, -1);
        
        // Clear trail after moving object to also clear the jump in the trail
        if (xSpeed != xSpeedOld || ySpeed != ySpeedOld) {
            this.GetComponent<TrailRenderer>().Clear();
        }
    }
}

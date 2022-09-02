using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PhysBox.Constants;

public class SinusoidalSpeedComponents : MonoBehaviour {

    [SerializeField] private Slider HSlider;
    [SerializeField] private Slider VSlider;
    private TrailRenderer trail;

    // Set initial values in inspector
    private float xSpeed;
    private float ySpeed;

    void Start() {
        xSpeed = HSlider.value;
        ySpeed = VSlider.value;
        trail = GetComponent<TrailRenderer>();
    }

    void FixedUpdate() {
        float xSpeedOld = xSpeed;
        float ySpeedOld = ySpeed;
        xSpeed = HSlider.value;
        ySpeed = VSlider.value;

        float xPos = 100 * Mathf.Sin(2 * pi * xSpeed * Time.timeSinceLevelLoad);
        float yPos = 100 * Mathf.Cos(2 * pi * ySpeed * Time.timeSinceLevelLoad);
        transform.localPosition = new Vector3(xPos, yPos, -1);

        // Clear trail after moving object to also clear the jump in the trail
        if (xSpeed != xSpeedOld || ySpeed != ySpeedOld) {
            trail.Clear();
        }
    }
}

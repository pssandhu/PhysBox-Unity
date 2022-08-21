using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PendulumController : MonoBehaviour {
    private bool simulationActive = false;
    private Transform pendulumTransform;

    [SerializeField]
    public Slider InitialPositionSlider; 
    private float initialPosition;

    void Awake() {
        pendulumTransform = gameObject.GetComponent<Transform>();
        SetInitialPosition(InitialPositionSlider.GetComponent<Slider>().value);
    }

    public void StartSimulation() {
        simulationActive = true;
    }

    public void StopSimulation() {
        simulationActive = false;
        pendulumTransform.rotation = Quaternion.Euler(0, 0, initialPosition);
    }

    public void SetInitialPosition(float value) {
        initialPosition = value;
        if (!simulationActive) {
            pendulumTransform.rotation = Quaternion.Euler(0, 0, value);
        }
    }

}

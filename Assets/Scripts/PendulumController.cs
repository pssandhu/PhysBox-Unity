using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PhysBox.Constants;
using static PhysBox.Utils;

public class PendulumController : MonoBehaviour {
    private bool simulationActive = false;

    [Header("Pendulum")]
    [SerializeField] private GameObject Rod;
    [SerializeField] private GameObject Sphere;
    private float sphereRadius;

    [Header("Pendulum Controls")]
    [SerializeField] private Slider InitialPositionSlider;
    [SerializeField] private Slider LengthSlider;
    [SerializeField] private Slider MassSlider;
    [SerializeField] private Slider DampingSlider;
    [SerializeField] private Toggle AlwaysUseSmallAngleToggle;

    [Header("Simulation Controls")]
    [SerializeField] private Button StartButton;
    [SerializeField] private Button StopButton;

    [Header("Stats")]
    [SerializeField] TMP_Text MeasuredPeriodText;

    [Header("Time Settings")]
    [SerializeField] private TimeSettings TimeSettings;
    private double timestep;

    private double theta;
    private double velocity;
    private double acceleration;

    // Frequency in rad/s
    // private float omega;

    private double measuredPeriod;
    private bool stopwatchActive;
    private int initialVelocitySign;

    // Length of rod in metres
    // To change default, min, and max length change the slider properties in the inspector
    private double length;

    // Time as simulation progresses in seconds
    private double simTime;

    // Initial position of pendulum in degrees
    // To change default, min, and max initial position change the slider properties in the inspector
    private float initialPosition;

    // Pendulum mass in kg
    private double mass;
    private double damping;

    void Awake() {
        sphereRadius = Sphere.transform.localScale.x / 2;
        SetInitialPosition(InitialPositionSlider.value);
        SetLength(LengthSlider.value);
        SetMass(MassSlider.value);
        SetDamping(DampingSlider.value);
        TimeSettings.onSettingsChanged.AddListener(UpdateTimeSettings);
        UpdateTimeSettings();

        // Set max damping to less than lowest damping needed for critical damping based on range of parameters
        double dampingMax = 2 * MassSlider.minValue * Math.Sqrt(g / LengthSlider.maxValue);
        DampingSlider.maxValue = (float)dampingMax - 0.01f;
    }

    void FixedUpdate() {
        if (simulationActive) {
            if (TimeSettings.RealtimeToggle.isOn) {
                // Get deltaTime for this update
                timestep = Time.deltaTime;
            }

            if (AlwaysUseSmallAngleToggle.isOn) {
                acceleration = -g / length * theta - damping / mass * velocity;
            } else {
                acceleration = -g / length * Math.Sin(theta) - damping / mass * velocity;
            }

            velocity += acceleration * timestep;
            double deltaTheta = velocity * timestep;
            theta += deltaTheta;
            transform.Rotate(0, 0, (float)ToDeg(deltaTheta));

            if (stopwatchActive) {
                Debug.Log("deltaTime: " + timestep);
                Debug.Log("Velocity: " + velocity);
                measuredPeriod += timestep * 2;

                // Simulation is not accurate enough for velocity to reach exactly zero so check for reversal in velocity direction
                if ((int)(velocity/Math.Abs(velocity)) != initialVelocitySign) {
                    stopwatchActive = false;
                    // Subtract on average half a timestep to account for when the velocity was actually zero
                    measuredPeriod -= 0.5 * timestep;
                    Debug.Log("Measured period: " + measuredPeriod);    
                }

                MeasuredPeriodText.text = "Measured period: " + measuredPeriod.ToString("0.0000") + " s";
            }

            /*
            // Using small angle approximation
            float gamma = damping / (2 * mass);
            omega = Mathf.Sqrt(g/length - Mathf.Pow(gamma, 2));

            float newTheta = ToRad(initialPosition) * Mathf.Exp(-gamma * simTime) *  Mathf.Cos(omega * simTime);
            transform.rotation = Quaternion.Euler(0, 0, ToDeg(newTheta));
            */

            simTime += timestep;
        }
    }

    public void StartSimulation() {
        InitialPositionSlider.interactable = false;
        LengthSlider.interactable = false;
        MassSlider.interactable = false;
        DampingSlider.interactable = false;
        AlwaysUseSmallAngleToggle.interactable = false;
        // Do not allow timestep to be changed during sim as it changes the result
        TimeSettings.RealtimeToggle.interactable = false;
        TimeSettings.RealtimeStepSlider.interactable = false;
        TimeSettings.TimestepSlider.interactable = false;
        StartButton.gameObject.SetActive(false);
        StopButton.gameObject.SetActive(true);

        theta = ToRad(initialPosition);
        velocity = 0;
        simTime = 0;
        measuredPeriod = 0;
        initialVelocitySign = -1 * (int)(initialPosition/Mathf.Abs(initialPosition));
        stopwatchActive = true;
        simulationActive = true;
    }

    public void StopSimulation() {
        simulationActive = false;
        transform.rotation = Quaternion.Euler(0, 0, initialPosition);

        InitialPositionSlider.interactable = true;
        LengthSlider.interactable = true;
        MassSlider.interactable = true;
        DampingSlider.interactable = true;
        AlwaysUseSmallAngleToggle.interactable = true;
        TimeSettings.RealtimeToggle.interactable = true;
        TimeSettings.RealtimeStepSlider.interactable = true;
        TimeSettings.TimestepSlider.interactable = true;
        StopButton.gameObject.SetActive(false);
        StartButton.gameObject.SetActive(true);
    }

    public void SetInitialPosition(float value) {
        initialPosition = value;
        transform.rotation = Quaternion.Euler(0, 0, value);
    }

    public void SetLength(float value) {
        length = value;

        Vector3 temp = Rod.transform.localScale;
        temp.y = value / 2;
        Rod.transform.localScale = temp;

        temp = Rod.transform.localPosition;
        temp.y = -1 * value / 2;
        Rod.transform.localPosition = temp;

        temp = Sphere.transform.localPosition;
        temp.y = -1 * (value + sphereRadius);
        Sphere.transform.localPosition = temp;
    }

    public void SetMass(float value) {
        mass = value;
    }

    public void SetDamping(float value) {
        damping = value;
    }

    public void UpdateTimeSettings() {
        if (TimeSettings.RealtimeToggle.isOn) {
            timestep = TimeSettings.RealtimeStepSlider.value;
            Time.fixedDeltaTime = TimeSettings.RealtimeStepSlider.value;
        } else {
            timestep = TimeSettings.TimestepSlider.value;
            Time.fixedDeltaTime = 1 / TimeSettings.StepFrequencySlider.value;
        }
    }

}

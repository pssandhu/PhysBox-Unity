using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Time Controls")]
    [SerializeField] private Toggle RealtimeToggle;
    [SerializeField] private Slider RealtimeStepSlider;
    [SerializeField] private Slider StepFrequencySlider;
    [SerializeField] private Slider TimestepSlider;
    private float timestep;

    private float theta;
    private float velocity;
    private float acceleration;

    // Frequency in rad/s
    // private float omega;

    private float measuredPeriod;
    private bool stopwatchActive;
    private int initialVelocitySign;

    // Length of rod in metres
    // To change default, min, and max length change the slider properties in the inspector
    private float length;

    // Time as simulation progresses in seconds
    private float simTime;

    // Initial position of pendulum in degrees
    // To change default, min, and max initial position change the slider properties in the inspector
    private float initialPosition;
    private const float g = 9.81f;

    // Pendulum mass in kg
    private float mass;
    private float damping;

    void Awake() {
        sphereRadius = Sphere.transform.localScale.x / 2;
        SetInitialPosition(InitialPositionSlider.value);
        SetLength(LengthSlider.value);
        SetMass(MassSlider.value);
        SetDamping(DampingSlider.value);
        UpdateTimeSettings();

        // Set max damping to less than lowest damping needed for critical damping based on range of parameters
        float dampingMax = 2 * MassSlider.minValue * Mathf.Sqrt(g / (LengthSlider.maxValue/100));
        DampingSlider.maxValue = dampingMax - 0.01f;
    }

    void FixedUpdate() {
        if (simulationActive) {
            if (RealtimeToggle.isOn) {
                // Get deltaTime for this update
                timestep = Time.deltaTime;
            }

            if (AlwaysUseSmallAngleToggle.isOn) {
                acceleration = -g / length * theta - damping / mass * velocity;
            } else {
                acceleration = -g / length * Mathf.Sin(theta) - damping / mass * velocity;
            }

            velocity += acceleration * timestep;
            float deltaTheta = velocity * timestep;
            theta += deltaTheta;
            transform.Rotate(0, 0, ToDeg(deltaTheta));

            if (stopwatchActive) {
                Debug.Log("deltaTime: " + timestep);
                Debug.Log("Velocity: " + velocity);
                measuredPeriod += timestep * 2;

                // Simulation is not accurate enough for velocity to reach exactly zero so check for reversal in velocity direction
                if ((int)(velocity/Mathf.Abs(velocity)) != initialVelocitySign) {
                    stopwatchActive = false;
                    // Subtract on average half a timestep to account for when the velocity was actually zero
                    // Then double for the whole period, so subtract a whole timestep
                    measuredPeriod -= timestep;
                    Debug.Log("Measured period: " + measuredPeriod);    
                }

                MeasuredPeriodText.text = "Measured period: " + measuredPeriod.ToString("0.000") + " s";
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
        RealtimeToggle.interactable = false;
        RealtimeStepSlider.interactable = false;
        TimestepSlider.interactable = false;
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
        RealtimeToggle.interactable = true;
        RealtimeStepSlider.interactable = true;
        TimestepSlider.interactable = true;
        StopButton.gameObject.SetActive(false);
        StartButton.gameObject.SetActive(true);
    }

    public void SetInitialPosition(float value) {
        initialPosition = value;
        transform.rotation = Quaternion.Euler(0, 0, value);
    }

    public void SetLength(float value) {
        Vector3 temp = Rod.transform.localScale;
        temp.y = value / 2;
        Rod.transform.localScale = temp;

        temp = Rod.transform.localPosition;
        temp.y = -value / 2;
        Rod.transform.localPosition = temp;

        temp = Sphere.transform.localPosition;
        temp.y = -1 * (value + sphereRadius);
        Sphere.transform.localPosition = temp;

        length = value / 100;
    }

    public void SetMass(float value) {
        mass = value;
    }

    public void SetDamping(float value) {
        damping = value;
    }

    public void UpdateTimeSettings() {
        if (RealtimeToggle.isOn) {
            timestep = RealtimeStepSlider.value;
            Time.fixedDeltaTime = RealtimeStepSlider.value;
        } else {
            timestep = TimestepSlider.value;
            Time.fixedDeltaTime = 1 / StepFrequencySlider.value;
        }
    }

    private float ToDeg(float value) {
        return 180 * value / Mathf.PI;
    }

    private float ToRad(float value) {
        return Mathf.PI * value / 180;
    }
}

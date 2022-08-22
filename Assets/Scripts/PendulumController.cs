using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PendulumController : MonoBehaviour {
    private bool simulationActive = false;

    [SerializeField]
    private GameObject Rod;

    [SerializeField]
    private GameObject Sphere;
    private float sphereRadius;

    [SerializeField]
    private Slider InitialPositionSlider;

    [SerializeField]
    private Slider LengthSlider;

    [SerializeField]
    private Slider DampingSlider;

    [SerializeField]
    private Button StartButton;

    [SerializeField]
    private Button StopButton;

    // private float theta;

    // Frequency in rad/s
    private float omega;

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
    private float mass = 1;
    private float damping;

    void Awake() {
        sphereRadius = Sphere.transform.localScale.x / 2;

        // Set max damping to less than lowest damping needed for critical damping based on range of parameters
        float dampingMax = 2 * mass * Mathf.Sqrt(g / (LengthSlider.maxValue/100));
        DampingSlider.maxValue = dampingMax - 0.01f;

        SetLength(LengthSlider.value);
        SetInitialPosition(InitialPositionSlider.value);
        SetDamping(DampingSlider.value);
    }

    void FixedUpdate() {
        if (simulationActive) {
            // float dTheta = -g / length * simTime * Mathf.Sin(theta)  * Time.deltaTime;
            // transform.Rotate(0, 0, ToDeg(dTheta));
            // theta += dTheta;

            // Using small angle approximation
            float gamma = damping / (2 * mass);
            omega = Mathf.Sqrt(g/length - Mathf.Pow(gamma, 2));

            float newTheta = ToRad(initialPosition) * Mathf.Exp(-gamma * simTime) *  Mathf.Cos(omega * simTime);
            transform.rotation = Quaternion.Euler(0, 0, ToDeg(newTheta));

            simTime += Time.deltaTime;
        }
    }

    public void StartSimulation() {
        InitialPositionSlider.interactable = false;
        LengthSlider.interactable = false;
        DampingSlider.interactable = false;
        StartButton.gameObject.SetActive(false);
        StopButton.gameObject.SetActive(true);

        // theta = ToRad(initialPosition);
        simTime = 0;
        simulationActive = true;
    }

    public void StopSimulation() {
        simulationActive = false;
        transform.rotation = Quaternion.Euler(0, 0, initialPosition);

        InitialPositionSlider.interactable = true;
        LengthSlider.interactable = true;
        DampingSlider.interactable = true;
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

    public void SetDamping(float value) {
        damping = value;
    }

    private float ToDeg(float value) {
        return 180 * value / Mathf.PI;
    }

    private float ToRad(float value) {
        return Mathf.PI * value / 180;
    }
}

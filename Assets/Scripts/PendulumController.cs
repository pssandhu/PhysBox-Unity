using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PendulumController : MonoBehaviour {
    private bool simulationActive = false;
    private Transform pendulumTransform;

    [SerializeField]
    public GameObject Rod;

    [SerializeField]
    public GameObject Sphere;
    private float sphereRadius;

    [SerializeField]
    public Slider InitialPositionSlider;

    [SerializeField]
    public Slider LengthSlider;

    [SerializeField]
    public Slider DampingSlider;

    [SerializeField]
    public Button StartButton;

    [SerializeField]
    public Button StopButton;

    // private float theta;

    // Natrual frequency in rad/s
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
        pendulumTransform = gameObject.GetComponent<Transform>();
        sphereRadius = Sphere.GetComponent<Transform>().localScale.x;

        // Set max damping to less than lowest damping needed for critical damping based on range of parameters
        float dampingMax = 2 * mass * Mathf.Sqrt(g / (LengthSlider.GetComponent<Slider>().maxValue/100));
        DampingSlider.GetComponent<Slider>().maxValue = dampingMax - 0.01f;

        SetLength(LengthSlider.GetComponent<Slider>().value);
        SetInitialPosition(InitialPositionSlider.GetComponent<Slider>().value);
        SetDamping(DampingSlider.GetComponent<Slider>().value);
    }

    void FixedUpdate() {
        if (simulationActive) {
            // float dTheta = -g / length * simTime * Mathf.Sin(theta)  * Time.deltaTime;
            // pendulumTransform.Rotate(0, 0, ToDeg(dTheta));
            // theta += dTheta;

            // Using small angle approximation
            float gamma = damping / (2 * mass);
            omega = Mathf.Sqrt(g/length - Mathf.Pow(gamma, 2));

            float newTheta = ToRad(initialPosition) * Mathf.Exp(-gamma * simTime) *  Mathf.Cos(omega * simTime);
            pendulumTransform.rotation = Quaternion.Euler(0, 0, ToDeg(newTheta));

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
        pendulumTransform.rotation = Quaternion.Euler(0, 0, initialPosition);

        InitialPositionSlider.interactable = true;
        LengthSlider.interactable = true;
        DampingSlider.interactable = true;
        StopButton.gameObject.SetActive(false);
        StartButton.gameObject.SetActive(true);
    }

    public void SetInitialPosition(float value) {
        initialPosition = value;
        if (!simulationActive) {
            pendulumTransform.rotation = Quaternion.Euler(0, 0, value);
        }
    }

    public void SetLength(float value) {
        Rod.GetComponent<Transform>().localScale = new Vector3 (2, value / 2, 1);
        Rod.GetComponent<Transform>().localPosition = new Vector3 (0, -value / 2, 0);
        Sphere.GetComponent<Transform>().localPosition = new Vector3(0, -1 * (value + sphereRadius / 2), 0);
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

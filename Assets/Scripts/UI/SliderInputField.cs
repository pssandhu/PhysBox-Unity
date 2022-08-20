using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderInputField : MonoBehaviour {

    [SerializeField]
    public GameObject Slider;

    private float minValue;
    private float maxValue;
    private float oldValue;

    [SerializeField]
    public bool UpdateSliderWithoutNotify = false;

    void Start() {
        minValue = Slider.GetComponent<Slider>().minValue;
        maxValue = Slider.GetComponent<Slider>().maxValue;
        UpdateFromSlider(Slider.GetComponent<Slider>().value);

        gameObject.GetComponent<TMP_InputField>().onValueChanged.AddListener(ValidateInput);
        Slider.GetComponent<Slider>().onValueChanged.AddListener(UpdateFromSlider);
    }

    private void ValidateInput(string newInput) {
        if (newInput != "" && newInput != "-") {
            bool negativeInput = false;

            if (newInput.StartsWith("-")) {
                negativeInput = true;
                newInput = newInput.Remove(0, 1);
            }

            float newValue = float.Parse(newInput);

            if (negativeInput) {
                newValue *= -1;
            }

            if (newValue < minValue || newValue > maxValue) {
                Debug.Log("Invalid slider field input");
                gameObject.GetComponent<TMP_InputField>().text = oldValue.ToString();
            } else {
                if (UpdateSliderWithoutNotify) {
                    Slider.GetComponent<Slider>().SetValueWithoutNotify(newValue);
                } else {
                    Slider.GetComponent<Slider>().value = newValue;
                }

                oldValue = newValue;
            }
        }
    }

    private void UpdateFromSlider(float newValue) {
        oldValue = newValue;
        gameObject.GetComponent<TMP_InputField>().text = newValue.ToString();
    }
}

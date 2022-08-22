using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderInputField : MonoBehaviour {

    [SerializeField] private Slider TargetSlider;

    private float minValue;
    private float maxValue;
    private float oldValue;
    [SerializeField] private bool UpdateSliderWithoutNotify = false;

    void Start() {
        minValue = TargetSlider.minValue;
        maxValue = TargetSlider.maxValue;
        UpdateFromSlider(TargetSlider.value);

        GetComponent<TMP_InputField>().onValueChanged.AddListener(ValidateInput);
        TargetSlider.onValueChanged.AddListener(UpdateFromSlider);
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
                GetComponent<TMP_InputField>().text = oldValue.ToString();
            } else {
                if (UpdateSliderWithoutNotify) {
                    TargetSlider.SetValueWithoutNotify(newValue);
                } else {
                    TargetSlider.value = newValue;
                }

                oldValue = newValue;
            }
        }
    }

    private void UpdateFromSlider(float newValue) {
        oldValue = newValue;
        GetComponent<TMP_InputField>().text = newValue.ToString();
    }
}

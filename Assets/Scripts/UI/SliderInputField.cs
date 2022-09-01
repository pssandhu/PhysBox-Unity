using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderInputField : MonoBehaviour {

    private TMP_InputField inputField;
    [SerializeField] private Slider TargetSlider;

    private float minValue;
    private float maxValue;
    private float oldValue;
    [SerializeField] private bool UpdateSliderWithoutNotify = false;
    private bool sliderIsStepped = false;

    void Start() {
        inputField = GetComponent<TMP_InputField>();
        inputField.onEndEdit.AddListener(ValidateInput);
        minValue = TargetSlider.minValue;
        maxValue = TargetSlider.maxValue;

        if (TargetSlider.TryGetComponent(out SliderStep targetSliderStepControl)) {
            targetSliderStepControl.onValueValidated.AddListener(UpdateValueFromSlider);
            sliderIsStepped = true;
        } else {
            TargetSlider.onValueChanged.AddListener(UpdateValueFromSlider);
        }

        UpdateValueFromSlider(TargetSlider.value);
    }

    void Update() {
        inputField.interactable = TargetSlider.interactable;
    }

    private void ValidateInput(string newInput) {
        if (newInput != "" && newInput != "-" && newInput != ".") {

            // Workaround for float.Parse not handling strings that start with "-"
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
                RevertInputChange();
            } else {
                if (UpdateSliderWithoutNotify) {
                    if (sliderIsStepped) {
                        Debug.LogWarning("Setting value of stepped slider without notify. Input validatation may not work as expected.");
                    }
                    TargetSlider.SetValueWithoutNotify(newValue);
                } else {
                    // No need to validate input matches slider step as the slider will do that when its
                    // value is changed and round if needed. That will in turn cause UpdateValueFromSlider
                    // to be called and change the inputField value again
                    TargetSlider.value = newValue;
                }

                oldValue = newValue;
            }
        } else {
            RevertInputChange();
        }
    }

    private void RevertInputChange() {
        Debug.Log("Invalid slider field input");
        inputField.text = oldValue.ToString();
    }

    private void UpdateValueFromSlider(float newValue) {
        oldValue = newValue;
        inputField.text = newValue.ToString();
    }
}

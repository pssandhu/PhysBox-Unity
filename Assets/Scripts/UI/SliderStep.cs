using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderStep : MonoBehaviour {

    [SerializeField] private float Step;
    private Slider slider;

    void Start() {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(ValidateValue);
    }

    private void ValidateValue(float value) {
        float steppedValue = Mathf.Round(value / Step) * Step;
        if (steppedValue != value) {
            // Workaround for needing this function to be called before other slider.onValueChanged
            // listeners and for calling all listeners apart from this one
            slider.onValueChanged.RemoveListener(ValidateValue);
            slider.value = steppedValue;
            slider.onValueChanged.AddListener(ValidateValue);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Slider))]
public class SliderStep : MonoBehaviour {

    [SerializeField] private float Step = 0.5f;
    private Slider slider;
    public UnityEvent<float> onValueValidated;

    void Awake() {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(ValidateValue);
        // TODO: Fix assert
        // Debug.Assert(Mathf.Abs(slider.value % Step) < 0.0000001, "Initial " + slider.name + " slider value should be a multiple of " + Step);
    }

    private void ValidateValue(float value) {
        float steppedValue = Mathf.Round(value / Step) * Step;
        if (steppedValue != value) {
            slider.SetValueWithoutNotify(steppedValue);
        }
        onValueValidated.Invoke(slider.value);
    }
}

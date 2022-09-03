using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TimeSettings : MonoBehaviour {

    public Toggle RealtimeToggle;
    [SerializeField] private TMP_Text RealtimeStepText;
    public Slider RealtimeStepSlider;
    [SerializeField] private TMP_Text StepFrequencyText;
    public Slider StepFrequencySlider;
    [SerializeField] private TMP_Text TimestepText;
    public Slider TimestepSlider;

    public UnityEvent onSettingsChanged;

    void Start() {
        RealtimeToggle.onValueChanged.AddListener((x) => onSettingsChanged.Invoke());
        RealtimeStepSlider.onValueChanged.AddListener((x) => onSettingsChanged.Invoke());
        StepFrequencySlider.onValueChanged.AddListener((x) => onSettingsChanged.Invoke());
        TimestepSlider.onValueChanged.AddListener((x) => onSettingsChanged.Invoke());

        UpdateTimeControls();
    }

    public void UpdateTimeControls() {
        if (RealtimeToggle.isOn) {
            RealtimeStepText.gameObject.SetActive(true);
            RealtimeStepSlider.gameObject.SetActive(true);

            StepFrequencyText.gameObject.SetActive(false);
            StepFrequencySlider.gameObject.SetActive(false);
            TimestepText.gameObject.SetActive(false);
            TimestepSlider.gameObject.SetActive(false);
        } else {
            RealtimeStepText.gameObject.SetActive(false);
            RealtimeStepSlider.gameObject.SetActive(false);

            StepFrequencyText.gameObject.SetActive(true);
            StepFrequencySlider.gameObject.SetActive(true);
            TimestepText.gameObject.SetActive(true);
            TimestepSlider.gameObject.SetActive(true);
        }
    }
}

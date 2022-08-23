using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeSettings : MonoBehaviour {

    [SerializeField] private Toggle RealtimeToggle;
    [SerializeField] private TMP_Text RealtimeStepText;
    [SerializeField] private Slider RealtimeStepSlider;
    [SerializeField] private TMP_Text StepFrequencyText;
    [SerializeField] private Slider StepFrequencySlider;
    [SerializeField] private TMP_Text TimestepText;
    [SerializeField] private Slider TimestepSlider;

    void Start() {
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

using System;
using System.Collections;
using System.Collections.Generic;
using EnglishKids.Robots;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : Singleton<OptionsController> {
    [SerializeField] private Slider _effectsSlider;
    [SerializeField] private Slider _voiceSlider;
    [SerializeField] private Slider _musicSlider;

    public Action<float> effectSliderAction;
    public Action<float> voiceSliderAction;
    public Action<float> musicSliderAction;


    void Start() {
        _effectsSlider.onValueChanged.RemoveAllListeners();
        _effectsSlider.onValueChanged.AddListener((arg0 => effectSliderAction?.Invoke(arg0)));

        _voiceSlider.onValueChanged.RemoveAllListeners();
        _voiceSlider.onValueChanged.AddListener((arg0 => voiceSliderAction?.Invoke(arg0)));

        _musicSlider.onValueChanged.RemoveAllListeners();
        _musicSlider.onValueChanged.AddListener((arg0 => musicSliderAction?.Invoke(arg0)));
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class VoiceButton : MonoBehaviour {

    [SerializeField] private Button _button;
    [SerializeField] private Text _buttonText;
    private RoboColors color;
    public void Setup(RoboColors col) {
        color = col;
        SetVoiceButtonText(color);
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(PlayColorWord);
    }

    private void SetVoiceButtonText(RoboColors col) {
        _buttonText.text = col.ToString();
    }

    private void PlayColorWord() {
        Debug.Log("Play color sound: " + color);

    }
}

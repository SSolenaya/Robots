using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public enum RoboColors {
    green,
    yellow
}

public enum Side
{
    left,
    right
}

public class RobotSpace : MonoBehaviour {

    private Side _side;
    [SerializeField] private Image _background;
    [SerializeField] private RobotShadow _roboContour;
    [SerializeField] private VoiceButton _voiceButton;

    public void Setup(Side side, RoboColors color) {
        _side = side;
        var bgPath = color.ToString() + "/" + "background_" + _side.ToString();
        SetSpriteTo(_background, bgPath);
        SetRobotShadowByColor(color);
        _voiceButton.Setup(color);
    }

    private void SetRobotShadowByColor(RoboColors color) {
        var path = "roboContour_" + color.ToString();
        var cont = Resources.Load<RobotShadow>("Prefabs/" + path);
        _roboContour = Instantiate(cont, this.transform);
        _roboContour.transform.localPosition = Vector3.zero;
        _roboContour.transform.localScale = Vector3.one;
    }

    private void ShowVoiceButton(bool var) {
        _voiceButton.gameObject.SetActive(true);
    }
    private void SetSpriteTo(Image img, string spriteName)
    {
        var spr = Resources.Load<Sprite>("Sprites/" + spriteName);
        try
        {
            img.sprite = spr;
        }
        catch
        {
            Debug.Log("Error in sprite attaching to robot space: " + spriteName);
        }
    }


}

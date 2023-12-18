using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTest : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider _slider = null;
    public Camera _camera = null;
    private float _cameraDeaultSize = 0.0F;
    void Start()
    {
        _slider.onValueChanged.AddListener(OnSliderValueChanged);
        _cameraDeaultSize = _camera.orthographicSize;
    }

    private void OnSliderValueChanged(float value) {
        if (_camera != null) {
            _camera.orthographicSize = _cameraDeaultSize + value;
        }
    }
}

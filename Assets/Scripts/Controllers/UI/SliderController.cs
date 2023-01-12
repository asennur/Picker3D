using Extensions;
using Signals;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider _sliderValue;
     private void Awake()
    {
        _sliderValue = transform.GetChild(0).GetComponent<Slider>();
    }
    private void Update()
    {
       _sliderValue.value = (float) UISignals.Instance.onGetScore?.Invoke();
    }
}

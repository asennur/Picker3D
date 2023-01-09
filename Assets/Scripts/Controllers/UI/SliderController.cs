using Extensions;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoSingleton<SliderController>
{
    public float totalBall;
    public Slider _sliderValue;
    public float finalScore;
     private void Awake()
    {
        _sliderValue = transform.GetChild(0).GetComponent<Slider>();
    }

    private void Start()
    {
        totalBall = GameObject.FindGameObjectsWithTag("Collectable").Length;
    }

    private void Update()
    {
        finalScore = _sliderValue.value;
    }

    public void UpdateSlider(float collectedBall)
    {
        _sliderValue.value = collectedBall / totalBall;
    }
}

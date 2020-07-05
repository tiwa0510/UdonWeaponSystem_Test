
using UdonSharp;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class SetAttackSampleFreamsSlider_Debug : UdonSharpBehaviour
{
    public Slider slider;
    public Text text;

    public SwingEstimator swingEstimator;

    private void Start()
    {
        slider.value = swingEstimator.attackSampleFreams;
    }
    private void Update()
    {
        swingEstimator.attackSampleFreams = (int)slider.value;
        text.text = swingEstimator.attackSampleFreams.ToString();
    }
}

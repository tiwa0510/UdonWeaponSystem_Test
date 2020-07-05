
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class SpeedText_Debug : UdonSharpBehaviour
{
    public bool isTip;
    Text text;
    public VelocityEstimator velocityEstimator;

    void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        if (Time.time > 1)
        {
            if (isTip)
            {
                text.text = ((float)velocityEstimator.GetProgramVariable("velocityScalarTip")).ToString();
            }
            else
            {
                text.text = ((float)velocityEstimator.GetProgramVariable("velocityScalarHandle")).ToString();
            }
        }
    }
}

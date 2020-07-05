
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class AccelText_Debug : UdonSharpBehaviour
{
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
            text.text = ((float)velocityEstimator.GetProgramVariable("accelerationScalar")).ToString();
        }
    }
}

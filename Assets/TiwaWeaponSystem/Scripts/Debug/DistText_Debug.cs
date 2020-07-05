
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class DistText_Debug : UdonSharpBehaviour
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
                text.text = ((float)velocityEstimator.GetProgramVariable("movingDistTip")).ToString();
            }
            else
            {
                text.text = ((float)velocityEstimator.GetProgramVariable("movingDistHandle")).ToString();
            }
        }
    }
}

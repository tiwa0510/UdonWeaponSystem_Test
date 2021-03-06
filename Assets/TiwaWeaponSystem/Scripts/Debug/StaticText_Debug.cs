
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class StaticText_Debug : UdonSharpBehaviour
{
    public bool isTip;
    Text text;
    public SwingEstimator swingEstimator;

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
                text.text = ((Vector3)swingEstimator.GetProgramVariable("startPositionTip")).ToString();
            }
            else
            {
                text.text = ((Vector3)swingEstimator.GetProgramVariable("startPositionHandle")).ToString();
            }
        }
    }
}

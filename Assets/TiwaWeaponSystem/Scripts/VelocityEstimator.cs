
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class VelocityEstimator : UdonSharpBehaviour
{
    public GameObject objTip;
    public GameObject objHandle;

    public int velocitySampleFreams = 5;
    public int attackSampleFreams = 5;

    public float attackSpeedTip = 20f;
    public float staticSpeedTip = 10f;
    public float effectiveDistTip = 5f;

    public float attackSpeedHandle = 20f;
    public float staticSpeedHandle = 10f;
    public float effectiveDistHandle = 5f;

    public GameObject Attacker;

    int i;
    int sampleCount;
    int attackSampleCount;

    // Tip
    Vector3 prevPositionTip;
    Vector3 startPositionTip;
    Vector3[] velocitySamplesTip;
    Vector3 velocityTip;
    float velocityScalarTip;
    float movingDistTip;

    // Handle
    Vector3 prevPositionHandle;
    Vector3 startPositionHandle;
    Vector3[] velocitySamplesHandle;
    Vector3 velocityHandle;
    float velocityScalarHandle;
    float movingDistHandle;

    // Handle

    private void Start()
    {
        velocitySamplesTip = new Vector3[velocitySampleFreams];
        velocitySamplesHandle = new Vector3[velocitySampleFreams];
        prevPositionTip = objTip.transform.position;
        prevPositionHandle = objHandle.transform.position;
    }

    // Ql: https://github.com/wacki/Unity-VRInputModule/blob/master/Assets/SteamVR/InteractionSystem/Core/Scripts/VelocityEstimator.cs
    private void Update()
    {
        sampleCount++;

        // Tip
        // ฌxๆพ
        velocitySamplesTip[sampleCount % velocitySamplesTip.Length] = (1.0f / Time.deltaTime) * (objTip.transform.position - prevPositionTip);
        prevPositionTip = objTip.transform.position;

        velocityTip = Vector3.zero;
        for (i = 0; i < velocitySampleFreams; i++)
        {
            velocityTip += velocitySamplesTip[i];
        }
        velocityTip *= (1.0f / velocitySampleFreams);
        velocityScalarTip = velocityTip.sqrMagnitude;

        // ร~๓ิฬสu๐ๆพ
        if (velocityScalarTip < staticSpeedTip)
        {
            startPositionTip = objTip.transform.position;
        }

        // ฺฎฃ
        movingDistTip = (objTip.transform.position - startPositionTip).sqrMagnitude;

        // Handle
        // ฌxๆพ
        velocitySamplesHandle[sampleCount % velocitySamplesHandle.Length] = (1.0f / Time.deltaTime) * (objHandle.transform.position - prevPositionHandle);
        prevPositionHandle = objHandle.transform.position;

        velocityHandle = Vector3.zero;
        for (i = 0; i < velocitySampleFreams; i++)
        {
            velocityHandle += velocitySamplesHandle[i];
        }
        velocityHandle *= (1.0f / velocitySampleFreams);
        velocityScalarHandle = velocityHandle.sqrMagnitude;

        // ร~๓ิฬสu๐ๆพ
        if (velocityScalarHandle < staticSpeedHandle)
        {
            startPositionHandle = objHandle.transform.position;
        }

        // ฺฎฃ
        movingDistHandle = (objHandle.transform.position - startPositionHandle).sqrMagnitude;

        // ฌxฦมฌxฦร~๓ิฉ็ฬฺฎฃลป่
        if (velocityScalarTip >= attackSpeedTip &&
            velocityScalarHandle >= attackSpeedHandle &&
            effectiveDistTip <= movingDistTip &&
            effectiveDistHandle <= movingDistHandle)
        {
            attackSampleCount = 0;
            Attacker.SetActive(true);
        }
        else
        {
            // Uป่ษAฑลattackSampleFreams๑ธsท้ฦUIน
            if (attackSampleCount > attackSampleFreams)
            {
                attackSampleCount = 0;
                Attacker.SetActive(false);
            }
            else
            {
                attackSampleCount++;
            }
        }
    }
}

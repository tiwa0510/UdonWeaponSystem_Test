
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

    // éQçl: https://github.com/wacki/Unity-VRInputModule/blob/master/Assets/SteamVR/InteractionSystem/Core/Scripts/VelocityEstimator.cs
    private void Update()
    {
        sampleCount++;

        // Tip
        // ë¨ìxéÊìæ
        velocitySamplesTip[sampleCount % velocitySamplesTip.Length] = (1.0f / Time.deltaTime) * (objTip.transform.position - prevPositionTip);
        prevPositionTip = objTip.transform.position;

        velocityTip = Vector3.zero;
        for (i = 0; i < velocitySampleFreams; i++)
        {
            velocityTip += velocitySamplesTip[i];
        }
        velocityTip *= (1.0f / velocitySampleFreams);
        velocityScalarTip = velocityTip.sqrMagnitude;

        // ê√é~èÛë‘ÇÃà íuÇéÊìæ
        if (velocityScalarTip < staticSpeedTip)
        {
            startPositionTip = objTip.transform.position;
        }

        // à⁄ìÆãóó£
        movingDistTip = (objTip.transform.position - startPositionTip).sqrMagnitude;

        // Handle
        // ë¨ìxéÊìæ
        velocitySamplesHandle[sampleCount % velocitySamplesHandle.Length] = (1.0f / Time.deltaTime) * (objHandle.transform.position - prevPositionHandle);
        prevPositionHandle = objHandle.transform.position;

        velocityHandle = Vector3.zero;
        for (i = 0; i < velocitySampleFreams; i++)
        {
            velocityHandle += velocitySamplesHandle[i];
        }
        velocityHandle *= (1.0f / velocitySampleFreams);
        velocityScalarHandle = velocityHandle.sqrMagnitude;

        // ê√é~èÛë‘ÇÃà íuÇéÊìæ
        if (velocityScalarHandle < staticSpeedHandle)
        {
            startPositionHandle = objHandle.transform.position;
        }

        // à⁄ìÆãóó£
        movingDistHandle = (objHandle.transform.position - startPositionHandle).sqrMagnitude;

        // ë¨ìxÇ∆â¡ë¨ìxÇ∆ê√é~èÛë‘Ç©ÇÁÇÃà⁄ìÆãóó£Ç≈îªíË
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
            // çUåÇîªíËÇ…òAë±Ç≈attackSampleFreamsâÒé∏îsÇ∑ÇÈÇ∆çUåÇèIóπ
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


using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SwingEstimator : UdonSharpBehaviour
{
    // ����_�̃I�u�W�F�N�g
    public GameObject objTip;
    public GameObject objHandle;

    const int velocitySampleFreams = 5; // �T���v������5�ŌŒ�
    const float velocityFactor = 0.2f; // �T���v�����͌Œ�Ȃ̂őO�����Čv�Z
    public int attackSampleFreams = 5;

    // MEMO: ����m�F�������ʗǂ������������l���f�t�H���g�ɂ��Ă����B����_�I�u�W�F�N�g�̈ʒu�����l
    public float attackSpeedTip = 50f;
    public float staticSpeedTip = 50f;
    public float effectiveDistTip = 1.5f;

    public float attackSpeedHandle = 150f;
    public float staticSpeedHandle = 50f;
    public float effectiveDistHandle = 0.55f;

    public GameObject Attacker;

    // ���[�J���ϐ����ł��邾���g��Ȃ�
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

    private void Start()
    {
        velocitySamplesTip = new Vector3[velocitySampleFreams];
        velocitySamplesHandle = new Vector3[velocitySampleFreams];
        prevPositionTip = objTip.transform.position;
        prevPositionHandle = objHandle.transform.position;
    }

    // �Q�l: https://github.com/wacki/Unity-VRInputModule/blob/master/Assets/SteamVR/InteractionSystem/Core/Scripts/VelocityEstimator.cs
    // �ł��邾���֐��Ăяo�������Ȃ�
    private void Update()
    {
        sampleCount++;

        // Tip
        // ���x�v�Z
        velocitySamplesTip[sampleCount % velocitySamplesTip.Length] = (1.0f / Time.deltaTime) * (objTip.transform.position - prevPositionTip);
        prevPositionTip = objTip.transform.position;

        velocityTip = Vector3.zero;
        for (i = 0; i < velocitySampleFreams; i++)
        {
            velocityTip += velocitySamplesTip[i];
        }
        velocityTip *= velocityFactor;
        velocityScalarTip = velocityTip.sqrMagnitude;

        // �Î~��Ԃ̈ʒu���擾
        if (velocityScalarTip < staticSpeedTip)
        {
            startPositionTip = objTip.transform.position;
        }

        // �X�C���O����
        movingDistTip = (objTip.transform.position - startPositionTip).sqrMagnitude;

        // Handle
        // ���x�v�Z
        velocitySamplesHandle[sampleCount % velocitySamplesHandle.Length] = (1.0f / Time.deltaTime) * (objHandle.transform.position - prevPositionHandle);
        prevPositionHandle = objHandle.transform.position;

        velocityHandle = Vector3.zero;
        for (i = 0; i < velocitySampleFreams; i++)
        {
            velocityHandle += velocitySamplesHandle[i];
        }
        velocityHandle *= velocityFactor;
        velocityScalarHandle = velocityHandle.sqrMagnitude;

        // �Î~��Ԃ̈ʒu���擾
        if (velocityScalarHandle < staticSpeedHandle)
        {
            startPositionHandle = objHandle.transform.position;
        }

        // �X�C���O����
        movingDistHandle = (objHandle.transform.position - startPositionHandle).sqrMagnitude;

        // ���x�Ɖ����x�ƐÎ~��Ԃ���̈ړ������Ŕ���
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
            // �U������ɘA����attackSampleFreams�񎸔s����ƍU���I��
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

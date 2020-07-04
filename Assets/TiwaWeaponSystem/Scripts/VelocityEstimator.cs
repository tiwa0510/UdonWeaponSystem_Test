
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class VelocityEstimator : UdonSharpBehaviour
{
    public int velocitySampleFreams = 5;
    public int attackSampleFreams = 5;
    public float attackSpeed = 20f;
    public float staticSpeed = 5f;
    public float effectiveDist = 1f;
    public GameObject Attacker;

    // �ł��邾�����[�J���ϐ����g��Ȃ�
    Vector3 prevPosition;
    Vector3 startPosition;
    int sampleCount;
    int attackSampleCount;
    int i;
    Vector3[] velocitySamples;
    Vector3 velocity;
    float velocityScalar;

    private void Start()
    {
        velocitySamples = new Vector3[velocitySampleFreams];
        prevPosition = transform.position;
    }

    // �Q�l: https://github.com/wacki/Unity-VRInputModule/blob/master/Assets/SteamVR/InteractionSystem/Core/Scripts/VelocityEstimator.cs
    private void Update()
    {
        // ���x�擾
        sampleCount++;
        velocitySamples[sampleCount % velocitySamples.Length] = (1.0f / Time.deltaTime) * (transform.position - prevPosition);
        prevPosition = transform.position;

        velocity = Vector3.zero;
        for (i = 0; i < velocitySampleFreams; i++)
        {
            velocity += velocitySamples[i];
        }
        velocity *= (1.0f / velocitySampleFreams);
        velocityScalar = velocity.sqrMagnitude;

        // �Î~��Ԃ̈ʒu���擾
        if(velocityScalar < staticSpeed)
        {
            startPosition = transform.position;
        }

        // ���x�ƐÎ~��Ԃ���̈ړ������Ŕ���
        if (velocityScalar >= attackSpeed &&
            effectiveDist <= (transform.position - startPosition).sqrMagnitude)
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

    // �f�o�b�O�p
    public Vector3 GetVelocityEstimate_Debug()
    {
        Vector3 velocity = Vector3.zero;

        Debug.Log(velocitySamples[0]);
        for (int i = 0; i < velocitySampleFreams; i++)
        {
            velocity += velocitySamples[i];
        }
        velocity *= (1.0f / velocitySampleFreams);

        return velocity;
    }

    // �f�o�b�O�p
    public float GetDistance_Debug()
    {
        Vector3 velocity = Vector3.zero;

        Debug.Log(velocitySamples[0]);
        for (int i = 0; i < velocitySampleFreams; i++)
        {
            velocity += velocitySamples[i];
        }
        velocity *= (1.0f / velocitySampleFreams);

        return velocity.sqrMagnitude;
    }
}

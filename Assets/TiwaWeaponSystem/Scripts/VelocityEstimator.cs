
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class VelocityEstimator : UdonSharpBehaviour
{
    public int velocitySampleFreams = 5;
    public int attackSampleFreams = 5;
    public float attackSpeed = 20f;
    public float attackAccel = 20f;
    public float staticSpeed = 10f;
    public float effectiveDist = 5f;
    public GameObject Attacker;

    // できるだけローカル変数を使わない
    Vector3 prevPosition;
    Vector3 startPosition;
    int sampleCount;
    int attackSampleCount;
    int i;
    Vector3[] velocitySamples;
    Vector3 velocity;
    Vector3 acceleration;
    float velocityScalar;
    float accelerationScalar;
    float dist;

    private void Start()
    {
        velocitySamples = new Vector3[velocitySampleFreams];
        prevPosition = transform.position;
    }

    // 参考: https://github.com/wacki/Unity-VRInputModule/blob/master/Assets/SteamVR/InteractionSystem/Core/Scripts/VelocityEstimator.cs
    private void Update()
    {
        // 速度取得
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

        // 加速度
        acceleration = Vector3.zero;
        for (int i = 2 + sampleCount - velocitySamples.Length; i < sampleCount; i++)
        {
            if (i >= 2)
            {
                int first = i - 2;
                int second = i - 1;

                Vector3 v1 = velocitySamples[first % velocitySamples.Length];
                Vector3 v2 = velocitySamples[second % velocitySamples.Length];
                acceleration += v2 - v1;
            }
        }
        acceleration *= (1.0f / Time.deltaTime);
        accelerationScalar = acceleration.sqrMagnitude;

        // 静止状態の位置を取得
        if (velocityScalar < staticSpeed)
        {
            startPosition = transform.position;
        }

        dist = (transform.position - startPosition).sqrMagnitude;
        // 速度と加速度と静止状態からの移動距離で判定
        if (velocityScalar >= attackSpeed &&
            accelerationScalar >= attackAccel &&
            effectiveDist <= dist)
        {
            attackSampleCount = 0;
            Attacker.SetActive(true);
        }
        else
        {
            // 攻撃判定に連続でattackSampleFreams回失敗すると攻撃終了
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

    // デバッグ用
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

    // デバッグ用
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


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

    // できるだけローカル変数を使わない
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

        // 静止状態の位置を取得
        if(velocityScalar < staticSpeed)
        {
            startPosition = transform.position;
        }

        // 速度と静止状態からの移動距離で判定
        if (velocityScalar >= attackSpeed &&
            effectiveDist <= (transform.position - startPosition).sqrMagnitude)
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

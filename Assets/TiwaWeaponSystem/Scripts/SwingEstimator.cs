
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SwingEstimator : UdonSharpBehaviour
{
    // 測定点のオブジェクト
    public GameObject objTip;
    public GameObject objHandle;

    const int velocitySampleFreams = 5; // サンプル数は5で固定
    const float velocityFactor = 0.2f; // サンプル数は固定なので前もって計算
    public int attackSampleFreams = 5;

    // MEMO: 動作確認した結果良い感じだった値をデフォルトにしておく。測定点オブジェクトの位置も同様
    public float attackSpeedTip = 50f;
    public float staticSpeedTip = 50f;
    public float effectiveDistTip = 1.5f;

    public float attackSpeedHandle = 150f;
    public float staticSpeedHandle = 50f;
    public float effectiveDistHandle = 0.55f;

    public GameObject Attacker;

    // ローカル変数をできるだけ使わない
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

    // 参考: https://github.com/wacki/Unity-VRInputModule/blob/master/Assets/SteamVR/InteractionSystem/Core/Scripts/VelocityEstimator.cs
    // できるだけ関数呼び出しをしない
    private void Update()
    {
        sampleCount++;

        // Tip
        // 速度計算
        velocitySamplesTip[sampleCount % velocitySamplesTip.Length] = (1.0f / Time.deltaTime) * (objTip.transform.position - prevPositionTip);
        prevPositionTip = objTip.transform.position;

        velocityTip = Vector3.zero;
        for (i = 0; i < velocitySampleFreams; i++)
        {
            velocityTip += velocitySamplesTip[i];
        }
        velocityTip *= velocityFactor;
        velocityScalarTip = velocityTip.sqrMagnitude;

        // 静止状態の位置を取得
        if (velocityScalarTip < staticSpeedTip)
        {
            startPositionTip = objTip.transform.position;
        }

        // スイング距離
        movingDistTip = (objTip.transform.position - startPositionTip).sqrMagnitude;

        // Handle
        // 速度計算
        velocitySamplesHandle[sampleCount % velocitySamplesHandle.Length] = (1.0f / Time.deltaTime) * (objHandle.transform.position - prevPositionHandle);
        prevPositionHandle = objHandle.transform.position;

        velocityHandle = Vector3.zero;
        for (i = 0; i < velocitySampleFreams; i++)
        {
            velocityHandle += velocitySamplesHandle[i];
        }
        velocityHandle *= velocityFactor;
        velocityScalarHandle = velocityHandle.sqrMagnitude;

        // 静止状態の位置を取得
        if (velocityScalarHandle < staticSpeedHandle)
        {
            startPositionHandle = objHandle.transform.position;
        }

        // スイング距離
        movingDistHandle = (objHandle.transform.position - startPositionHandle).sqrMagnitude;

        // 速度と加速度と静止状態からの移動距離で判定
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
}

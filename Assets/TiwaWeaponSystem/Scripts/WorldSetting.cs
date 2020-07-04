
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class WorldSetting : UdonSharpBehaviour
{
    public float jumpPower;

    void Start()
    {
        if (Networking.LocalPlayer != null)
        {
            Networking.LocalPlayer.SetJumpImpulse(jumpPower);
        }
    }
}

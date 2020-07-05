
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class NPCSystem : UdonSharpBehaviour
{
    DelayTimer thisTimer;
    Collider thisCollider;
    public MeshRenderer thisMeshRenderer;
    public Material hitMaterial;
    public Material normalMaterial;

    private void Start()
    {
        thisTimer = GetComponent<DelayTimer>();
        thisCollider = GetComponent<Collider>();
        thisTimer.SetTimerCapacity(1);

        thisMeshRenderer.material = normalMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<WeaponAttack>())
        {
            if(thisCollider.enabled == true)
            {
                thisCollider.enabled = false;
                thisMeshRenderer.material = hitMaterial;
                thisTimer.StartTimer(0, this, "FinishInvisibleTime", 0.25f);
            }
        }
    }

    public void FinishInvisibleTime()
    {
        thisCollider.enabled = true;
        thisMeshRenderer.material = normalMaterial;
    }
}

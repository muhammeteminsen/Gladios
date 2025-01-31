using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownHandler : MonoBehaviour
{
    public CooldownHandler Insatance;
    private bool canUse = true;
    private void Awake()
    {
        Insatance = this;
    }
    public void StartCooldown(int CooldownTime)
    {
        if (canUse)
        {
            StartCoroutine(CooldownCoroutine(CooldownTime));
        }
    }

    private IEnumerator CooldownCoroutine(int cooldownTime)
    {
        canUse = false;
        yield return new WaitForSeconds(cooldownTime);
        canUse = true;
    }

    public bool IsReady()
    {
        return canUse;
    }
}

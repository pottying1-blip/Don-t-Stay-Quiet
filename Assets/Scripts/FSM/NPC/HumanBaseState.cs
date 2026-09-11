using UnityEngine;

public abstract class HumanBaseState
{
    public abstract void EnterState(HumanStateManager humanState);
    public abstract void UpdateState(HumanStateManager humanState);
    public abstract void OnCollisionEnter(HumanStateManager humanState);
}

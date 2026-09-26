using UnityEngine;

public class DeathEvent : MonoBehaviour
{
    [SerializeField]private HumanStateManager humanStateManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TriggerExplosionDeath();
    }

    public void TriggerExplosionDeath()
    {
        humanStateManager.deathCause = DeathCause.Explosion;
        humanStateManager.isDead = true;
        humanStateManager.SwitchState(humanStateManager.humanDeadState);
    }
}

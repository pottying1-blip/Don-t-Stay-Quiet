
using Unity.VisualScripting;
using UnityEngine;

public class HumanAttackState : HumanBaseState
{
    
    public override void EnterState(HumanStateManager humanState)
    {
        Debug.Log("QUAI VAT!");
        humanState.isPursuing = true;
    }

    public override void UpdateState(HumanStateManager humanState)
    {
        float rotationSpeed = 50f;
        float cooldownTime = 1f;
        float distance = Vector2.Distance(humanState.transform.position, humanState.playerController.transform.position);
        Vector2 direction = (Vector2)humanState.playerController.transform.position - (Vector2)humanState.transform.position;
        float rotateAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion entityRotation = Quaternion.Euler(0,0,rotateAngle);

        if (!humanState.playerController.isInvisible && distance < humanState.soldierAttackDis 
        && humanState.playerController.currentState != humanState.playerController.disguisedState)
        {
            humanState.transform.rotation = Quaternion.RotateTowards(humanState.transform.rotation,
            entityRotation, rotationSpeed*Time.deltaTime);

            if (Time.time >= humanState.lastSpawnTime + cooldownTime)
            {
                FiredBullet(humanState, direction, entityRotation);
            }
        }
        if (humanState.isPursuing && humanState.playerController.isInvisible 
        && humanState.playerController.currentState != humanState.playerController.disguisedState
        || humanState.isPursuing && humanState.playerController.currentState == humanState.playerController.disguisedState)
        {
            humanState.onHoldAttackTimer += Time.deltaTime;
            if (humanState.onHoldAttackTimer >= humanState.returnToPatrolTime)
            {
                humanState.gameManager.falseAlarm = true;
                humanState.onHoldAttackTimer = 0f;
                humanState.isPursuing = false;
                humanState.isReturningToPos = true;
                if (humanState.nPCData.canPatrol)
                {
                    humanState.SetDestination(humanState.posA);
                } 
                else
                {humanState.SetDestination(humanState.stationaryPatrolPos);}
            }
        } 

        if (humanState.isReturningToPos 
        && !humanState.navMeshAgent.pathPending 
        && humanState.navMeshAgent.remainingDistance <= 0.1f)
        {
            humanState.isReturningToPos = false;
            humanState.gameManager.falseAlarm = false;
            humanState.gameManager.hasAlert = false;
            humanState.gameManager.hasPlayAlert = false;

            if (humanState.gameManager.currentAlert.TryGetComponent<AlertReturn>(out var button))
            button.isResolved = true;
            humanState.SwitchState(humanState.humanPatrolState);
        }
    }

    void FiredBullet(HumanStateManager humanState, Vector2 direction, Quaternion entityRotation)
    {
        GameObject bullet = Object.Instantiate<GameObject>(humanState.nPCData.bulletPrefab, humanState.transform.position, humanState.transform.rotation);
        if (bullet.TryGetComponent<Bullet>(out var bullets))
        {
            bullets.Launch(direction, entityRotation);
            humanState.humanSoundSource.PlayOneShot(humanState.gunSound);
        }
        humanState.lastSpawnTime = Time.time;
    }

    public override void OnCollisionEnter(HumanStateManager humanState)
    {
        
    }

}

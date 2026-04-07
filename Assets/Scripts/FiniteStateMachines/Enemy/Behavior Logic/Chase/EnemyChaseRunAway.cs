using UnityEngine;

[CreateAssetMenu(fileName = "EnemyChaseRunAway", menuName = "Enemy/Chase/Direct Chase")]
public class EnemyChaseRunAway : EnemyChaseSOBase
{
    [SerializeField] private float _runAwaySpeed = 1.5f;
    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
    }
    override public void DoEnterLogic()
    {
        base.DoEnterLogic();

    }

    override public void DoExitLogic()
    {
        base.DoExitLogic();
    }

    override public void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        Vector2 runDir = -(playerTransform.position - enemy.transform.position).normalized;
        enemy.MoveEnemy(runDir * _runAwaySpeed);

    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }
}

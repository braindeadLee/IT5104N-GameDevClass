using UnityEngine;

[CreateAssetMenu(fileName = "EnemyIdleStandStill", menuName = "Enemy/Idle/Stand Still")]
public class EnemyIdleStandStill : EnemyIdleSOBase
{
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

        enemy.MoveEnemy(Vector2.zero);

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

using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Direct Chase", menuName = "Enemy/Chase/Direct Chase")]
public class EnemyChaseDirectToPlayer : EnemyChaseSOBase
{
    [SerializeField] private float _movementSpeed = 1.75f;
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

        Vector2 movementDirection = (playerTransform.position - enemy.transform.position).normalized;
        enemy.MoveEnemy(movementDirection * _movementSpeed);

        if(enemy.IsWithinStrikingDistance)
        {
            enemy.StateMachine.ChangeState(enemy.AttackState);
        }

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

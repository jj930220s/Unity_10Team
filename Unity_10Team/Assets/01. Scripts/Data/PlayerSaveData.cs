[System.Serializable]
public class PlayerSaveData
{
    public float hp;
    public float attack;
    public float defence;
    public float attackDelay;
    public float speed;
    public float rotationDamping;

    public float moveSpeedModifier;
    public float attackMoveSpeedModifier;

    public PlayerSaveData(PlayerSObj playerSObj)
    {
        hp = playerSObj.defaultData.baseHP;
        attack = playerSObj.defaultData.baseAttack;
        defence = playerSObj.defaultData.baseDefence;
        attackDelay = playerSObj.defaultData.baseAttackDelay;
        speed = playerSObj.defaultData.baseSpeed;
        rotationDamping = playerSObj.defaultData.baseRotationDamping;

        moveSpeedModifier = playerSObj.defaultData.moveSpeedModifier;
        attackMoveSpeedModifier = playerSObj.attackData.attackMoveSpeedModifier;
    }
}

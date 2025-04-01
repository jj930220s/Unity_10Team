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

    public PlayerSaveData(PlayerStatus status)
    {
        hp = status.status[STATTYPE.HP];
        attack = status.status[STATTYPE.ATK];
        defence = status.status[STATTYPE.DEF];
        attackDelay = status.status[STATTYPE.ATKDELAY];
        speed = status.status[STATTYPE.SPEED];
    }
}

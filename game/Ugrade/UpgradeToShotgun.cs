internal class UpgradeToShotgun : Upgrade
{
    public override void action(Player player)
    {
        player.weapon = new ShotgunWeapon();
    }
    public UpgradeToShotgun() : base()
    {

    }
}
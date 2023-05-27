internal class UpgradeToRifle : Upgrade
{
    public override void action(Player player)
    {
        player.weapon = new RifleWeapon();
    }
    public UpgradeToRifle() : base()
    {

    }
}
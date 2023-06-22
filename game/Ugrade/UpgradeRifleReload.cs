internal class UpgradeRifleReload : Upgrade
{
    public override void action(Player player)
    {
        player.weapon.ReloadTime -= 0.025f;
    }
    public UpgradeRifleReload() : base()
    {

    }
}
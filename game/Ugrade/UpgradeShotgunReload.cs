internal class UpgradeShotgunReload : Upgrade
{
    public override void action(Player player)
    {
        player.weapon.ReloadTime -= 0.05f;
    }
    public UpgradeShotgunReload() : base()
    {

    }
}
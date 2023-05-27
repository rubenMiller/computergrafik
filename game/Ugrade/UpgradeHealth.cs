internal class UpgradeHealth : Upgrade
{
    public override void action(Player player)
    {
        player.maxHealth += 2;
    }
    public UpgradeHealth() : base()
    {

    }
}
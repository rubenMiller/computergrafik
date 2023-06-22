internal class UpgradeRifleRange : Upgrade
{
    public override void action(Player player)
    {
        player.weapon.Range += 2f;
    }
    public UpgradeRifleRange() : base()
    {

    }
}
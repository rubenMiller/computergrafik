internal class UpgradeShotgunBullets : Upgrade
{
    public override void action(Player player)
    {
        if(player.weapon is ShotgunWeapon shotgun)
        {
            shotgun.AdditionalBulletsPerSide++;
        }
    }
    public UpgradeShotgunBullets() : base()
    {

    }
}
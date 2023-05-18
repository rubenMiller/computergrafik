using System;
using System.Collections.Generic;
using OpenTK.Mathematics;


internal class EnemySpawner
{
    private List<Enemy> SpawnEnemies(int numberOfEnemies, Player player, int enemyType)
    {
        List<Enemy> listOfEnemies = new List<Enemy>();
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector2 center = new Vector2(random.Next(-50, 50) * 0.1f, random.Next(-50, 50) * 0.1f);
            if (Vector2.Distance(player.Center, center) < 1f)
            {
                i--;
                continue;
            }
            Enemy enemy = new Enemy(center, enemyType);
            listOfEnemies.Add(enemy);
        }
        return listOfEnemies;
    }

    public List<Enemy> MakeEnemies(Player player, int waveCount)
    {
        List<Enemy> listOfEnemies = SpawnEnemies(50 + 15 * waveCount, player, 1);
        listOfEnemies.AddRange(SpawnEnemies(5 + 10 * waveCount, player, 2));
        listOfEnemies.AddRange(SpawnEnemies(3 + 2 * waveCount, player, 3));

        return listOfEnemies;
    }

    private Random random = new Random();


    public EnemySpawner()
    {

    }
}

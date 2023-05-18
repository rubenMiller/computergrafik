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
            Vector2 center = new Vector2(random.Next(-5, 5), random.Next(-5, 5));
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

    public List<Enemy> MakeEnemies(Player player)
    {
        List<Enemy> listOfEnemies = SpawnEnemies(50, player, 1);
        listOfEnemies.AddRange(SpawnEnemies(15, player, 2));
        listOfEnemies.AddRange(SpawnEnemies(2, player, 3));

        return listOfEnemies;
    }

    private Random random = new Random();


    public EnemySpawner()
    {
        
    }
}

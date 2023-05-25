using System;
using System.Collections.Generic;
using OpenTK.Mathematics;

internal class Wave
{
    private List<Enemy> SpawnEnemies(int numberOfEnemies, Player player, Func<Vector2, Enemy> enemyCreator)
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
            Enemy enemy = enemyCreator(center);
            listOfEnemies.Add(enemy);
        }
        return listOfEnemies;
    }
    private List<Enemy> MakeEnemies(Player player)
    {
        List<Enemy> listOfEnemies = new List<Enemy>();
        listOfEnemies.AddRange(SpawnEnemies(5 + 2 * WaveCount, player, center => new baseEnemy(center)));
        listOfEnemies.AddRange(SpawnEnemies(5 + 2 * WaveCount, player, center => new runnerEnemy(center)));
        listOfEnemies.AddRange(SpawnEnemies(1 + 1 * WaveCount, player, center => new bigEnemy(center)));
        listOfEnemies.AddRange(SpawnEnemies(0 + WaveCount * 1, player, center => new shootingEnemy(center)));

        return listOfEnemies;
    }

    public void Update(float elapsedTime, Player player, List<Enemy> listOfEnemies)
    {
        waveTime = waveTime + elapsedTime;
        timeSinceLastSpawn = timeSinceLastSpawn + elapsedTime;
        if (waveTime > 90f)
        {
            WaveCount++;
            waveTime = 0f;
        }
        if (timeSinceLastSpawn > timeBetweenSPawns)
        {
            listOfEnemies.AddRange(MakeEnemies(player));
            timeSinceLastSpawn = 0f;
        }
    }

    public bool readyForNewWave = true;
    public int WaveCount = 1;
    public float waveTime = 0;
    private float timeSinceLastSpawn = 9f;
    private float timeBetweenSPawns = 10f;
    private Random random = new Random();

    public Wave()
    {

    }
}
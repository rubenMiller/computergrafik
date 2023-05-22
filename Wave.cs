using System;
using System.Collections.Generic;
using OpenTK.Mathematics;

internal class Wave
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
            Enemy enemy = new Enemy(center, enemyType, new Vector2(0, 0));
            listOfEnemies.Add(enemy);
        }
        return listOfEnemies;
    }

    private List<Enemy> MakeEnemies(Player player)
    {
        List<Enemy> listOfEnemies = SpawnEnemies(5 + 2 * WaveCount, player, 1);
        listOfEnemies.AddRange(SpawnEnemies(5 + 2 * WaveCount, player, 2));
        listOfEnemies.AddRange(SpawnEnemies(1 + 1 * WaveCount, player, 3));
        listOfEnemies.AddRange(SpawnEnemies(1 + WaveCount * 1, player, 4));

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
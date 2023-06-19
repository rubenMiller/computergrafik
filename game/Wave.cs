using System;
using System.Collections.Generic;
using OpenTK.Mathematics;

internal class Wave
{
    private float getRandomCoordinates(int Max, int Min)
    {
        bool useNegativeRange = random.Next(2) == 0;

        if (useNegativeRange)
        {
            return random.Next((Min - 5) * 10, Min * 10) * 0.1f;
        }
        else
        {
            return random.Next(Max * 10, (Max + 5 * 10)) * 0.1f;
        }
    }
    private List<Enemy> SpawnEnemies(int numberOfEnemies, Player player, GameBorder gameBorder, Func<Vector2, Enemy> enemyCreator)
    {
        List<Enemy> listOfEnemies = new List<Enemy>();
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector2 center = new Vector2(getRandomCoordinates(gameBorder.MaxX, gameBorder.MinX), getRandomCoordinates(gameBorder.MaxY, gameBorder.MinY));
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
    private List<Enemy> MakeEnemies(Player player, GameBorder gameBorder)
    {
        List<Enemy> listOfEnemies = new List<Enemy>();
        listOfEnemies.AddRange(SpawnEnemies(4 + 2 * WaveCount, player, gameBorder, center => new baseEnemy(center)));
        listOfEnemies.AddRange(SpawnEnemies(3 + 2 * WaveCount, player, gameBorder, center => new runnerEnemy(center)));
        listOfEnemies.AddRange(SpawnEnemies(0 + 1 * (WaveCount - 1), player, gameBorder, center => new bigEnemy(center)));
        listOfEnemies.AddRange(SpawnEnemies(0 + 1 * WaveCount, player, gameBorder, center => new shootingEnemy(center)));

        return listOfEnemies;
    }

    public void Update(float elapsedTime, Player player, List<Enemy> listOfEnemies, GameBorder gameBorder, GameState gameState)
    {
        waveTime = waveTime + elapsedTime;
        timeSinceLastSpawn = timeSinceLastSpawn + elapsedTime;
        if (waveTime > 10f && listOfEnemies.Count == 0)
        {
            WaveCount++;
            waveTime = 0f;
            gameState.transitionToState(GameState.STATE.STATE_WAVEOVER);
        }
        if (timeSinceLastSpawn > timeBetweenSPawns && waveTime < 45f)
        {
            listOfEnemies.AddRange(MakeEnemies(player, gameBorder));
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
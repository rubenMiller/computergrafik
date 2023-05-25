public class GameBorder
{
    public int MaxX { get; private set; }
    public int MaxY { get; private set; }
    public int MinX { get; private set; }
    public int MinY { get; private set; }
    public GameBorder(int maxX, int maxY, int minX, int minY)
    {
        MaxX = maxX;
        MaxY = maxY;
        MinX = minX;
        MinY = minY;
    }
}
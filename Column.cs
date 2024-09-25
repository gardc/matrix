namespace Matrix;

public class Column
{
    private static readonly Random random = new();
    public Character Char { get; private set; } = new();
    public int Position { get; private set; }
    public int Velocity { get; private set; }
    private int ConsoleHeight { get; set; }

    public Column(int consoleHeight)
    {
        ConsoleHeight = consoleHeight;
        Reset();
    }

    public void Reset()
    {
        Position = -1 * random.Next(ConsoleHeight);
        Velocity = random.Next(1, 3);
        Char.Regenerate();
    }

    public void Move()
    {
        Position += Velocity;
        if (Position >= ConsoleHeight)
        {
            Reset();
        }
    }
}
namespace Matrix;

public class Character
{
    private static readonly Random random = new();
    public char Char { get; private set; }

    public Character()
    {
        Regenerate();
    }

    public void Regenerate()
    {
        Char = (char)random.Next(33, 127);
    }
}

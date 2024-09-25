using System.CommandLine;
using Matrix;

var clearOption = new Option<bool>(
    name: "--clear",
    description: "Clear the console when exiting.",
    getDefaultValue: () => false);

var rootCommand = new RootCommand("Matrix-like console animation");
rootCommand.AddOption(clearOption);

rootCommand.SetHandler((clearOptionValue) =>
{
    RunMatrixAnimation(clearOptionValue);
}, clearOption);

return rootCommand.Invoke(args);

static void RunMatrixAnimation(bool clearOnExit)
{
    Console.CursorVisible = false;
    int width = Console.WindowWidth;
    int height = Console.WindowHeight;

    List<Column> columns = Enumerable.Range(0, width)
        .Select(_ => new Column(height))
        .ToList();
    Console.CancelKeyPress += delegate
    {
        Console.CursorVisible = true;
        if (clearOnExit)
        {
            Console.Clear();
        }
    };

    char[,] previousFrame = new char[height, width];
    while (true)
    {
        char[,] currentFrame = new char[height, width];

        // for each column x in width of console, get the column and move its char
        for (int x = 0; x < width; x++)
        {
            Column column = columns[x];
            if (column.Position >= 0 && column.Position < height) // if column is within the console height 
            {
                currentFrame[column.Position, x] = column.Char.Char;
            }
            column.Move();
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (currentFrame[y, x] != previousFrame[y, x])
                {
                    Console.SetCursorPosition(x, y);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(currentFrame[y, x] == '\0' ? ' ' : currentFrame[y, x]);
                }
            }
        }

        previousFrame = (char[,])currentFrame.Clone();

        Thread.Sleep(50);
    }
}
namespace CyberguardGame;
public class MazeLevel
{
    public int[,] Grid {get; private set;}                          // Macierz reprezentująca labirynt
    public (int X, int Y) StartPoint {get; private set;}            // Punkt początkowy
    public (int X, int Y) EndPoint {get; private set;}              // Punkt końcowy
    public int Width => Grid.GetLength(1);                          // Liczba kolumn
    public int Height => Grid.GetLength(0);                         // Liczba wierszy

    // Konstruktor klasy
    public MazeLevel(int[,] grid, (int X, int Y) startPoint, (int X, int Y) endPoint)
    {
        Grid = grid;
        StartPoint = startPoint;
        EndPoint = endPoint;
    }

    public static MazeLevel Easy => new MazeLevel(
        new int[,]
            {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 0, 0, 0, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 0, 0, 0, 0, 1, 1, 1 },
                { 1, 0, 0, 0, 1, 1, 0, 0, 0, 1 },
                { 1, 0, 1, 1, 1, 1, 1, 1, 0, 0 },
                { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
            },
        startPoint: (0, 1),
        endPoint: (9, 5)    
    );

    public static MazeLevel Medium => new MazeLevel(
        new int[,]
            {
                { 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1},
                { 1, 0, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1},
                { 1, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1},
                { 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1},
                { 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1},
                { 1, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0},
                { 1, 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1},
                { 1, 0, 1, 1, 0, 0, 0, 1, 0, 1, 1, 1},
                { 1, 0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 1},
                { 0, 0, 1, 1, 0, 1, 0, 0, 0, 1, 1, 1},
                { 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1},
            },
        startPoint: (0, 9),
        endPoint: (11, 5)
    );

    public static MazeLevel Hard => new MazeLevel(
        new int[,]
            {
                { 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1},
                { 1, 0, 1, 1, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1},
                { 1, 0, 1, 1, 0, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1},
                { 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 0, 1},
                { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 1, 1},
                { 1, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1},
                { 1, 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1},
                { 1, 0, 1, 1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1},
                { 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 1},
                { 1, 0, 1, 1, 0, 1, 0, 0, 0, 1, 1, 1, 0, 1, 1},
                { 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1},
                { 1, 0, 1, 1, 0, 1, 0, 0, 0, 1, 1, 1, 0, 0, 0},
                { 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            },
        startPoint: (0, 4),
        endPoint: (14, 11)
    );
}
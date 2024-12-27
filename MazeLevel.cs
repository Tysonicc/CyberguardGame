namespace CyberguardGame;
public class MazeLevel                                              /** Klasa reprezentująca poziom labiryntu */
{
    public int[,] Grid {get; private set;}                          /** Macierz reprezentująca labirynt */
    public (int X, int Y) StartPoint {get; private set;}            /** Punkt początkowy */
    public (int X, int Y) EndPoint {get; private set;}              /** Punkt końcowy */
    public int Width => Grid.GetLength(1);                          /** Liczba kolumn (szerokość labiryntu) */
    public int Height => Grid.GetLength(0);                         /** Liczba wierszy (wysokość labiryntu) */

    /** Konstruktor klasy, który inicjalizuje macierz labiryntu i punkt startowy oraz końcowy macierzy */
    public MazeLevel(int[,] grid, (int X, int Y) startPoint, (int X, int Y) endPoint)
    {
        Grid = grid;                                                /** Ustawienie macierzy labiryntu */
        StartPoint = startPoint;                                    /** Ustawienie punktu początkowego */
        EndPoint = endPoint;                                        /** Ustawienie punktu końcowego */
    }

    public static MazeLevel Easy => new MazeLevel(                  /** Łatwy poziom labiryntu */
        
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
        
        startPoint: (0, 1),                                         /** Ustawienie punktu początkowego macierzy */
        endPoint: (9, 5)                                            /** Ustawienie punktu końcowego macierzy */
    );

    public static MazeLevel Medium => new MazeLevel(                /** Średni poziom labiryntu */
        
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
        
        startPoint: (0, 9),                                         /** Ustawienie punktu początkowego macierzy */
        endPoint: (11, 5)                                           /** Ustawienie punktu końcowego macierzy */
    );

    public static MazeLevel Hard => new MazeLevel(                  /** Trudny poziom labiryntu */
        
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
        
        startPoint: (0, 4),                                         /** Ustawienie punktu początkowego macierzy */
        endPoint: (14, 11)                                          /** Ustawienie punktu końcowego macierzy */
    );
}
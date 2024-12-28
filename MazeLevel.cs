namespace CyberguardGame;

/** Klasa reprezentujaca poziom labiryntu */
public class MazeLevel                                              
{
    /** Macierz reprezentujaca labirynt */
    public int[,] Grid {get; private set;}
    /** Punkt poczatkowy */
    public (int X, int Y) StartPoint {get; private set;}
    /** Punkt końcowy */
    public (int X, int Y) EndPoint {get; private set;}
    /** Liczba kolumn (szerokosz labiryntu) */
    public int Width => Grid.GetLength(1);
    /** Liczba wierszy (wysokosz labiryntu) */
    public int Height => Grid.GetLength(0);                         

    /** Konstruktor klasy, ktory inicjalizuje macierz labiryntu i punkt startowy oraz końcowy macierzy */
    public MazeLevel(int[,] grid, (int X, int Y) startPoint, (int X, int Y) endPoint)
    {
        /** Ustawienie macierzy labiryntu */
        Grid = grid;
        /** Ustawienie punktu poczatkowego */
        StartPoint = startPoint;
        /** Ustawienie punktu końcowego */
        EndPoint = endPoint;                                        
    }

    /** latwy poziom labiryntu */
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

        /** Ustawienie punktu poczatkowego macierzy */
        startPoint: (0, 1),
        /** Ustawienie punktu końcowego macierzy */
        endPoint: (9, 5)                                            
    );

    /** sredni poziom labiryntu */
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

        /** Ustawienie punktu poczatkowego macierzy */
        startPoint: (0, 9),
        /** Ustawienie punktu końcowego macierzy */
        endPoint: (11, 5)                                           
    );

    /** Trudny poziom labiryntu */
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

        /** Ustawienie punktu poczatkowego macierzy */
        startPoint: (0, 4),
        /** Ustawienie punktu końcowego macierzy */
        endPoint: (14, 11)                                          
    );
}
using CyberguardGame;

/** Klasa reprezentujaca pozycje gracza */
public class Player                                     
{
    /** Wlasciwosc reprezentujaca pozycje gracza na osi X */
    public int X {get; private set;}
    /** Wlasciwosc reprezentujaca pozycje gracza na osi Y */
    public int Y {get; private set;}
    /** Konstruktor klasy, ktory inicjalizuje pozycje gracza */
    public Player(int x, int y)                         
    {
        /** Ustawienie pozycji X gracza */
        X = x;
        /** Ustawienie pozycji Y gracza */
        Y = y;                                          
    }

    /** Metoda do poruszania sie gracza w labiryncie */
    public void Move(Keys key, Maze maze)               
    {
        /** Zmienna potrzebna do przechowania nowej pozycji X gracza */
        int newX = X;
        /** Zmienna potrzebna do przechowania nowej pozycji Y gracza */
        int newY = Y;

        /** Poruszanie sie gracza */
        switch (key)                                    
        {
            /** Ruch w gore */
            case Keys.W:                     
                newY--;
                break;

            /** Ruch w lewo */
            case Keys.A:              
                newX--;
                break;

            /** Ruch w dol */
            case Keys.S:                                
                newY++;
                break;

            /** Ruch w prawo */
            case Keys.D:           
                newX++;
                break;
        }

        /** Sprawdzenie, czy nowa pozycja gracza jest w obrebie labiryntu i nie jest sciana */
        if (newX >= 0 && newX < maze.Width && newY >= 0 && newY < maze.Height && maze.Grid[newY, newX] != 1)
        {
            /** Nowa pozycja gracza na osi X */
            X = newX;
            /** Nowa pozycja gracza na osi Y */
            Y = newY;                                   
        }
    }
}
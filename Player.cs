using CyberguardGame;

public class Player                                     // Klasa reprezentująca pozycję gracza
{
    public int X {get; private set;}                    // Właściwość reprezentująca pozycję gracza na osi X
    public int Y {get; private set;}                    // Właściwość reprezentująca pozycję gracza na osi Y
    public Player(int x, int y)                         // Konstruktor klasy, który inicjalizuje pozycję gracza
    {
        X = x;                                          // Ustawienie pozycji X gracza
        Y = y;                                          // Ustawienie pozycji Y gracza
    }

    public void Move(Keys key, Maze maze)               // Metoda do poruszania się gracza w labiryncie
    {
        int newX = X;                                   // Zmienna potrzebna do przechowania nowej pozycji X gracza
        int newY = Y;                                   // Zmienna potrzebna do przechowania nowej pozycji Y gracza

        switch (key)                                    // Poruszanie się gracza
        {
            case Keys.W:                                // Ruch w górę
                
                newY--;
                break;
            
            case Keys.A:                                // Ruch w lewo
                
                newX--;
                break;
            
            case Keys.S:                                // Ruch w dół
                
                newY++;
                break;
            
            case Keys.D:                                // Ruch w prawo
                
                newX++;
                break;
        }

        // Sprawdzenie, czy nowa pozycja gracza jest w obrębie labiryntu i nie jest ścianą
        if (newX >= 0 && newX < maze.Width && newY >= 0 && newY < maze.Height && maze.Grid[newY, newX] != 1)
        {
            X = newX;                                   // Nowa pozycja gracza na osi X
            Y = newY;                                   // Nowa pozycja gracza na osi Y
        }
    }
}
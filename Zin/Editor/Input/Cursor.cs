namespace Zin.Editor;

public struct Cursor
{
    public int X;
    public int Y;

    public Cursor()
    {
        X = 0;
        Y = 0;
    }

    public Cursor(int x, int y)
    {
        X = x;
        Y = y;
    }
}
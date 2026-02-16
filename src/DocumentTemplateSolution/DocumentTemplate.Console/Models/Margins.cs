using DocumentTemplate.Console.Interfaces;

namespace DocumentTemplate.Console.Models;

public class Margins : IPrototype<Margins>
{
    public int Top { get; set; }
    public int Bottom { get; set; }
    public int Left { get; set; }
    public int Right { get; set; }
    
    public Margins Clone()
    {
        return new Margins
        {
            Top = Top,
            Bottom = Bottom,
            Left = Left,
            Right = Right
        };
    }
}
namespace DocumentTemplate.Console.Interfaces;

public interface IPrototype<T>
{
    T Clone();
}
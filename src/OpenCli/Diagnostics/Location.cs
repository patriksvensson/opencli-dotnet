namespace OpenCli;

[PublicAPI]
public readonly struct Location
{
    public int Row { get; init; }
    public int Column { get; init; }
}
public struct GridPosition
{
    public int _xPosition;
    public int _zPosition;

    public GridPosition(int xPosition, int zPosition)
    {
        _xPosition = xPosition;
        _zPosition = zPosition;
    }

    public override string ToString()
    {
        return $"x: {_xPosition} z: {_zPosition}";
    }
}
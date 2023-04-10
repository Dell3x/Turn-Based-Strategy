using System;

public struct GridPosition : IEquatable<GridPosition>
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

    public bool Equals(GridPosition other)
    {
        return this == other;
    }

    public override bool Equals(object obj)
    {
        return obj is GridPosition position && _xPosition == position._xPosition && _zPosition == position._zPosition;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_zPosition, _xPosition);
    }
    
    public static bool operator ==(GridPosition a, GridPosition b)
    {
        return a._xPosition == b._xPosition && a._zPosition == b._zPosition;
    }

    public static bool operator !=(GridPosition a, GridPosition b)
    {
        return !(a == b);
    }
}
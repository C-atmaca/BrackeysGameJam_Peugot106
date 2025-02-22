using UnityEngine;

public enum TileType
{
    Empty, Straight, Corner, ThreeWay, FourWay, Room, Enterance
}

public struct Tile
{
    public TileType tileType;
};

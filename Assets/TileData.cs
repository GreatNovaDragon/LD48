using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New MyTile", menuName = "Tile with Data")]
public class TileData : Tile  // or TileBase or RuleTile or other
{
    // will be able to plug in value you want in Inspector for asset
    public enum e_TileType{
          Ground, Wall, Above
    }
    public e_TileType TileType;
}

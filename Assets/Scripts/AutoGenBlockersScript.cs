#if (UNITY_EDITOR)

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class AutoGenBlockersScript : MonoBehaviour
{
    // Auto generate tiles onto a tilemap with colliders to block player from going into the water

    // Iterate through each tile in src_tilemap and if contains water_tiles,
    // copy that tile to water_blocker_tilemap.
    // water_blocker_tilemap should already have colliders

    //REFs:
    //https://learn.unity.com/tutorial/editor-scripting
    //https://discourse.mapeditor.org/t/automatically-setting-collisions-on-each-tile/3775/16
    //https://forum.unity.com/threads/tilemap-tile-positions-assistance.485867/#post-3165314
    //https://docs.unity3d.com/ScriptReference/Tilemaps.Tilemap.html
    public GameObject src_tilemap_obj;
    private Tilemap src_tilemap_ = null;
    public Tilemap water_blocker_tilemap;
    public List<Tile> water_tiles;
    private List<string> water_tile_names_;

    // NOT USED
    private Vector3 spawnPoint;
    private List<Vector3> availablePlaces;

    public void AutoGenBlockers()
    {
        src_tilemap_ = src_tilemap_obj.GetComponent<Tilemap>();
        availablePlaces = new List<Vector3>();

        //foreach (var item in water_tiles)
        //{
        //    if (item)
        //    {
        //        water_tile_names_.Add(item.name);
        //    }
        //}
        for (int i = 0; i < water_tiles.Count; i++)
        {
            if (water_tiles[i])
            {
                water_tile_names_.Add(water_tiles[i].name);
            }
            else
            {
                Debug.Log(">>> ERROR at " + i);
            }
        }
        
        water_blocker_tilemap.ClearAllTiles();
        for (int n = src_tilemap_.cellBounds.xMin; n < src_tilemap_.cellBounds.xMax; n++)
        {
            for (int p = src_tilemap_.cellBounds.yMin; p < src_tilemap_.cellBounds.yMax; p++)
            {
                Vector3Int tile_local_pos = (new Vector3Int(n, p, (int)src_tilemap_.transform.position.y));
                Vector3 tile_world_pos = src_tilemap_.CellToWorld(tile_local_pos);
                if (src_tilemap_.HasTile(tile_local_pos))
                {
                    //Tile at "place"
                    //availablePlaces.Add(place);
                    if (water_tile_names_.Contains(src_tilemap_.GetTile(tile_local_pos).name))
                    {
                        water_blocker_tilemap.SetTile(tile_local_pos, src_tilemap_.GetTile(tile_local_pos));
                    }
                }
                else
                {
                    //No tile at "place"
                }
            }
        }
    }
}

#endif
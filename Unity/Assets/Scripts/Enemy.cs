using UnityEngine;
using System.Collections;

public class Enemy : GridPlayer
{
    public override string playerTypeName => gameObject.name;
    public float enemyTurnTime;

    public override void StartPlayer()
    {
        StartCoroutine(EnemyTurn());
    }

    public override void UpdatePlayer()
    {
        // Implement enemy-specific logic here
    }

    private IEnumerator EnemyTurn()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemyTurnTime);
            TakeTile();
        }
    }

    void TakeTile()
    {
        HexTile randomTile = null;
        HexTile randomNeighbor = null;
        int attempts = 50;

        for (int i = 0; i < attempts; i++)
        {
            if (ownedTiles.Count == 0)
            {
                break;
            }

            int randomOwnedTile = Random.Range(0, ownedTiles.Count);
            randomTile = ownedTiles[randomOwnedTile];

            // Check if the randomTile has valid neighbors
            if (randomTile.neighbors.Count == 0)
            {
                continue;
            }

            int randomNeighborTile = Random.Range(0, randomTile.neighbors.Count);
            randomNeighbor = randomTile.neighbors[randomNeighborTile];

            if (CanAddTile(randomNeighbor) && randomNeighbor.owner != this)
            {
                break;
            }
            else
            {
                randomNeighbor = null;
            }
        }

        // Check if a valid randomNeighbor was found before attempting to add it
        if (randomNeighbor != null)
        {
            CheckAndAddTile(randomNeighbor);
        }
    }
}

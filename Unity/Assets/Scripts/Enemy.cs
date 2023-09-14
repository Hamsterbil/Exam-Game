using UnityEngine;
using System.Collections;

public class Enemy : GridPlayer
{
    public override string playerTypeName => "Enemy";
    public Settings settings;

    public override void StartPlayer()
    {
        money = 10000;
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
            yield return new WaitForSeconds(settings.enemyTurnTime);
            addCellToEnemy();
        }
    }

    void addCellToEnemy()
    {
        HexCell randomCell = null;
        HexCell randomNeighbor = null;
        int attempts = 50;
        for (int i = 0; i < attempts; i++)
        {
            int randomOwnedCell = Random.Range(0, ownedTiles.Count);
            randomCell = ownedTiles[randomOwnedCell];
            int randomNeighborCell = Random.Range(0, randomCell.neighbors.Count);
            randomNeighbor = randomCell.neighbors[randomNeighborCell];

            if (CanAddCell(randomNeighbor))
            {
                break;
            }
            else
            {
                randomNeighbor = null;
            }
        }

        CheckAndAddCell(randomNeighbor);
    }
}

using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestSuite
{
    private Game game;

    [SetUp]
    public void Setup()
    {
        GameObject gameGameObject =
            MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
        game = gameGameObject.GetComponent<Game>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(game.gameObject);
    }

    // 1
    [UnityTest]
    public IEnumerator AsteroidsMoveDown()
    {
        // 3
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        // 4
        float initialYPos = asteroid.transform.position.y;
        // 5
        yield return new WaitForSeconds(0.1f);
        // 6
        Assert.Less(asteroid.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator GameOverOccursOnAsteroidCollision()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        //1
        asteroid.transform.position = game.GetShip().transform.position;
        //2
        yield return new WaitForSeconds(0.1f);

        //3
        Assert.True(game.isGameOver);
    }

    [UnityTest]
    public IEnumerator NewGameRestartsGame()
    {
        //1
        game.isGameOver = true;
        game.NewGame();
        //2
        Assert.False(game.isGameOver);
        yield return null;
    }

    [UnityTest]
    public IEnumerator LaserMovesUp()
    {
        // 1
        GameObject laser = game.GetShip().SpawnLaser();
        // 2
        float initialYPos = laser.transform.position.y;
        yield return new WaitForSeconds(0.1f);
        // 3
        Assert.Greater(laser.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator LaserDestroysAsteroid()
    {
        // 1
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        UnityEngine.Assertions.Assert.IsNull(asteroid);
    }

    [UnityTest]
    public IEnumerator DestroyedAsteroidRaisesScore()
    {
        // 1
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        Assert.AreEqual(game.score, 1);
    }

    [UnityTest]
    public IEnumerator StartNewSetsScoreZero()
    {
        // 1
        game.score = 99;
        game.isGameOver = true;
        game.NewGame();

        yield return new WaitForSeconds(0.01f);
        // 2
        Assert.AreEqual(game.score, 0);
    }

    [UnityTest]
    public IEnumerator MoveLeftAndRight()
    {
        // 1
        Ship ship = game.GetShip();
        // 2
        float initialXPos = ship.transform.position.x;
        ship.MoveRight();
        yield return new WaitForSeconds(0.1f);
        // 3
        Assert.Greater(ship.transform.position.x, initialXPos); // Move Right Works

        // 4
        initialXPos = ship.transform.position.x;
        ship.MoveLeft();
        yield return new WaitForSeconds(0.1f);
        // 5
        Assert.Greater(initialXPos, ship.transform.position.x); // Move Left Works
    }

    [UnityTest]

    public IEnumerator MoveUpAndDown()
    {

        Ship ship = game.GetShip();

        float initialYPosition = ship.transform.position.y;

        ship.MoveUp();
        yield return new WaitForSeconds(0.1f);
        // 3
        Assert.Greater(ship.transform.position.y, initialYPosition); // Move up works




        ship.MoveDown();
        yield return new WaitForSeconds(0.1f);
        // 3
        Assert.Less(ship.transform.position.y, initialYPosition); // Move Down works
    }


}

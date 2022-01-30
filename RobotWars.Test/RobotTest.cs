using System;
using Moq;
using NUnit.Framework;
using RobotWars.Domain;
using RobotWars.Interfaces;

namespace RobotWars.Test;

public class RobotTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    [TestCase("0,2,E", "MLMRMMMRMMRR", "4, 1, N", 0)]
    [TestCase("4,4,S", "LMLLMMLMMMRMM", "0, 1, W", 1)]
    [TestCase("2,2,W", "MLMLMLMRMRMRMRM", "2, 2, N", 0)]
    [TestCase("1,3,N", "MMLMMLMMMMM", "0, 0, S", 3)]
    public void CorrectInitialisation_ReturnsCorrectResult(string initialPosition, string movementInstructions, string expectedFinalPositin, int expectedPenalties)
    {
        var mockArena = new Mock<IArena>();
        mockArena.Setup(x => x.Height).Returns(5);
        mockArena.Setup(x => x.Width).Returns(5);

        Robot robot = new();
        robot.PutOnArena(mockArena.Object, initialPosition);

        foreach (char instruction in movementInstructions)
        {
            robot.SendMovementInstruction(instruction);
        }

        Assert.AreEqual(expectedFinalPositin, robot.GetCurrentLocation());
        Assert.AreEqual(expectedPenalties, robot.GetPenalties());
    }

    [Test]
    public void NoArena_GetCurrentLocation_ReturnCorrectMessage()
    {
        Robot robot = new();

        Assert.AreEqual("Robot is not on the arena", robot.GetCurrentLocation());
    }

    [Test]
    public void NoArena_SendMovementInstruction_ThrowException()
    {
        Robot robot = new();
        var ex = Assert.Throws<Exception>(() => robot.SendMovementInstruction('M'));

        Assert.AreEqual("Robot is not on arena", ex.Message);
    }
}
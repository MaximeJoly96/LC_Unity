using Engine.Movement;
using Engine.Movement.Moves;
using Movement;
using NUnit.Framework;

namespace Testing.Engine.Movement
{
    public class XmlMovementParserTests : XmlBaseParser
    {
        protected override string TestFilePath { get { return "Assets/Tests/Engine/Movement/TestData.xml"; } }

        [Test]
        public void ParseGetOnOffVehicleTest()
        {
            GetOnOffVehicle getOn = XmlMovementParser.ParseGetOnOffVehicle(GetDataToParse("GetOnOffVehicle"));

            Assert.NotNull(getOn);
        }

        [Test]
        public void ParseScrollMapTest()
        {
            ScrollMap scroll = XmlMovementParser.ParseScrollMap(GetDataToParse("ScrollMap"));

            Assert.AreEqual(3, scroll.X);
            Assert.AreEqual(-2, scroll.Y);
            Assert.AreEqual(5, scroll.Speed);
        }

        [Test]
        public void ParseTransferObjectTest()
        {
            TransferObject transfer = XmlMovementParser.ParseTransferObject(GetDataToParse("TransferObject"));

            Assert.AreEqual(TransferObject.PossibleDirection.Retain, transfer.Direction);
            Assert.AreEqual(5, transfer.X);
            Assert.AreEqual(3, transfer.Y);
            Assert.AreEqual(5, transfer.MapId);
            Assert.IsFalse(transfer.Inside);
        }

        [Test]
        public void ParseSetMoveRouteTest()
        {
            SetMoveRoute route = XmlMovementParser.ParseSetMoveRoute(GetDataToParse("SetMoveRoute"));

            Assert.AreEqual(true, route.RepeatAction);
            Assert.AreEqual(true, route.SkipIfCannotMove);
            Assert.AreEqual(false, route.WaitForCompletion);
            Assert.AreEqual("3", route.AgentId);

            MoveRelative moveRelative = route.Moves[0] as MoveRelative;
            Assert.AreEqual(3, moveRelative.DeltaX);
            Assert.AreEqual(-1, moveRelative.DeltaY);

            MoveTowards moveTowards = route.Moves[1] as MoveTowards;
            Assert.AreEqual("target", moveTowards.Target);
            Assert.AreEqual(0.5f, moveTowards.Distance);

            MoveAway moveAway = route.Moves[2] as MoveAway;
            Assert.AreEqual("target", moveAway.Target);
            Assert.AreEqual(0.8f, moveAway.Distance);

            StepForward stepForward = route.Moves[3] as StepForward;
            Assert.NotNull(stepForward);

            StepBackward stepBackward = route.Moves[4] as StepBackward;
            Assert.NotNull(stepBackward);

            JumpRelative jumpRelative = route.Moves[5] as JumpRelative;
            Assert.AreEqual(5, jumpRelative.DeltaX);
            Assert.AreEqual(6, jumpRelative.DeltaY);

            Wait wait = route.Moves[6] as Wait;
            Assert.AreEqual(6, wait.Duration);

            Turn turn = route.Moves[7] as Turn;
            Assert.AreEqual(Direction.Left, turn.Direction);

            TurnRelative turnRelative = route.Moves[8] as TurnRelative;
            Assert.AreEqual(45, turnRelative.Angle);

            TurnTowards turnTowards = route.Moves[9] as TurnTowards;
            Assert.AreEqual("target", turnTowards.Target);

            TurnAway turnAway = route.Moves[10] as TurnAway;
            Assert.AreEqual("target", turnAway.Target);

            ChangeSpeed changeSpeed = route.Moves[11] as ChangeSpeed;
            Assert.AreEqual(5, changeSpeed.Speed);

            WalkingAnimation walkingAnimation = route.Moves[12] as WalkingAnimation;
            Assert.AreEqual(true, walkingAnimation.On);

            DirectionFix directionFix = route.Moves[13] as DirectionFix;
            Assert.AreEqual(false, directionFix.On);

            Through through = route.Moves[14] as Through;
            Assert.AreEqual(false, through.On);

            Transparent transparent = route.Moves[15] as Transparent;
            Assert.AreEqual(true, transparent.On);

            ChangeOpacity changeOpacity = route.Moves[16] as ChangeOpacity;
            Assert.AreEqual(0.5f, changeOpacity.Alpha);
        }

        [Test]
        public void ParseCameraFollowPlayerTest()
        {
            CameraFollowPlayer follow = XmlMovementParser.ParseCameraFollowPlayer(GetDataToParse("CameraFollowPlayer"));

            Assert.IsTrue(follow.Follow);
        }

        [Test]
        public void ParseEnterBuildingTest()
        {
            EnterBuilding enter = XmlMovementParser.ParseEnterBuilding(GetDataToParse("EnterBuilding"));

            Assert.AreEqual("door", enter.AgentId);
        }
    }
}

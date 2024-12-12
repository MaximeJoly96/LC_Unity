using System.Xml;
using System;
using Engine.Movement.Moves;
using System.Globalization;
using Movement;
using System.Collections.Generic;

namespace Engine.Movement
{
    public static class XmlMovementParser
    {
        public static GetOnOffVehicle ParseGetOnOffVehicle(XmlNode data)
        {
            GetOnOffVehicle getOn = new GetOnOffVehicle();

            return getOn;
        }

        public static ScrollMap ParseScrollMap(XmlNode data)
        {
            ScrollMap scrollMap = new ScrollMap();

            scrollMap.X = float.Parse(data.Attributes["X"].InnerText, CultureInfo.InvariantCulture);
            scrollMap.Y = float.Parse(data.Attributes["Y"].InnerText, CultureInfo.InvariantCulture);
            scrollMap.Speed = float.Parse(data.Attributes["Speed"].InnerText, CultureInfo.InvariantCulture);

            return scrollMap;
        }

        public static TransferObject ParseTransferObject(XmlNode data)
        {
            TransferObject transfer = new TransferObject();

            transfer.Direction = (TransferObject.PossibleDirection)Enum.Parse(typeof(TransferObject.PossibleDirection), data.SelectSingleNode("Direction").InnerText);
            transfer.Target = data.Attributes["Target"].InnerText;

            XmlNode destination = data.SelectSingleNode("Destination");

            transfer.X = float.Parse(destination.Attributes["X"].InnerText, CultureInfo.InvariantCulture);
            transfer.Y = float.Parse(destination.Attributes["Y"].InnerText, CultureInfo.InvariantCulture);
            transfer.MapId = int.Parse(destination.Attributes["MapId"].InnerText);

            XmlAttribute insideAttribute = destination.Attributes["Inside"];

            if( insideAttribute != null)
                transfer.Inside = bool.Parse(destination.Attributes["Inside"].InnerText);

            return transfer;
        }

        public static SetMoveRoute ParseSetMoveRoute(XmlNode data)
        {
            SetMoveRoute route = new SetMoveRoute();

            route.RepeatAction = bool.Parse(data.Attributes["RepeatAction"].InnerText);
            route.SkipIfCannotMove = bool.Parse(data.Attributes["SkipIfCannotMove"].InnerText);
            route.WaitForCompletion = bool.Parse(data.Attributes["WaitForCompletion"].InnerText);
            route.AgentId = data.Attributes["AgentId"].InnerText;

            List<Move> moves = ParseMoves(data);
            foreach (Move m in moves)
                route.AddMove(m);

            return route;
        }

        public static List<Move> ParseMoves(XmlNode data)
        {
            List<Move> moves = new List<Move>();

            foreach (XmlNode node in data.ChildNodes)
            {
                string nodeName = node.Name;

                if (nodeName == typeof(MoveRelative).Name)
                {
                    MoveRelative moveRel = new MoveRelative();
                    moveRel.DeltaX = float.Parse(node.Attributes["DeltaX"].InnerText, CultureInfo.InvariantCulture);
                    moveRel.DeltaY = float.Parse(node.Attributes["DeltaY"].InnerText, CultureInfo.InvariantCulture);

                    moves.Add(moveRel);
                }
                else if (nodeName == typeof(MoveTowards).Name)
                {
                    MoveTowards moveTow = new MoveTowards();
                    moveTow.Target = node.Attributes["Target"].InnerText;
                    moveTow.Distance = float.Parse(node.Attributes["Distance"].InnerText, CultureInfo.InvariantCulture);

                    moves.Add(moveTow);
                }
                else if (nodeName == typeof(MoveAway).Name)
                {
                    MoveAway moveAway = new MoveAway();
                    moveAway.Target = node.Attributes["Target"].InnerText;
                    moveAway.Distance = float.Parse(node.Attributes["Distance"].InnerText, CultureInfo.InvariantCulture);

                    moves.Add(moveAway);
                }
                else if (nodeName == typeof(StepForward).Name)
                {
                    moves.Add(new StepForward());
                }
                else if (nodeName == typeof(StepBackward).Name)
                {
                    moves.Add(new StepBackward());
                }
                else if (nodeName == typeof(JumpRelative).Name)
                {
                    JumpRelative jump = new JumpRelative();
                    jump.DeltaX = float.Parse(node.Attributes["DeltaX"].InnerText, CultureInfo.InvariantCulture);
                    jump.DeltaY = float.Parse(node.Attributes["DeltaY"].InnerText, CultureInfo.InvariantCulture);

                    moves.Add(jump);
                }
                else if (nodeName == typeof(Wait).Name)
                {
                    Wait wait = new Wait();
                    wait.Duration = float.Parse(node.Attributes["Duration"].InnerText, CultureInfo.InvariantCulture);

                    moves.Add(wait);
                }
                else if (nodeName == typeof(Turn).Name)
                {
                    Turn turn = new Turn();
                    turn.Direction = (Direction)Enum.Parse(typeof(Direction), node.Attributes["Direction"].InnerText);

                    moves.Add(turn);
                }
                else if (nodeName == typeof(TurnRelative).Name)
                {
                    TurnRelative turnRel = new TurnRelative();
                    turnRel.Angle = int.Parse(node.Attributes["Angle"].InnerText);

                    moves.Add(turnRel);
                }
                else if (nodeName == typeof(TurnTowards).Name)
                {
                    TurnTowards turnTow = new TurnTowards();
                    turnTow.Target = node.Attributes["Target"].InnerText;

                    moves.Add(turnTow);
                }
                else if (nodeName == typeof(TurnAway).Name)
                {
                    TurnAway turnAway = new TurnAway();
                    turnAway.Target = node.Attributes["Target"].InnerText;

                    moves.Add(turnAway);
                }
                else if (nodeName == typeof(ChangeSpeed).Name)
                {
                    ChangeSpeed change = new ChangeSpeed();
                    change.Speed = float.Parse(node.Attributes["Speed"].InnerText, CultureInfo.InvariantCulture);

                    moves.Add(change);
                }
                else if (nodeName == typeof(WalkingAnimation).Name)
                {
                    WalkingAnimation walkingAnim = new WalkingAnimation();
                    walkingAnim.On = bool.Parse(node.Attributes["Status"].InnerText);

                    moves.Add(walkingAnim);
                }
                else if (nodeName == typeof(DirectionFix).Name)
                {
                    DirectionFix directionFix = new DirectionFix();
                    directionFix.On = bool.Parse(node.Attributes["Status"].InnerText);

                    moves.Add(directionFix);
                }
                else if (nodeName == typeof(Through).Name)
                {
                    Through through = new Through();
                    through.On = bool.Parse(node.Attributes["Status"].InnerText);

                    moves.Add(through);
                }
                else if (nodeName == typeof(Transparent).Name)
                {
                    Transparent transparent = new Transparent();
                    transparent.On = bool.Parse(node.Attributes["Status"].InnerText);

                    moves.Add(transparent);
                }
                else if (nodeName == typeof(ChangeOpacity).Name)
                {
                    ChangeOpacity change = new ChangeOpacity();
                    change.Alpha = float.Parse(node.Attributes["Alpha"].InnerText, CultureInfo.InvariantCulture);

                    moves.Add(change);
                }
                else if (nodeName == typeof(SetPosition).Name)
                {
                    SetPosition set = new SetPosition();
                    set.X = float.Parse(node.Attributes["X"].InnerText, CultureInfo.InvariantCulture);
                    set.Y = float.Parse(node.Attributes["Y"].InnerText, CultureInfo.InvariantCulture);

                    moves.Add(set);
                }
                else
                {
                    throw new InvalidOperationException("Unsupported Move for SetMoveRoute");
                }
            }

            return moves;
        }

        public static CameraFollowPlayer ParseCameraFollowPlayer(XmlNode node)
        {
            CameraFollowPlayer follow = new CameraFollowPlayer();

            follow.Follow = bool.Parse(node.Attributes["Follow"].InnerText);

            return follow;
        }

        public static ChangeFloor ParseChangeFloor(XmlNode node)
        {
            ChangeFloor change = new ChangeFloor();

            change.Up = bool.Parse(node.Attributes["Up"].InnerText);
            change.X = float.Parse(node.Attributes["X"].InnerText, CultureInfo.InvariantCulture);
            change.Y = float.Parse(node.Attributes["Y"].InnerText, CultureInfo.InvariantCulture);

            return change;
        }

        public static SetFloor ParseSetFloor(XmlNode node)
        {
            SetFloor set = new SetFloor();

            set.FieldId = int.Parse(node.Attributes["FieldId"].InnerText);
            set.FloorId = int.Parse(node.Attributes["FloorId"].InnerText);

            return set;
        }
    }
}

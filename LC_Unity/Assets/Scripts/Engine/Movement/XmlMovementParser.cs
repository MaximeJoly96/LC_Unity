﻿using System.Xml;
using System;
using Engine.Movement.Moves;

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

            scrollMap.X = int.Parse(data.Attributes["X"].InnerText);
            scrollMap.Y = int.Parse(data.Attributes["Y"].InnerText);
            scrollMap.Speed = int.Parse(data.Attributes["Speed"].InnerText);

            return scrollMap;
        }

        public static TransferObject ParseTransferObject(XmlNode data)
        {
            TransferObject transfer = new TransferObject();

            transfer.Direction = (TransferObject.PossibleDirection)Enum.Parse(typeof(TransferObject.PossibleDirection), data.SelectSingleNode("Direction").InnerText);
            transfer.Fade = (TransferObject.FadeType)Enum.Parse(typeof(TransferObject.FadeType), data.SelectSingleNode("Fade").InnerText);

            XmlNode destination = data.SelectSingleNode("Destination");

            transfer.X = int.Parse(destination.Attributes["X"].InnerText);
            transfer.Y = int.Parse(destination.Attributes["Y"].InnerText);
            transfer.MapId = int.Parse(destination.Attributes["MapId"].InnerText);

            return transfer;
        }

        public static SetMoveRoute ParseSetMoveRoute(XmlNode data)
        {
            SetMoveRoute route = new SetMoveRoute();

            route.RepeatAction = bool.Parse(data.Attributes["RepeatAction"].InnerText);
            route.SkipIfCannotMove = bool.Parse(data.Attributes["SkipIfCannotMove"].InnerText);
            route.WaitForCompletion = bool.Parse(data.Attributes["WaitForCompletion"].InnerText);

            foreach(XmlNode node in data.ChildNodes)
            {
                string nodeName = node.Name;

                if (nodeName == typeof(MoveRelative).Name)
                {
                    MoveRelative moveRel = new MoveRelative();
                    moveRel.X = int.Parse(node.Attributes["X"].InnerText);
                    moveRel.Y = int.Parse(node.Attributes["Y"].InnerText);

                    route.AddMove(moveRel);
                }
                else if(nodeName == typeof(MoveTowards).Name)
                {
                    MoveTowards moveTow = new MoveTowards();
                    moveTow.Target = node.Attributes["Target"].InnerText;
                    moveTow.Distance = int.Parse(node.Attributes["Distance"].InnerText);

                    route.AddMove(moveTow);
                }
                else if(nodeName == typeof(MoveAway).Name)
                {
                    MoveAway moveAway = new MoveAway();
                    moveAway.Target = node.Attributes["Target"].InnerText;
                    moveAway.Distance = int.Parse(node.Attributes["Distance"].InnerText);

                    route.AddMove(moveAway);
                }
                else if(nodeName == typeof(StepForward).Name)
                {
                    route.AddMove(new StepForward());
                }
                else if (nodeName == typeof(StepBackward).Name)
                {
                    route.AddMove(new StepBackward());
                }
                else if(nodeName == typeof(JumpRelative).Name)
                {
                    JumpRelative jump = new JumpRelative();
                    jump.X = int.Parse(node.Attributes["X"].InnerText);
                    jump.Y = int.Parse(node.Attributes["Y"].InnerText);

                    route.AddMove(jump);
                }
                else if(nodeName == typeof(Wait).Name)
                {
                    Wait wait = new Wait();
                    wait.Duration = int.Parse(node.Attributes["Duration"].InnerText);

                    route.AddMove(wait);
                }
                else if(nodeName == typeof(Turn).Name)
                {
                    Turn turn = new Turn();
                    turn.Direction = (Turn.PossibleDirection)Enum.Parse(typeof(Turn.PossibleDirection), node.Attributes["Direction"].InnerText);

                    route.AddMove(turn);
                }
                else if (nodeName == typeof(TurnRelative).Name)
                {
                    TurnRelative turnRel = new TurnRelative();
                    turnRel.Angle = int.Parse(node.Attributes["Angle"].InnerText);

                    route.AddMove(turnRel);
                }
                else if (nodeName == typeof(TurnTowards).Name)
                {
                    TurnTowards turnTow = new TurnTowards();
                    turnTow.Target = node.Attributes["Target"].InnerText;

                    route.AddMove(turnTow);
                }
                else if(nodeName == typeof(TurnAway).Name)
                {
                    TurnAway turnAway = new TurnAway();
                    turnAway.Target = node.Attributes["Target"].InnerText;

                    route.AddMove(turnAway);
                }
                else if(nodeName == typeof(ChangeSpeed).Name)
                {
                    ChangeSpeed change = new ChangeSpeed();
                    change.Speed = int.Parse(node.Attributes["Speed"].InnerText);

                    route.AddMove(change);
                }
                else if (nodeName == typeof(ChangeFrequency).Name)
                {
                    ChangeFrequency change = new ChangeFrequency();
                    change.Speed = int.Parse(node.Attributes["Speed"].InnerText);

                    route.AddMove(change);
                }
                else if(nodeName == typeof(WalkingAnimation).Name)
                {
                    WalkingAnimation walkingAnim = new WalkingAnimation();
                    walkingAnim.On = bool.Parse(node.Attributes["Status"].InnerText);

                    route.AddMove(walkingAnim);
                }
                else if (nodeName == typeof(DirectionFix).Name)
                {
                    DirectionFix directionFix = new DirectionFix();
                    directionFix.On = bool.Parse(node.Attributes["Status"].InnerText);

                    route.AddMove(directionFix);
                }
                else if (nodeName == typeof(Through).Name)
                {
                    Through through = new Through();
                    through.On = bool.Parse(node.Attributes["Status"].InnerText);

                    route.AddMove(through);
                }
                else if (nodeName == typeof(Transparent).Name)
                {
                    Transparent transparent = new Transparent();
                    transparent.On = bool.Parse(node.Attributes["Status"].InnerText);

                    route.AddMove(transparent);
                }
                else if(nodeName == typeof(ChangeOpacity).Name)
                {
                    ChangeOpacity change = new ChangeOpacity();
                    change.Alpha = float.Parse(node.Attributes["Alpha"].InnerText);

                    route.AddMove(change);
                }
                else
                {
                    throw new InvalidOperationException("Unsupported Move for SetMoveRoute");
                }
            }

            return route;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Pieces
{
    public class Pawn : Piece
    {
        private Color color;
        private bool partOfTopBoard;

        public Pawn()
        {
            Directions = new List<Point>
            {
                new Point { PosX = 1, PosY = -1 },
                new Point { PosX = -1, PosY = -1 },
                new Point { PosX = 0, PosY = -1 },
            };

            Image = @"Images\Pawn_B.png";
        }

        public override bool PartOfTopBoard
        { 
            get { return partOfTopBoard; }
            set
            {
                partOfTopBoard = value;
                if (partOfTopBoard)
                Directions = new List<Point>()
                {
                    new Point { PosX = 1, PosY = 1 },
                    new Point { PosX = -1, PosY = 1 },
                    new Point { PosX = 0, PosY = 1 },
                };
            }
        }

        public override Color Color
        {
            get { return color; }
            set 
            { 
                color = value;
                if (Color == Color.White)
                    Image = @"Images\Pawn_W.png";
            }
        }

        public override bool CanBeMovedToSquare(Square end)
        {
            var dir = ChooseRightDirection(end.Point);

            if (dir == null) return false;

            if (dir.PosX != 0 && Point.GoToDirection(dir).Equals(end.Point) && end.Piece != null)
                return true;

            var points = Point.AllMovesWithinDirection(end.Point, dir);
            if (dir.PosX == 0 && (points.Count == 1 ^ (points.Count == 2 && IsFirstMove)) && end.Piece == null)
                return true;

            return false;
        }

        public override object Clone()
        {
            return new Pawn()
            {
                Color = Color,
                Image = Image,
                Point = Point.Clone() as Point
            };
        }
    }
}

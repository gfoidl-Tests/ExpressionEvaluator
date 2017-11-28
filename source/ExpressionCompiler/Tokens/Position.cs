using System;

namespace ExpressionCompiler.Tokens
{
    public struct Position : IEquatable<Position>
    {
        public int Start { get; }
        public int End   { get; }
        //---------------------------------------------------------------------
        public Position(int start, int end)
        {
            this.Start = start;
            this.End   = end;
        }
        //---------------------------------------------------------------------
        public static implicit operator Position((int Start, int End) p)
            => new Position(p.Start, p.End);
        //---------------------------------------------------------------------
        public static readonly Position NotDefined = new Position(-1, -1);
        //---------------------------------------------------------------------
        public override string ToString()
        {
            if (this.Start == this.End)
                return this.End.ToString();

            return $"(start: {this.Start}, end: {this.End})";
        }
        //---------------------------------------------------------------------
        public bool Equals(Position other) 
            => this.Start == other.Start 
            && this.End   == other.End;
        //---------------------------------------------------------------------
        public override bool Equals(object obj)
        {
            if (obj is Position other) return this.Equals(other);

            return false;
        }
        //---------------------------------------------------------------------
        public override int GetHashCode() => this.Start.GetHashCode();
        //---------------------------------------------------------------------
        public static bool operator ==(Position p1, Position p2) =>  p1.Equals(p2);
        public static bool operator !=(Position p1, Position p2) => !p1.Equals(p2);
    }
}
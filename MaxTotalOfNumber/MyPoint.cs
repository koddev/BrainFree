using System;

namespace MaxTotalOfNumber;

public struct MyPoint
{
 
     public MyPoint(int x, int y)
     {
         X = x;
         Y = y;
     }

     public int X { readonly get; private set; } = -1;
     public int Y { readonly get; private set; } = -1;


     public static MyPoint Empty = new MyPoint(-1, -1);
     public bool IsValidPoint()
     {
         return X>=0 && Y>=0 && X<=7 && Y<=7;
     }
     public static MyPoint operator +(MyPoint p1, MyPoint p2)
     {
         return new MyPoint(p1.X + p2.X, p1.Y + p2.Y);

     } 
     public static MyPoint operator -(MyPoint p1, MyPoint p2)
     {
         return new MyPoint(p1.X - p2.X, p1.Y  - p2.Y);

     }

     public static bool operator ==(MyPoint p1, MyPoint p2)
     {
         return p1.X==p2.X && p1.Y==p2.Y;
     }
     public static bool operator !=(MyPoint p1, MyPoint p2)
     {
         return !(p1 == p2);
     }

     public override string ToString()
     {
         return $"{X},{Y}";
     }

   
}

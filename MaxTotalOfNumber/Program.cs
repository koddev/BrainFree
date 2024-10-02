namespace MaxTotalOfNumber;

  internal class Program
  {

      static int[] sayilar = new int[64] {
      3,7,4,18,17,17,10,1,10,13,11,5,14,13,14,17,14,6,12,12,9,20,1,2,6,16,5,11,15,16,14,4,6,2,20,18,13,16,12,9,19,3,3,7,7,15,19,20,1,18,15,9,4,8,11,8,19,8,13,2,10,12,2,9
      };

      static readonly int[,] matris = new int[8, 8];

      static readonly int MaxStep = 21;
      public static void Main()
      {

          
      
          for (int i = 0; i < 8; i++)
              for (int j = 0; j < 8; j++)
                  matris[i, j] = sayilar[8 * i + j % 8];
   
         
          var resultPoints =new  List<(int,List<List<MyPoint>>)>(); 

          for (int i = 0; i < 8; i++)
              for (int j = 0; j < 8; j++)
              {
                  var usePoints = new List<MyPoint>();
                  var currentPoint = new MyPoint(i, j);
                  var startPoint = new MyPoint(i, j);
                  usePoints.Add(startPoint);
                  var result = new List<List<MyPoint>>();

                  MoveAll(usePoints, currentPoint, startPoint, ref result);

                  var maxVal = result.Select(s => s.Sum(t => matris[t.X, t.Y])).Max(); 
                  
                  resultPoints.Add((maxVal, result.Where(w => w.Sum(t => matris[t.X, t.Y]) == maxVal).ToList()));
              }

          
          var maxValLast = resultPoints.Max(m => m.Item1);
          var resultPointsLast = resultPoints.Where(w=>w.Item1 == maxValLast).SelectMany(m => m.Item2).ToList();
          var resultNumbersLast = resultPointsLast.Select(s => s.Select(s2 => matris[s2.X, s2.Y])).ToList();
     
      }

      static void MoveAll(List<MyPoint> usePoints, MyPoint currentPoint, MyPoint startPoint,ref List<List<MyPoint>> result)
      {
          var useNumber = GetMatrisNumber(usePoints);
          var currentStep  = usePoints.Count;
          var endPointStep = GetEndpointStep(currentPoint, startPoint);
          var IsReturnStartPoint = currentStep + endPointStep >= (MaxStep-1);

          var closedPoints = IsReturnStartPoint ? GetClosedPoints(currentPoint,startPoint) : GetClosedPoints(currentPoint,MyPoint.Empty);

          var matrisBit = GetMatrisBit(useNumber);

          if (currentStep > 2 )
          {
              if(endPointStep==0)
              {
                  result.Add(usePoints);
                  if(closedPoints.Count==1 && closedPoints.FirstOrDefault() == startPoint) return;                     
              }                
          }

          foreach (var point in closedPoints)
          {
              if (matrisBit[point.X, point.Y]) continue;

              var myUsePoints = usePoints.ToList();
              myUsePoints.Add(point);

              MoveAll(myUsePoints, point, startPoint, ref result);

          }
         
      }

      static List<int> GetMatrisNumber(List<MyPoint> usePoints)
      {
          return usePoints.Select(s => matris[s.X, s.Y]).ToList();            
      }

      static bool[,] GetMatrisBit(List<int> useNumber)
      {
          bool[,] tmpMatris = new bool[8, 8];

          for (int i = 0; i < 8; i++)
              for (int j = 0; j < 8; j++)
                  tmpMatris[i, j] = useNumber.Contains(matris[i,j]) ? true:false;

          return tmpMatris;
      }



      static MyPoint[] direction =
          [
              new MyPoint(-1, 0),
              new MyPoint(1, 0),
              new MyPoint(0, -1),
              new MyPoint(0, 1),
          ];


      static int GetEndpointStep(MyPoint currentPoint,MyPoint endPoint)
      {
          int step = 0;

          var points = GetClosedPoints(currentPoint, endPoint);
          while (points.Any() && !points.Contains(endPoint))
          {
              step++;
              points = GetClosedPoints(points[0], endPoint);
          }
          return step;
        
      }

      static List<MyPoint> GetClosedPoints(MyPoint p, MyPoint startPoint)
      {

          var points =new List<MyPoint>(4)
                   {
                         p + direction[0],
                         p + direction[1],
                         p + direction[2],
                         p + direction[3]
                   } .Where(w => w.IsValidPoint())
                   .ToList() ;




          if (startPoint.IsValidPoint())
          {
              var points2 = new List<MyPoint>();

              if (p == startPoint)
              {

                  points2.Add(p);
              }
              else
              {
                  if (startPoint.X != p.X)
                  {
                      var tmpPoint = startPoint.X < p.X ? direction[0] : direction[1];
                      points2.Add(p + tmpPoint);
                  }
                  
                  if (startPoint.Y != p.Y)
                  {
                      var tmpPoint = startPoint.Y < p.Y ? direction[2] : direction[3];
                      points2.Add(p + tmpPoint);
                  }
              }


              return points2 ;
          }
          else
          {
              return points ;
          }


      }


  }

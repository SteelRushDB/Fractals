using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Fractals;

public class FractalSerpinskyCarpet: FractalBase
{
    public FractalSerpinskyCarpet(Canvas canvas, double recursionLevel, Color startColor, Color endColor) 
        : base(canvas, recursionLevel, startColor, endColor)
    {
        
    }

    public override void DrawFractal()
    {
        double centerX = _canvas.ActualWidth / 2;
        double centerY = _canvas.ActualHeight / 2;
    
        // Начальное положение и размер квадрата
        double initialSize = Math.Min(centerX, centerY) * 1; // Размер исходного квадрата
        double startX = centerX - initialSize / 2;
        double startY = centerY - initialSize / 2;
        
        Recursion(startX, startY, initialSize, _recursionLevel);
    }



    private void Recursion(double x, double y, double sideLength, double recursionLevel)
    {
        double sl = sideLength / 3;
        DrawFilledSquare(x+1*sl, y+1*sl, sl, recursionLevel);

        if (recursionLevel > 0)
        {
            Recursion(x+0*sl, y+0*sl, sl,recursionLevel-1);
            Recursion(x+1*sl, y+0*sl, sl,recursionLevel-1);
            Recursion(x+2*sl, y+0*sl, sl,recursionLevel-1);
            
            Recursion(x+0*sl, y+1*sl, sl,recursionLevel-1);
            // Пустой квадрат посередине
            Recursion(x+2*sl, y+1*sl, sl,recursionLevel-1);
            
            Recursion(x+0*sl, y+2*sl, sl,recursionLevel-1);
            Recursion(x+1*sl, y+2*sl, sl,recursionLevel-1);
            Recursion(x+2*sl, y+2*sl, sl,recursionLevel-1);
        }
    }
    
}
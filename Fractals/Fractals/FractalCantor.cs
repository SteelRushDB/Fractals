using System.Windows.Controls;
using System.Windows.Media;

namespace Fractals;

public class FractalCantor : FractalBase
{
    private double _recursionDistance;
    public FractalCantor(Canvas canvas, double recursionLevel, double recursionDistance, Color startColor, Color endColor) 
        : base(canvas, recursionLevel, startColor, endColor)
    {
        _recursionDistance = recursionDistance;
    }
    
    public override void DrawFractal()
    {
        double centerX = _canvas.ActualWidth / 2;
        double centerY = _canvas.ActualHeight / 2;
        
        Recursion(centerX - centerX/2, centerX + centerX/2, centerY, centerY, _recursionLevel);
    }

    public void Recursion(double x1, double x2, double y1, double y2, double recursionLevel)
    {
        DrawSimpleLine(x1, x2, y1, y2, 10, recursionLevel);

        if (recursionLevel == 0) return;

        double dx = (x2 - x1) / 3;
        Recursion(x1, x1+dx, y1 + _recursionDistance, y2 + _recursionDistance, recursionLevel - 1 );
        Recursion(x1+2*dx, x2, y1 + _recursionDistance, y2 + _recursionDistance, recursionLevel - 1 );
    }
}
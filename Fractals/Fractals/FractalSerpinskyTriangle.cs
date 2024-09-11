using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Fractals;

public class FractalSerpinskyTriangle : FractalBase
{
    public FractalSerpinskyTriangle(Canvas canvas, double recursionLevel, Color startColor, Color endColor) 
        : base(canvas, recursionLevel, startColor, endColor)
    {
        
    }
    
    public override void DrawFractal()
    {
        // Получаем центр Canvas
        double centerX = _canvas.ActualWidth / 2;
        double centerY = _canvas.ActualHeight / 2;

        // Вычисляем высоту равностороннего треугольника
        double height = (Math.Sqrt(3) / 2) * centerX;

        // Координаты вершин треугольника
        double x1 = centerX - centerX / 2;  // Левая вершина
        double y1 = centerY + height / 2;

        double x2 = centerX + centerX / 2;  // Правая вершина
        double y2 = centerY + height / 2;

        double x3 = centerX;                   // Верхняя вершина
        double y3 = centerY - height / 2;

        // Рисуем треугольник
        Recursion(x1, y1, x2, y2, x3, y3, _recursionLevel);
    }
    
    
    private void Recursion(double x1, double y1, double x2, double y2, double x3, double y3, double recursionLevel)
    {
        DrawEmptyTriangle(x1, y1, x2, y2, x3, y3, recursionLevel);
        
        if (recursionLevel > 0)
        {
            Recursion(x1, y1, (x1+x2)/2, (y1+y2)/2,(x1+x3)/2, (y1+y3)/2, recursionLevel-1 );
            Recursion((x1+x2)/2, (y1+y2)/2, x2, y2,(x2+x3)/2, (y2+y3)/2, recursionLevel-1 );
            Recursion((x1+x3)/2, (y1+y3)/2, (x2+x3)/2, (y2+y3)/2,x3, y3, recursionLevel-1 );
        }
    }
}
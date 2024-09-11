using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Fractals;

public class FractalKoch: FractalBase
{
    public FractalKoch(Canvas canvas, double recursionLevel, Color startColor, Color endColor)
        : base(canvas, recursionLevel, startColor, endColor)
    {
        
    }

    public override void DrawFractal()
    {
        double centerX = _canvas.ActualWidth / 2;
        double centerY = _canvas.ActualHeight / 2;
        
        Recursion(centerX/4, centerX*1.75, centerY*1.5, centerY*1.5, _recursionLevel);
        
    }

    private void Recursion(double x1, double x2, double y1, double y2, double recursionLevel)
    {
        DrawSimpleLineUnder(x1, x2, y1, y2, 1, recursionLevel);
        
        if (recursionLevel > 0)
        {
            // Шаг 1: делим линию на три равные части
            double dx = (x2 - x1) / 3.0;
            double dy = (y2 - y1) / 3.0;

            double xA = x1 + dx; // Точка 1/3
            double yA = y1 + dy;

            double xB = x1 + 2 * dx; // Точка 2/3
            double yB = y1 + 2 * dy;

            // Шаг 2: вычисляем координаты вершины треугольника
            double angle = -Math.PI / 3; // 60 градусов в радианах
            double xPeak = xA + (dx * Math.Cos(angle) - dy * Math.Sin(angle));
            double yPeak = yA + (dx * Math.Sin(angle) + dy * Math.Cos(angle));
            

            DrawSimpleWhiteLine(xA,xB,yA,yB, 3);
            // Шаг 3: рекурсивно рисуем четыре линии
            Recursion(x1, xA, y1, yA, recursionLevel - 1); // Первая часть
            Recursion(xA, xPeak, yA , yPeak, recursionLevel - 1); // Восходящая линия
            Recursion(xPeak, xB,yPeak, yB, recursionLevel - 1); // Спускающаяся линия
            Recursion(xB, x2, yB, y2, recursionLevel - 1); // Последняя часть
        }
        
    }
    
    public void DrawSimpleWhiteLine(double x1, double x2, double y1, double y2, double thinkess)
    {
        Line oneLine = new Line
        {
            X1 = x1,
            Y1 = y1,
            X2 = x2,
            Y2 = y2,
            Stroke = Brushes.White,
            StrokeThickness = thinkess
        };
        
        _canvas.Children.Add( oneLine);
    }
    
    public void DrawSimpleLineUnder(double x1, double x2, double y1, double y2, double thinkess, double recursionLevel)
    {
        Line oneLine = new Line
        {
            X1 = x1,
            Y1 = y1,
            X2 = x2,
            Y2 = y2,
            Stroke = GetGradientBrush(recursionLevel),
            StrokeThickness = thinkess
        };
        
        _canvas.Children.Insert(0, oneLine);
    }
    
    
    
    
    private void DrawLineTest(double x1, double x2, double y1, double y2, double recursionLevel)
    {
        if (recursionLevel == 0)
        {
            // Базовый случай: рисуем прямую линию
            DrawSimpleLine(x1, x2, y1, y2, 1, recursionLevel);
        }
        else
        {
            // Шаг 1: делим линию на три равные части
            double dx = (x2 - x1) / 3.0;
            double dy = (y2 - y1) / 3.0;

            double xA = x1 + dx; // Точка 1/3
            double yA = y1 + dy;

            double xB = x1 + 2 * dx; // Точка 2/3
            double yB = y1 + 2 * dy;

            // Шаг 2: вычисляем координаты вершины треугольника
            double angle = -Math.PI / 3; // 60 градусов в радианах
            double xPeak = xA + (dx * Math.Cos(angle) - dy * Math.Sin(angle));
            double yPeak = yA + (dx * Math.Sin(angle) + dy * Math.Cos(angle));


            // Шаг 3: рекурсивно рисуем четыре линии
            Recursion(x1, xA, y1, yA, recursionLevel - 1); // Первая часть
            Recursion(xA, xPeak, yA , yPeak, recursionLevel - 1); // Восходящая линия
            Recursion(xPeak, xB,yPeak, yB, recursionLevel - 1); // Спускающаяся линия
            Recursion(xB, x2, yB, y2, recursionLevel - 1); // Последняя часть
        }
    }
}
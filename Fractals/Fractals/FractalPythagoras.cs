using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Fractals;

public class FractalPythagoras: FractalBase
{
    private double _iterationCompression;
    private double _leftPartDegree;
    private double _rightPartDegree;
    
    public FractalPythagoras(Canvas canvas, double recursionLevel, double iterationCompression, double leftPartDegree, double rightPartDegree, 
        Color startColor, Color endColor)
        : base(canvas, recursionLevel, startColor, endColor)
    {
        _iterationCompression = iterationCompression;
        _leftPartDegree = leftPartDegree;
        _rightPartDegree = rightPartDegree;
    }
    

    public override void DrawFractal()
    {
        double centerX = _canvas.ActualWidth / 2;
        double centerY = _canvas.ActualHeight / 2;
        
        Recursion(centerX, centerX, centerY * 1.75, centerY*1.25, _recursionLevel);
    }

    private void Recursion(double x1, double x2, double y1, double y2, double recursionLevel)
    {
        DrawSimpleLine(x1, x2, y1, y2, 1, recursionLevel);
        
        if (recursionLevel > 0)
        {
            // Вычисляем длину текущей линии
            double lineLength = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2)) * _iterationCompression;

            // Угол текущей линии (угол между горизонталью и линией)
            double currentAngle = Math.Atan2(y2 - y1, x2 - x1);

            // Левый угол
            double leftAngle = currentAngle - Math.PI * _leftPartDegree / 180.0; // Конвертация углов в радианы
            double leftX2 = x2 + lineLength * Math.Cos(leftAngle); // Новая координата X для левой линии
            double leftY2 = y2 + lineLength * Math.Sin(leftAngle); // Новая координата Y для левой линии

            // Правый угол
            double rightAngle = currentAngle + Math.PI * _rightPartDegree / 180.0;
            double rightX2 = x2 + lineLength * Math.Cos(rightAngle); // Новая координата X для правой линии
            double rightY2 = y2 + lineLength * Math.Sin(rightAngle); // Новая координата Y для правой линии

            // Рекурсивно рисуем следующие линии
            Recursion(x2, leftX2, y2, leftY2, recursionLevel - 1);
            Recursion(x2, rightX2, y2, rightY2, recursionLevel - 1);
        }

    }
    
}
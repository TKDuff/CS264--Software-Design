public abstract class Shape
{
    public string svgLine;
    public string info;
}

//All shapes that take 4 parameters
public class Rectangle : Shape
{
    public Rectangle(int num1, int num2, int num3, int num4)
    {
        svgLine = $"<rect x=\"{num1}\" y=\"{num2}\" width=\"{num3}\" height=\"{num4}\" />";
        info = $"Rectangle (x={num1} y={num2} width={num3} height={num4})";
    }
}

public class Ellipse : Shape
{
    public Ellipse(int num1, int num2, int num3, int num4)
    {
        svgLine = $"<ellipse cx=\" {num1} \" cy=\" {num2} \" rx=\" {num3} \" ry=\" {num4} \" />";
        info = $"Ellipse (cx={num1}, cy={num2}, rx={num3}, cy={num4} )";
    }
}

public class Line : Shape
{    
    public Line(int num1, int num2, int num3, int num4)
    {
        svgLine = $"<line x1=\"{num1}\" x2=\" {num2} \" y1=\" {num3} \" y2=\" {num4}\" style=\"stroke:" + "black" + ";fill:" + "grey" + ";stroke-width:" + 2 +"\" />";;
        info = $"Line (x1={num1}, x2={num2}, y1={num3}, y2={num4} )";
    }   
}

public class Path : Shape
{    
    public Path(int num1, int num2, int num3, int num4)
    {
        svgLine = $"<path d=\"M{num1} 100 L{num2} 200 L{num3} {num4} Z\" />";
        info = $"Path (d=M{num1} 100 L{num2} 200 L{num3} {num4} Z) ";
    }   
}

//Circle takes 3 parameters
public class Circle : Shape
{    
    public Circle(int cx, int cy, int r)
    {
        svgLine = $"<Circle cx=\"{cx}\" cy=\"{cy}\" r=\"{r}\" />";
        info = $"Circle (x={cx} y={cy} radius={r})";
    }   
}

//Polygone and PolyLine take one parameter
public class Polygon : Shape
{    
    public Polygon(string points)
    {
        this.svgLine = $"<polygon points= {points}  style=\"stroke:  purple  ;fill:lime;stroke-width:1\" />";
        this.info = $"Polygon (points={points})";
    }   
}

public class Polyline : Shape
{    
    public Polyline(string points)
    {
        svgLine = $"<polyline points= {points}  style=\"stroke:black;fill:none ;stroke-width:4\" />";
        info = $"Polyline (points={points})";
    }   
}
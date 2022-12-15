using static System.Console;
using System;
using System.Collections.Generic;
/*
Abstract class "Shape" is inherited by each shape below, while Shape contains all fields, the inherited shape only makes use of the fields relavent to it. 
Methods:
        svgLine() - returns svg code for shape, using the shapes field value
        info() - returns string about shapes z-index and field values
        update() - Updates shape, asks user to select which field to change and allows them to enter the new value

All shape subclasses below use these methods, I only commented the rectangle methods as they are implemented pretty much the same for the rest

 */

 //create style class
 //use factory to get style, don't use new
public abstract class Shape
{
    public abstract string svgLine();
    public abstract string info();
    public abstract void update();


    public int xPos {get; set;}
    public int yPos {get; set;}
    public static int ListzIndex {get; set;} = 0; //global variable to keep track of how many shapes on stack


    public int zIndex {get; set;}

    //Rectangle
    public int width {get; set;}
    public int height {get; set;}

    //Circle and Ellipse
    public int rx {get; set;}   //single field used for circle radius and ellipse rx value
    public int ry {get; set;} 

    //Line
    public int x1 {get; set;}
    public int x2 {get; set;}
    public int y1 {get; set;}
    public int y2 {get; set;}


    //Polygone
    public string points {get; set;}

    //Path
    public string path {get; set;}
}

public class Rectangle : Shape
{
    public Rectangle(){
        
        Write("Enter the x position: ");
        this.xPos = Convert.ToInt32(ReadLine());
        Write("Enter the y position: ");
        this.yPos = Convert.ToInt32(ReadLine());
        Write("Enter the height : ");
        this.height = Convert.ToInt32(ReadLine());
        Write("Enter the width : ");
        this.width = Convert.ToInt32(ReadLine());
        this.zIndex = ListzIndex;
        ListzIndex++;
    }

    public override void update() {
        WriteLine("Enter what you want to update (x,y,height,width): ");    //ask the user which filed they would like to change
        string input = ReadLine();
        //switch cases which will ask user to enter new value depending on which field they choose to change
        switch (input)
        {
            case "x":
                Write("Enter the new x position: ");
                this.xPos = Convert.ToInt32(ReadLine());
                break;
            case "y":
                Write("Enter the new y position: ");
                this.yPos = Convert.ToInt32(ReadLine());
                break;
            case "height":
                Write("Enter the new height: ");
                this.height = Convert.ToInt32(ReadLine());
                break;
            case "width":
                Write("Enter the new with: ");
                this.width = Convert.ToInt32(ReadLine());
                break;
            default:
                break;
        }
    }   
    //svgLine() returns svg code, using fields entered by the user, for html file so shape 
    public override string svgLine() => "<rect x=\"" + xPos + "\" y=\"" + yPos+ "\" width=\"" + width + "\" height=\"" + height;

    //info() returns information about the shape object, being its z-index and field values
    public override string info() => "\nAt z-index " + zIndex + " - Rectangle:\n\txPos: " + xPos +"\n\tyPos: " + yPos +"\n\twidth: " + width + "\n\theight: " + height;    
}

public class Circle : Shape
{
    public Circle(){
        Write("Enter the x position: ");
        this.xPos = Convert.ToInt32(ReadLine());
        Write("Enter the y position: ");
        this.yPos = Convert.ToInt32(ReadLine());
        Write("Enter the radius : ");
        this.rx = Convert.ToInt32(ReadLine());
        this.zIndex = ListzIndex;
        ListzIndex++;
    }


    public override void update() {
        WriteLine("Enter what you want to update (x,y,rx): ");
        string input = ReadLine();
        switch (input)
        {
            case "x":
                Write("Enter the new x position: ");
                this.xPos = Convert.ToInt32(ReadLine());
                break;
            case "y":
                Write("Enter the new y position: ");
                this.yPos = Convert.ToInt32(ReadLine());
                break;
            case "rx":
                Write("Enter the new radius: ");
                this.height = Convert.ToInt32(ReadLine());
                break;
            default:
                break;
        }
    }

    public override string svgLine() => "<circle cx=\"" + xPos + "\" cy=\"" + yPos+ "\" r=\"" + rx;
    public override string info() => "\nAt z-index " + zIndex + " - Circle:\n\txPos: " + xPos +"\n\tyPos: " + yPos +"\n\tradius: " + rx;    
}

public class Ellipse : Shape
{
    public Ellipse(){
        Write("Enter the x position: ");
        this.xPos = Convert.ToInt32(ReadLine());
        Write("Enter the y position: ");
        this.yPos = Convert.ToInt32(ReadLine());
        Write("Enter the rx radius : ");
        this.rx = Convert.ToInt32(ReadLine());
        Write("Enter the ry radius : ");
        this.ry = Convert.ToInt32(ReadLine());
        this.zIndex = ListzIndex;
        ListzIndex++;

    }

    public override void update() {
        WriteLine("Enter what you want to update (x,y,rx,ry): ");
        string input = ReadLine();
        switch (input)
        {
            case "x":
                Write("Enter the new x position: ");
                this.xPos = Convert.ToInt32(ReadLine());
                break;
            case "y":
                Write("Enter the new y position: ");
                this.yPos = Convert.ToInt32(ReadLine());
                break;
            case "rx":
                Write("Enter the new rx radius: ");
                this.rx = Convert.ToInt32(ReadLine());
                break;
            case "ry":
                Write("Enter the new ry radius: ");
                this.ry = Convert.ToInt32(ReadLine());
                break;
            default:
                break;
        }
    }

    public override string svgLine() => "<ellipse cx=\"" + xPos + "\" cy=\"" + yPos+ "\" rx=\"" + rx + "\" ry=\"" + ry;
    public override string info() => "\nAt z-index " + zIndex + " - Ellipse:\n\txPos: " + xPos +"\n\tyPos: " + yPos +"\n\trx: " + rx + "\n\try:" + ry;    
}


public class Line : Shape
{
    public Line(){
        Write("Enter the x1 position: ");
        this.x1 = Convert.ToInt32(ReadLine());
        Write("Enter the y1 position: ");
        this.y1 = Convert.ToInt32(ReadLine());
        Write("Enter the x2 radius : ");
        this.x2 = Convert.ToInt32(ReadLine());
        Write("Enter the y2 radius : ");
        this.y2 = Convert.ToInt32(ReadLine());
        this.zIndex = ListzIndex;
        ListzIndex++;
    }

    public override void update() {
        WriteLine("Enter what you want to update (x1,y1,x2,y2): ");
        string input = ReadLine();
        switch (input)
        {
            case "x1":
                Write("Enter the new x position: ");
                this.x1 = Convert.ToInt32(ReadLine());
                break;
            case "y1":
                Write("Enter the new y position: ");
                this.y1 = Convert.ToInt32(ReadLine());
                break;
            case "x2":
                Write("Enter the new rx radius: ");
                this.x2 = Convert.ToInt32(ReadLine());
                break;
            case "y2":
                Write("Enter the new ry radius: ");
                this.y2 = Convert.ToInt32(ReadLine());
                break;
            default:
                break;
        }
    }

    public override string svgLine() => "<line x1=\"" + x1 + "\" x2=\"" + x2 +  "\" y1=\"" + y1 + "\" y2=\"" + y2;
    public override string info() => "\nAt z-index " + zIndex + " - Line:\n\txPos: " + x1 +"\n\tyPos: " + y1 +"\n\tx2: " + x2 +"\n\ty2: " + y2;    
}

public class Polygon : Shape
{
    public Polygon(){
        //this.points = points;
        Write("Enter the points");
        this.points = ReadLine();
        this.zIndex = ListzIndex;
        ListzIndex++;
    }

    public override void update() {
        WriteLine("Enter the new points: ");
        this.points = ReadLine();
        
    }

    public override string svgLine() => "<polygon points=\"" + points;
    public override string info() => "\nAt z-index " + zIndex + " - Polygon :\n\tPoints: " + points; 
}

public class Polyline : Shape
{
    public Polyline(){
        Write("Enter the points");
        this.points = ReadLine();
        this.zIndex = ListzIndex;
        ListzIndex++;
    }

    public override void update() {
        WriteLine("Enter the new points: ");
        this.points = ReadLine(); 
    }

    public override string svgLine() => "<polyline points=\"" + points;
    public override string info() => "\nAt z-index " + zIndex + " - Polyline :\n\tPoints: " + points; 
}

public class Path : Shape
{
    public Path(string path){
        Write("Enter the points");
        this.points = ReadLine();
        this.zIndex = ListzIndex;
        ListzIndex++;
    }

    public override void update() {
        WriteLine("Enter the new points: ");
        this.points = ReadLine(); 
    }

    public override string svgLine() => "<path d=\"" + path + "\" />";
    public override string info() => "At z-index " + zIndex + " - Path :\n\tPath: " + path; 
}

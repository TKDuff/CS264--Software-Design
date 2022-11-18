using static System.Console;
using System;
using System.Collections.Generic;

public class ShapeOriginator{

    private string svgLine; //shapes svg code to be put into html file
    private string info;    //shapes information, used to be printed to console

    //Rectangle, ellipse and line all take four integer paramters, use switch case to determine which one to creat in set method
    //set method takes parameters from program, determine shape to create, fill in paramters to svg code and information lines
    public void set(string type, int num1, int num2, int num3, int num4){
        switch(type)
        {
            case "rect":
                this.svgLine = $"<rect x=\"{num1}\" y=\"{num2}\" width=\"{num3}\" height=\"{num4}\" />";
                this.info = $"Rectangle (x={num1} y={num2} width={num3} height={num4})";
                break;
            case "ellipse":
                this.svgLine = $"<ellipse cx=\" {num1} \" cy=\" {num2} \" rx=\" {num3} \" ry=\" {num4} \" />";
                this.info = $"Ellipse (cx={num1}, cy={num2}, rx={num3}, cy={num4} )";
                break;
            case "line":
                this.svgLine = $"<line x1=\"{num1}\" x2=\" {num2} \" y1=\" {num3} \" y2=\" {num4}\" style=\"stroke:" + "black" + ";fill:" + "grey" + ";stroke-width:" + 2 +"\" />";;
                this.info = $"Line (x1={num1}, x2={num2}, y1={num3}, y2={num4} )";
                break;
            case "path":
                this.svgLine = $"<path d=\"M{num1} 100 L{num2} 200 L{num3} 200 Z\" />";
                this.info = $"<Path (d=M{num1} 100 L{num2} 200 L{num3} {num4} Z) ";
                break;
        }
        
        Console.WriteLine(info + " added to the canvas");
    }

    //Circle takes in 3 number
    public void set(string type, int num1, int num2, int num3){
        this.svgLine = $"<{type} cx=\"{num1}\" cy=\"{num2}\" r=\"{num3}\" />";
        this.info = $"{type} (x={num1} y={num2} radius={num3})";
        WriteLine(info + " added to the canvas");
    }

    //Polygone, PolyLine take in one paramter, just a set of points
    public void set(string type, string points){
        switch(type)
        {
            case "polygon":
            this.svgLine = $"<polygon points= {points}  style=\"stroke:  purple  ;fill:lime;stroke-width:1\" />";
            this.info = $"Polygon (points={points})";
            break;
            case "polyline":
            this.svgLine = $"<polyline points= {points}  style=\"stroke:black;fill:none ;stroke-width:4\" />";
            this.info = $"Polyline (points={points})";
            break;
        }
        WriteLine(info + " added to the canvas");
    }

    //method that takes a snapshot or originator object, turns it into memento, taking paramters of svgLine and info
    public ShapeMemento storeShapeInMemento() {
        //Console.WriteLine("Storing memento");
        return new ShapeMemento(svgLine, info);
    }

    //method which program calls to traverse snapshot of shapes in caretaker using undo and redo, argument m is shape caretaker returns when undoing or redoing a shape
    public string restoreFromMemento(ShapeMemento m){
        if(m is null){      //in case no undid all shapes on canvas no shapes can be returned therefore set svgLine and info to acknowledge this
            svgLine = "No Shape on Canvas";
            info = "No Shape on Canvas";
        } else {
        svgLine = m.getSavedSvgLine();  //get new undid/redid shapes information 
        info = m.getInfo();
        }
        return info;
    }

    //getters for shape object parameters
    public string getSvgLine() { return this.svgLine;}
    public string getInfo() {return this.info;}
}
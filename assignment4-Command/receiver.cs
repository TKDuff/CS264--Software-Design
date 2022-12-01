//Receiver class in canvas itself, contains extra logic to store the shapes
using System;
using System.IO;
using System.Collections.Generic;


public class Canvas
{
    
    private List<Shape> canvas = new List<Shape>();   //stack storing shapes on canvas
    string svgCode;     //store shapes in string so can be displayed

    //Method that takes shape from "AddShapeCommand" command classes "execute" method and adds it to the canvas
    public void Add(Shape s)
    {
        canvas.Add(s);
        Console.WriteLine("Added Shape to canvas: " + s.info);
    }
    

    //Method that removes shape from top of canvas, is called by "AddShapeCommand" command class "unexecute" method
    public Shape Remove()
    { 
        Shape s = canvas[canvas.Count-1];    //Subtract one from index as List index starts at 1, not 0
        canvas.RemoveAt(canvas.Count-1);
        Console.WriteLine("Removed Shape from canvas: " + s.info);
        return s;
    }

    public string display() {
        if(canvas.Count == 0){     //if nothing on canvas, cannot display anything
            return "Nothing to display";
        }
        svgCode = "";
        svgCode += ("<html>\n\t<body>\n\t\t<svg width=\"" + 1000 + "\" height=\"" + 1000 + "\">\n"); //boilerplate html

        
        foreach(Shape item in canvas){  //for each shape Memento in savedShape list
            svgCode += "\t\t\t" + item.svgLine + "\n";    //add shape mementos information to string which will be written to html file
        }
        svgCode += ("\t\t</svg>\n\t</body>\n</html>\n"); //boilerplate html
        return svgCode;
        
    }
}
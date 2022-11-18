using System;
using System.Collections.Generic;
using System.Linq;
//Caretaker class
class ShapeCaretaker{
    public static List<ShapeMemento> savedShapes;   //list to store current shapes on the canvas
    public static List<ShapeMemento> undidShapes;   //list to store shapes undid of of the canvas
    public int savedLength;
    public int undidLength;
    public string svgCode;     //string that stores svg code to write to html file

    //initialise lists
    public ShapeCaretaker(){
        savedShapes = new List<ShapeMemento>();
        undidShapes = new List<ShapeMemento>();
    }

    //adding Shape memento to canvas is simple 
    public void addShape(ShapeMemento Shape){
        savedShapes.Add(Shape);
    }

    //undo involves removing shape memento from front of savedShapes and moving it to front of undidShapes
    //taking current shape and moving it to storage
    public ShapeMemento Undo(){
        savedLength = savedShapes.Count-1;
        if(savedLength == -1){      //if no shapes on canvas to be undone
            Console.WriteLine("Cannot undo when no shapes on canvas");
            return null;
        } else {
            Console.WriteLine(savedShapes[savedLength].getInfo() + " removed from canvas");
            undidShapes.Add(savedShapes[savedLength]);  //add shape at top of savedShapes to top of undidShapes
            savedShapes.RemoveAt(savedLength);          //remove shape from top of savedShapes
            return (savedLength > 0) ? savedShapes[savedLength-1] : null;//check if savedShape length is greater than 0, if not, no more shapes on canvas therefore cannot return a shape so return null  
        }
    }

    //Redo involves removing shape memento from front of undidShapes and moving it to front or savedShapes
    //taking last undid shape and putting it back on the canvas
    public ShapeMemento Redo() {
        undidLength = undidShapes.Count-1;
        if(undidLength == -1){      //if no shapes undone
            Console.WriteLine("Cannot redo, no shapes to re-add");
            return null;
        } else {
            Console.WriteLine(undidShapes[undidLength].getInfo() + " added to canvas");
            savedShapes.Add(undidShapes[undidLength]);      //add shape at top of undidShapes to top of savedShapes
            undidShapes.RemoveAt(undidLength);              //remove shape from top of undidShapes
            return savedShapes[savedShapes.Count-1];
        }
    }

    public string DisplayCanvas() {
        if(savedShapes.Count == 0){     //if nothing on canvas, cannot display anything
            return "Nothing to display";
        }
        svgCode = "";
        svgCode += ("<html>\n\t<body>\n\t\t<svg width=\"" + 1000 + "\" height=\"" + 1000 + "\">\n"); //boilerplate html

        
        foreach(ShapeMemento item in savedShapes){  //for each shape Memento in savedShape list
            svgCode += "\t\t\t" + item.getSavedSvgLine() + "\n";    //add shape mementos information to string which will be written to html file
        }
        svgCode += ("\t\t</svg>\n\t</body>\n</html>\n"); //boilerplate html
        return svgCode;
    }
    
}
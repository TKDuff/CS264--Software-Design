using static System.Console;
using System;
using System.Collections.Generic;

public class ShapeOriginator{

    private string svgLine; //shapes svg code to be put into html file
    private string info;    //shapes information, used to be printed to console

    //Rectangle, ellipse and line all take four integer paramters, use switch case to determine which one to creat in set method
    //set method takes parameters from program, determine shape to create, fill in paramters to svg code and information lines
    public void set(Shape shape){
        this.svgLine = shape.svgLine;
        this.info = shape.info;
        
        Console.WriteLine(info + " added to the canvas");
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
using static System.Console;
using System;
using System.Collections.Generic;

public class Factory {

    public Factory() {

    }
     
    public Shape generateShape(String typeOfShape){
        switch(typeOfShape){
            case "circle":
                return new Circle();
            case "rectangle":
                return new Rectangle();
            case "ellipse":
                return new Ellipse();
            case "line":
                return new Line();
            case "polygon":
                return new Polygon();
            case "polyline":
                return new Polyline();
            default:
                return null;
        }
        }
        
}
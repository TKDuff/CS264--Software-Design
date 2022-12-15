using static System.Console;
using System;
using System.Collections.Generic;

public class ShapeFactory {

    public ShapeFactory() {

    }
     
    public Shape generateShape(String typeOfShape){
        Random rnd = new Random();
        switch(typeOfShape){
            case "circle":
                return new Circle(rnd.Next(1, 500), rnd.Next(1, 500), rnd.Next(1, 500));
            case "rectangle":
                return new Rectangle(rnd.Next(1, 500), rnd.Next(1, 500), rnd.Next(1, 500), rnd.Next(1, 500));
            case "ellipse":
                return new Ellipse(rnd.Next(1, 500), rnd.Next(1, 500), rnd.Next(1, 500), rnd.Next(1, 500));
            case "line":
                return new Line(rnd.Next(1, 500), rnd.Next(1, 500), rnd.Next(1, 500), rnd.Next(1, 500));
            case "polygon":
                return new Polygon(pickPoints());
            case "polyline":
                return new Polyline(pickPoints());
            case "path":
                return new Path(rnd.Next(100, 300), rnd.Next(30, 180), rnd.Next(100, 600), rnd.Next(1, 500));
            default:
                return null;
        }
    }

    //gets random set of points between 6 and 10, will always be and even number of points returned
    string pickPoints() {
        Random rnd = new Random();
        string points = "\"";
        int numberOfPoints = rnd.Next(3, 5) * 2;    //random number between 6 and 10
        int seed;
        for(int i = 0; i < numberOfPoints-1; i+=2){     //for each point
        seed = rnd.Next(1,500);
        points += $"{rnd.Next(seed, seed+150)},{rnd.Next(seed+150, seed+300)} ";  //assign random value and store as variable
        }
        return points + "\"";
    }
        
}
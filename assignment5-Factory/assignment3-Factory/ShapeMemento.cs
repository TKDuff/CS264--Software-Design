using System;
public class ShapeMemento {

    private string svgLine; //snapshot of svg code for shape
    private string info;    //snapshot of shape information (type & parameters)

    public ShapeMemento(string svgLine, string info){
        this.svgLine = svgLine;
        this.info = info;
    }

    //methods to get shape memento properties 
    public string getSavedSvgLine(){return svgLine;}
    public string getInfo() {return info;}
}
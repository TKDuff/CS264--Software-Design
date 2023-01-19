using System;

public class EmojiFeatureMemento {

    private string svgLine; //snapshot of svg code for EmojiFeature

    public EmojiFeatureMemento(string svgLine){
        this.svgLine = svgLine;
    }

    //methods to get EmojiFeature memento properties 
    public string getSavedSvgLine(){return svgLine;}
}
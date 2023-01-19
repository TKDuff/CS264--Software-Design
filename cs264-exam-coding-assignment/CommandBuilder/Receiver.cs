using System;
using System.IO;
using System.Collections.Generic;

namespace CommandBuilder
{
    public class Receiver
    {
        Dictionary<string, Emoji> FeatureList = new Dictionary<string, Emoji>();
        string Prevstyle = null;
        string svgCode;

        public void add(string name, Emoji feature){
            Console.WriteLine($"Added {name}, style A");
            FeatureList.Add(name, feature);
        }

        public void removeFeature(string name){
            Console.WriteLine($"Removed {name}");
            FeatureList.Remove(name);
        }

        public void MoveEmojiFeatureHorizontal(string name, bool MoveLeft, int magnitude){
            if(MoveLeft) {magnitude *= -1;}
            FeatureList[name].X += magnitude;
            Console.WriteLine($"Moved {name} by {magnitude}");

        }

        public void MoveEmojiFeatureVertical(string name, bool MoveDown, int magnitude){
            if(MoveDown) {magnitude *= -1;}
            FeatureList[name].Y += magnitude;
            Console.WriteLine($"Moved {name} by {magnitude}");
        }

        public void ChangeFeatureStyle(string feature, char Style){
            string styleChoosen = null;
            Prevstyle = FeatureList[feature].Style;
            switch(Style){
                case 'A':
                    styleChoosen = "fill=\"black\" stroke=\"#000000\" stroke-miterlimit=\"10\" stroke-width=\"2\"";
                    break;
                case 'B':
                    styleChoosen = "fill=\"green\" stroke=\"#000000\" stroke-miterlimit=\"10\" stroke-width=\"2\"";
                    break;
                case 'C':
                    styleChoosen = "fill=\"blue\" stroke=\"#000000\" stroke-miterlimit=\"10\" stroke-width=\"2\"";
                    break;
                default:
                    break;
            }
            FeatureList[feature].Style = styleChoosen;
            Console.WriteLine($"{feature} style set to {Style}");
        }

        public void UnChangeFeatureStyle(string feature){
            FeatureList[feature].Style = Prevstyle;
            Console.WriteLine($"{feature} style set to {Prevstyle}");
        }

        public string Draw(){
            string svgCode = "<svg id=\"emoji\" viewBox=\"0 0 500 500\" xmlns=\"http://www.w3.org/2000/svg\"> \n\t<g>\n\t\t";
            svgCode += "<circle cx=\"250\" cy=\"250\" r=\"250\" fill=\"yellow\" stroke-width=\"2\"/>";
            foreach(var item in FeatureList.Values)
            {
            svgCode += "\n\t\t" + item.ShowInfo();
            }
            svgCode += "\n\t</g>\n</svg>";
            return svgCode;
            }
    }

}
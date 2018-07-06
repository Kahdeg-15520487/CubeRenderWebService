using System;
using Bridge.Html5;
using Bridge.jQuery2;

namespace renderCube
{

    public class App
    {
        public static void Main()
        {
            var canvas = new HTMLTextAreaElement
            {
                Id = "canvas",
                ReadOnly = true,
                Rows = 50,
                Cols = 50
            };

            var div = new HTMLDivElement();

            var inputX = new HTMLInputElement()
            {
                Type = InputType.Number,
                DefaultValue = "0",
                Value = "8",
            };
            var inputY = new HTMLInputElement()
            {
                Type = InputType.Number,
                DefaultValue = "0",
                Value = "7"
            };
            var inputZ = new HTMLInputElement()
            {
                Type = InputType.Number,
                DefaultValue = "0",
                Value = "6"
            };

            var button = new HTMLButtonElement
            {
                InnerHTML = "Render",
                OnClick = (ev) =>
                {
                    if (double.TryParse(inputX.Value, out double x)
                     && double.TryParse(inputY.Value, out double y)
                     && double.TryParse(inputZ.Value, out double z))
                    {
                        string url = "https://anchat.azurewebsites.net/api/render/" + $"{x}/{y}/{z}";

                        jQuery.GetJSON(
                            url, null,
                            delegate (object data, string s, jqXHR jqXHR)
                            {

                                canvas.Value = data["rendered"].ToString();

                                int w = int.Parse(data["width"].ToString());
                                int h = int.Parse(data["height"].ToString());

                                canvas.Cols = Math.Max(canvas.Cols, w);
                                canvas.Rows = Math.Max(canvas.Rows, h);

                            });
                    }
                }
            };

            // Add the Button to the page
            div.AppendChild(new HTMLLabelElement { TextContent = "X:" });
            div.AppendChild(inputX);
            div.AppendChild(new HTMLBRElement());
            div.AppendChild(new HTMLLabelElement { TextContent = "Y:" });
            div.AppendChild(inputY);
            div.AppendChild(new HTMLBRElement());
            div.AppendChild(new HTMLLabelElement { TextContent = "Z:" });
            div.AppendChild(inputZ);
            div.AppendChild(new HTMLBRElement());
            div.AppendChild(button);
            Document.Body.AppendChild(div);
            Document.Body.AppendChild(new HTMLHRElement());
            Document.Body.AppendChild(canvas);
        }
    }
}
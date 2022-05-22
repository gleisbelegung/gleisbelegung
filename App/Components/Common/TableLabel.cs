using Godot;

namespace Gleisbelegung.App.Components.Common
{
    public class TableLabel : Label
    {
        public TableLabel(string text)
        {
            Align = Label.AlignEnum.Center;
            Text = text;
        }
    }
}
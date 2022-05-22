using Godot;

namespace Gleisbelegung.App.Components.Common
{
    public class TableColumn : VBoxContainer
    {
        public TableColumn()
        {
            // AddConstantOverride("separation", 0);
            RectMinSize = new Vector2(100, 0);
        }
    }
}
using Godot;

namespace Gleisbelegung.App.Extensions
{
    public static class NodeExtensions
    {
        public static Node GetRoot(this Node node)
        {
            return node.GetTree().Root.GetNode("Spatial");
        }
    }
}
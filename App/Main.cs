using System;
using System.Linq;
using Gleisbelegung.App.Common;
using Gleisbelegung.App.STSConnect;
using Godot;

public class Main : Node
{

	private STSSocket stsSocket;

	public override void _Ready()
	{

		var interfaceType = typeof(IEventListener);
		var all = AppDomain.CurrentDomain.GetAssemblies()
		  .SelectMany(x => x.GetTypes())
		  .Where(x => interfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
		  .Select(x => Activator.CreateInstance(x))
		  .ToList();

		stsSocket = new STSSocket();

		GD.Print("Hello World!");
	}

	public override void _Notification(int what)
	{
		if (what == MainLoop.NotificationWmQuitRequest || what == MainLoop.NotificationWmGoBackRequest)
		{
			GD.Print("Quitting");
		}
	}
}

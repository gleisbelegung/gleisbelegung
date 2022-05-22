using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Gleisbelegung.App.Common;
using Gleisbelegung.App.Components.Common;
using Gleisbelegung.App.Events;
using Godot;

public class GleisbelegungTable : HBoxContainer, IEventListener<ConnectionStatusEvent>
{

    public override void _Ready()
    {
        this.RegisterSubscriptions();
        // AddConstantOverride("separation", 0);
    }

    public void ProcessEvent(ConnectionStatusEvent eventData)
    {
        if (eventData.ConnectionStatus == ConnectionStatus.ESTABLISHED)
        {
            PopulateTable();
        }
    }

    private void PopulateTable()
    {
        var database = Database.GetInstance();
        var platforms = database.Platforms.Values;

        var timeColumn = new TableColumn();
        var time = database.Time;

        var tableHeaderItem = new TableLabel("Zeit / Gleis");
        timeColumn.AddChild(tableHeaderItem);

        for (int i = 0; i < 60; i++)
        {
            var platformLabel = new TableLabel(time.AddMinutes(i).ToString("HH:mm"));

            timeColumn.AddChild(platformLabel);
        }
        AddChild(timeColumn);


        foreach (var platform in platforms)
        {
            var column = new TableColumn();
            var platformLabel = new TableLabel(platform.Name);

            column.AddChild(platformLabel);
            AddChild(column);
        }
    }
}

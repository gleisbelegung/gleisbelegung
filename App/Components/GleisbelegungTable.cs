using System;
using Gleisbelegung.App.Common;
using Gleisbelegung.App.Components.Common;
using Gleisbelegung.App.Events;
using Godot;

public class GleisbelegungTable : HBoxContainer, IEventListener<ConnectionStatusEvent>
{
    private const int MinutesInAdvance = 60;

    public override void _Ready()
    {
        this.RegisterSubscriptions();
    }

    public void ProcessEvent(ConnectionStatusEvent eventData)
    {
        if (eventData.ConnectionStatus == ConnectionStatus.REFETCHING_TRAIN_DETAILS)
        {
            PopulateTable();
        }
    }

    private void PopulateTable()
    {
        var database = Database.Instance;
        var platforms = database.Platforms.Values;

        var timeColumn = new TableColumn();
        var time = database.Time;

        var tableHeaderItem = new TableLabel("Zeit / Gleis");
        timeColumn.AddChild(tableHeaderItem);

        for (int i = 0; i < MinutesInAdvance; i++)
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

            var timeOnly = database.Time.TimeOfDay;

            for (int i = 0; i < MinutesInAdvance; i++)
            {
                var trainLabel = new TrainTableLabel(platform, timeOnly.Add(TimeSpan.FromMinutes(i)));

                column.AddChild(trainLabel);
            }
        }
    }
}

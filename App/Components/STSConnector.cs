using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Gleisbelegung.App.Common;
using Gleisbelegung.App.Events;
using Gleisbelegung.App.STSConnect;
using Godot;

public class STSConnector : WindowDialog, IEventListener<ConnectionStatusEvent>
{
    public LineEdit _ipInput;
    public Button _connectButton;

    public override void _Ready()
    {
        this.RegisterSubscriptions();
        CallDeferred("popup");
    }

    public override void _EnterTree()
    {
        base._EnterTree();


        _ipInput = GetNode<LineEdit>("VBoxContainer/IPInput");
        _connectButton = GetNode<Button>("VBoxContainer/ConnectButton");

        _connectButton.Connect("pressed", this, nameof(OnConnectButtonPressed));

        _ipInput.Text += GetLocalIPAddress();
    }

    private void OnConnectButtonPressed()
    {
        _connectButton.Text = "Waiting...";
        _connectButton.Disabled = true;

        Task.Run(() =>
        {
            new STSSocket(_ipInput.Text);
        });
    }

    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }

    public void ProcessEvent(ConnectionStatusEvent eventData)
    {
        _connectButton.Text = eventData.ConnectionStatus.ToString();

        if (eventData.ConnectionStatus == ConnectionStatus.ESTABLISHED)
        {
            Hide();
        }
    }
}

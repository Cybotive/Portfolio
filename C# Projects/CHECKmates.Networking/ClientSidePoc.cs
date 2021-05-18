using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;
using Xamarin.Forms;

namespace CHECKmates.Networking
{
    public class ClientSidePoc : ContentPage
    {
        private StackLayout stackLay;
        private Entry ipEntry;
        private Label connectionStatusLabel;
        private Label addressListLabel;

        private CheckmatesTcpClient client;

        private IPAddress hostIp;
        private int maxLengthEntry = 15;

        public ClientSidePoc()
        {
            SetupLayout();
            DisplayEntry();

            client = new CheckmatesTcpClient();
        }

        private void SetupLayout()
        {
            stackLay = new StackLayout { Padding = 10 };
            connectionStatusLabel = new Label { Text = "Not Connected." };
            addressListLabel = new Label { Text = "Local IP Addresses:\n" };

            Button connectButton = new Button();
            Button voteExplainButton = new Button();

            connectButton.Text = "Connect";
            connectButton.Clicked += OnConnectButtonClicked;

            voteExplainButton.Text = "Explain Voting Modes";
            voteExplainButton.Clicked += OnVoteExplainButtonClicked;

            stackLay.Children.Add(voteExplainButton);
            stackLay.Children.Add(connectionStatusLabel);
            stackLay.Children.Add(connectButton);
            stackLay.Children.Add(addressListLabel);

            string localHostname = Dns.GetHostName();
            IPAddress[] addList = Dns.GetHostEntry(localHostname).AddressList;

            foreach (IPAddress ip in addList)
            {
                // Test that current ip is IPV4 and it is not the "localhost" address.
                if (ip.AddressFamily is AddressFamily.InterNetwork && !ip.Equals(IPAddress.Loopback))
                {
                    addressListLabel.Text += ip.ToString() + '\n';
                }
            }

            this.Content = stackLay;
        }

        private void DisplayEntry()
        {
            ipEntry = new Entry
            {
                Placeholder = "IP Address of Host...",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                TextTransform = TextTransform.Uppercase,
                MaxLength = maxLengthEntry,
                IsSpellCheckEnabled = false,
                IsTextPredictionEnabled = false
            };

            if (hostIp != null)
            {
                ipEntry.Text = hostIp.ToString();
            }

            stackLay.Children.Add(ipEntry);
        }

        private async void OnVoteExplainButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutVotingModes());
        }

        private async void OnConnectButtonClicked(object sender, EventArgs args)
        {
            if (client.ConnectedPort != CheckmatesTcpServer.PortInvalid)
                return;

            int connectedPort = CheckmatesTcpServer.PortInvalid;

            try
            {
                hostIp = IPAddress.Parse(ipEntry.Text);
            }
            catch (ArgumentNullException)
            {
                connectionStatusLabel.Text = "Invalid IP.";
                return;
            }
            catch (FormatException)
            {
                connectionStatusLabel.Text = "Invalid IP.";
                return;
            }

            try
            {
                connectedPort = client.Connect(hostIp);
            }
            catch (ApplicationException)
            {
                connectionStatusLabel.Text = "Connection Error.";
                return;
            }

            if (connectedPort != CheckmatesTcpServer.PortInvalid)
            {
                connectionStatusLabel.Text = "Connected! @" + hostIp.ToString() + ":" + connectedPort;

                AppData.SetOffline(false);
                string nextPageName = "Choices";

                AppData.IsWaiting = true;
                await Navigation.PushAsync(new Waiting(nextPageName));
            }
            else
            {
                connectionStatusLabel.Text = "Host Not Found!";
            }
        }
    }
}
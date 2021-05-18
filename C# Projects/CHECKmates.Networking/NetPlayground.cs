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
    public class NetPlayground : ContentPage
    {
        private StackLayout stackLay;
        private Label httpListenLabel;
        private Label httpStatusLabel;
        private Label tcpStatusLabel;

        private HttpClient httpClient;
        private CheckmatesTcpServer server;

        public NetPlayground()
        {
            setupLayout();

            httpClient = new HttpClient();
            server = new CheckmatesTcpServer();

            this.Disappearing += server.KillServer; //potentially move to App.OnPause()
        }

        private void setupLayout()
        {
            stackLay = new StackLayout();
            httpListenLabel = new Label { Text = HttpListener.IsSupported ? "HttpListener Supported." : "HttpListener Not Supported." };
            httpStatusLabel = new Label { Text = "Webpage not yet gathered." };
            tcpStatusLabel = new Label { Text = "TCP not yet setup." };

            Button getButton = new Button();
            Button listenButton = new Button();

            getButton.Text = "Get Google.com (Unthreaded)";
            getButton.Clicked += OnGetButtonClicked;

            listenButton.Text = "Start TCP Listener Thread";
            listenButton.Clicked += OnListenButtonClicked;

            stackLay.Children.Add(httpListenLabel);
            stackLay.Children.Add(tcpStatusLabel);
            stackLay.Children.Add(listenButton);
            stackLay.Children.Add(getButton);

            stackLay.Children.Add(httpStatusLabel);

            this.Content = stackLay;
        }

        private async void OnGetButtonClicked(object sender, EventArgs args)
        {
            await HttpGetAsync();
        }

        private void OnListenButtonClicked(object sender, EventArgs args)
        {
            string address = server.StartOnLan();

            if(server.IsActive())
            {
                tcpStatusLabel.Text = string.Format("Server is ACTIVE at {0}", address);
            }
            else
            {
                tcpStatusLabel.Text = "Server FAILED to start!";
            }
        }

        private async System.Threading.Tasks.Task HttpGetAsync()
        {
            try
            {
                string responseBody = await httpClient.GetStringAsync("http://www.google.com/");
                httpStatusLabel.Text = responseBody;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nHTTP Get Failed!");
                Console.WriteLine("Message :{0} ", e.Message);

                httpStatusLabel.Text = "GET Failed!";
            }
        }
    }
}
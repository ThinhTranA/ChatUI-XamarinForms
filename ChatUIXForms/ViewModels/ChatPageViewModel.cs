using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ChatUIXForms.Models;
using Xamarin.Forms;

namespace ChatUIXForms.ViewModels
{
    public class ChatPageViewModel: INotifyPropertyChanged
    {
        public bool ShowScrollTap { get; set; } = false;
        public bool LastMessageVisible { get; set; } = true;
        public int PendingMessageCount { get; set; } = 0;
        public bool PendingMessageCountVisible { get { return PendingMessageCount > 0; } }

        public Queue<Message> DelayedMessages { get; set; } = new Queue<Message>();
        public ObservableCollection<Message> Messages { get; set; } = new ObservableCollection<Message>();
        public string TextToSend { get; set; }
        public ICommand OnSendCommand { get; set; }
        public ICommand MessageAppearingCommand { get; set; }
        public ICommand MessageDisappearingCommand { get; set; }

        public ChatPageViewModel()
        {
            Messages.Insert(0,new Message() { Text = "Hi" });
            Messages.Insert(0, new Message() { Text = "How are you?", User = App.User});
            Messages.Insert(0, new Message() { Text = "What's new?" });

            MessageAppearingCommand = new Command<Message>(OnMessageAppearing);
            MessageDisappearingCommand = new Command<Message>(OnMessageDisappearing);

            OnSendCommand = new Command(() =>
            {
                if(!string.IsNullOrEmpty(TextToSend)){
                    Messages.Insert(0, new Message() { Text = TextToSend, User = App.User });
                    TextToSend = string.Empty;
                }
               
            });

            ////Code to simulate reveing a new message procces
            //Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            //{
            //    if (LastMessageVisible)
            //    {
            //        Messages.Insert(0, new Message(){ Text = "New message test" , User="Mario"});
            //    }
            //    else
            //    {
            //        DelayedMessages.Enqueue(new Message() { Text = "New message test" , User = "Mario"});
            //        PendingMessageCount++;
            //    }
            //    return true;
            //});

           
           
        }

        void OnMessageAppearing(Message message)
        {
            var idx = Messages.IndexOf(message);
            if (idx <= 6)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    while (DelayedMessages.Count > 0)
                    {
                        Messages.Insert(0, DelayedMessages.Dequeue());
                    }
                    ShowScrollTap = false;
                    LastMessageVisible = true;
                    PendingMessageCount = 0;
                });
            }
        }

        void OnMessageDisappearing(Message message)
        {
            var idx = Messages.IndexOf(message);
            if (idx >= 6)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ShowScrollTap = true;
                    LastMessageVisible = false;
                });

            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}

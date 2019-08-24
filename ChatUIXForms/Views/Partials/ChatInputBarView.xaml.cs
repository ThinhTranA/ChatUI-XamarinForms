using System;
using System.Collections.Generic;
using ChatUIXForms.ViewModels;
using Xamarin.Forms;

namespace ChatUIXForms.Views.Partials
{
    public partial class ChatInputBarView : ContentView
    {


        public ChatInputBarView()
        {
            InitializeComponent();

              isButtonsVisible = true;
            expandButton.IsVisible = false;

            if (Device.RuntimePlatform == Device.iOS)
            {
                this.SetBinding(HeightRequestProperty, new Binding("Height", BindingMode.OneWay, null, null, null, chatTextInput));
            }
        }
        public void Handle_Completed(object sender, EventArgs e)
        {
            (this.Parent.Parent.BindingContext as ChatPageViewModel).OnSendCommand.Execute(null);
            chatTextInput.Focus();

            UnFocusEntry();
            ToggleButtons();
        }


        private void ExpandButton_Clicked(object sender, EventArgs e)
        {
            expandButton.IsVisible = false;
            micButton.IsVisible = true;
            speakerButton.IsVisible = true;
        }

        private void ChatTextInputTapped(object sender, EventArgs e)
        {

        }

        public void UnFocusEntry(){
            chatTextInput?.Unfocus();
        }

     

        private bool isButtonsVisible;
        public void ToggleButtons()
        {
            isButtonsVisible = !isButtonsVisible;
          

           
            if (isButtonsVisible)//chatTextInput.IsFocused)
            {
                //Fading buttons from right to left order
                micButton.FadeTo(0, 1500, Easing.SpringIn);
                micButton.IsVisible = false;

                speakerButton.FadeTo(0, 1500, Easing.SpringIn);
                speakerButton.IsVisible = false;

                expandButton.IsVisible = true;
            }
            else
            {
                micButton.Opacity = 1;
               // micButton.IsVisible = isButtonsVisible;
                speakerButton.Opacity = 1;
                //speakerButton.IsVisible = isButtonsVisible;

            }
        }

    }
}

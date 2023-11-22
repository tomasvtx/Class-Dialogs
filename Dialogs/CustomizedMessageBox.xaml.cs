using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Dialogs
{
    public partial class CustomizedMessageBox : Window
    {
        public byte MessageBoxID;

        public CustomizedMessageBox(string title, string message, IEnumerable<string> buttonLabels)
        {
            InitializeComponent();

            Title = title;
            Titleq.Content = title;
            ShowInTaskbar = false;
            Content.Text = message;
            WindowStyle = WindowStyle.None;

            // Configure buttons
            ConfigureButtons(buttonLabels);
        }

        private void ConfigureButtons(IEnumerable<string> labels)
        {
            // Clear existing buttons from the StackPanel
            ButtonStackPanel.Children.Clear();

            for (int i = 0; i < labels.ToArray().Length; i++)
            {
                var button = new Button
                {
                    Content = labels.ToArray()[i],
                    Foreground = GetButtonForeground(i),
                    Style = (Style)FindResource("ButtonStyle"), // Assuming you have a ButtonStyle defined in your resources
                    Margin = new Thickness(10),
                    MinWidth = 100
                };

                var ButtonId = (byte)(i + 1);

                // Attach a Click event handler
                button.Click += (sender, e) =>
                {
                    MessageBoxID = ButtonId;
                    Close();
                };

                // Add the button to the StackPanel
                ButtonStackPanel.Children.Add(button);
            }
        }

        private SolidColorBrush GetButtonForeground(int index)
        {
            switch (index)
            {
                case 0:
                    return Brushes.Green;
                case 1:
                    return Brushes.Red;
                case 2:
                    return Brushes.Orange;
                case 3:
                    return Brushes.Firebrick;
                case 4:
                    return Brushes.Sienna;
                default:
                    return Brushes.Black; // Default color, change as needed
            }
        }


        private void TitleqMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
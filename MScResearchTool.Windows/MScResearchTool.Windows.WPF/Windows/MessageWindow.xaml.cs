using System.Windows;

namespace MScResearchTool.Windows.WPF.Windows
{
    public partial class MessageWindow : Window
    {
        public MessageWindow(string title, string content, string confirmation)
        {
            InitializeComponent();

            TitleBlock.Text = title;
            ContentBlock.Text = content;
            CloseButton.Content = confirmation;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

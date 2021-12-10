using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace JoJoSuite.UI
{
    /// <summary>
    /// Interaction logic for RecorderCodeGenerator.xaml
    /// </summary>
    public partial class RecorderCodeGenerator : Window
    {
        RecordedElements recordedElement;
        public RecorderCodeGenerator(RecordedElements _recordedElement)
        {
            InitializeComponent();

            recordedElement = _recordedElement;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (recordedElement != null)
                {
                    txtCode.Document.Blocks.Clear();

                    foreach (var element in recordedElement.Elements)
                    {
                        txtCode.Document.Blocks.Add(new Paragraph(new Run(element.ElementXPath)));
                    }
                }
                else
                {
                    btnCopy.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void BtnCopy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(new TextRange(txtCode.Document.ContentStart, txtCode.Document.ContentEnd).Text);

            ////Set text to ricktextbox
            //txtCode.Document.Blocks.Clear();
            //txtCode.Document.Blocks.Add(new Paragraph(new Run("Text")));
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void gridTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnWinClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}

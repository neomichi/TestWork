using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Calculator.Core;

namespace Calculator.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PostfixNotationExpression pne;
        public MainWindow()
        {
            InitializeComponent();
            var buttonsList = PapaGrid.Children.Cast<Control>()
                .Where(x => x is Button).Select(x=>(Button)x).ToList();

            foreach(var button in buttonsList)
            {
                button.Click += Button_Click;
            }

            pne = new PostfixNotationExpression();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var action = ((Button)sender).Content.ToString();
            if (!string.IsNullOrWhiteSpace(action))
            {
                action = action.Trim().ToLower().Replace("button", "");
                var currentText = TextBox.Text;

                var sb = new StringBuilder(currentText);                
                switch (action)
                {
                    case "+": { sb.Append("+"); break; }
                    case "-": { sb.Append("-"); break; }
                    case "*": { sb.Append("*"); break; }
                    case "/": { sb.Append("/"); break; }
                    case "^": { sb.Append("^"); break; }
                    case ")": { sb.Append(")"); break; }
                    case "(": { sb.Append("("); break; }
                    case ".": { sb.Append("."); break; }

                    case "back":{ sb = new StringBuilder(currentText.RemoveLast()); break;}
                    case "c": { sb = new StringBuilder(""); break; }
                    case "±": { sb= new StringBuilder(currentText.IfNegativeStr()); break; }
                    case "=": { sb = new StringBuilder(pne.result(currentText).ToString()); break; }
                }
                //if number
                sb.Append(string.IsNullOrWhiteSpace(Regex.Match(action, @"\d").Value) ? "" : action);



                //

                TextBox.Text = sb.ToString();
                //MessageBox.Show(action);
            }

            
        }
    }
}

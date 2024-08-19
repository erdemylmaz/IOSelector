
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace IOSelector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _typeIndex = 3;
        private int _langIndex = 0;

        private Dictionary<string, string[]> _OSAIInputTagNamePairs = new Dictionary<string, string[]>();
        private Dictionary<string, bool> _OSAIInputTagStatusPairs = new Dictionary<string, bool>();

        private Dictionary<string, string[]> _OSAIOutputTagNamePairs = new Dictionary<string, string[]>();
        private Dictionary<string, bool> _OSAIOutputTagStatusPairs = new Dictionary<string, bool>();


        private Dictionary<string, string[]> _ISACInputTagNamePairs = new Dictionary<string, string[]>();
        private Dictionary<string, bool> _ISACInputTagStatusPairs = new Dictionary<string, bool>();

        private Dictionary<string, string[]> _ISACOutputTagNamePairs = new Dictionary<string, string[]>();
        private Dictionary<string, bool> _ISACOutputTagStatusPairs = new Dictionary<string, bool>();

        public MainWindow()
        {
            InitializeComponent();
            _ReadExcel();
        }

        public void SavePreferences(object sender, RoutedEventArgs e)
        {
            _typeIndex = type_combobox.SelectedIndex;
            _langIndex = language_combobox.SelectedIndex;
            _ClearStatuses();
            InitializeSettings();
        }

        public void CreateIOFile(object sender, RoutedEventArgs e)
        {
            string outputText = "";
            Params @params = new Params();
            
            if(_typeIndex == 0)
            {
                // CREATE OSAI
            } else
            {
                // CREATE ISAC
                string InputText = CreateLinesForISAC(_ISACInputTagNamePairs, _ISACInputTagStatusPairs, "Input");
                string OutputText = CreateLinesForISAC(_ISACOutputTagNamePairs, _ISACOutputTagStatusPairs, "Output");
                outputText += InputText;
                outputText += OutputText;
            }

            string FilePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\io.txt";
            File.WriteAllText(FilePath, outputText);
        }

        private string CreateLinesForISAC(Dictionary<string, string[]> tagNamePairs, Dictionary<string, bool> tagStatusPairs, string type)
        {
            Params @params = new Params();

            string outputText = "";

            int nodeIndex = 1;
            int wIndex = 0;
            int dimIndex = 1;

            int lineIndex = 0;
            foreach (var pair in tagNamePairs)
            {
                bool status = tagStatusPairs[pair.Key];
                if (status)
                {
                    if (lineIndex % 16 == 0)
                    {
                        string Title = $";{type} - Node {nodeIndex};W{wIndex};DIM {dimIndex} / 0-{@params.SettingsPerSection - 1};{@params.SettingsPerSection}\n";
                        outputText += Title;
                        nodeIndex++;
                        wIndex++;
                        dimIndex++;
                    }
                    string lineText =
                        $"{addExtraZeros(lineIndex % 16)}; {string.Join("_", pair.Key.Split(" "))}; {lineIndex % 16}. {pair.Value[_langIndex]}\n";
                    outputText += lineText;
                    lineIndex++;
                }
            }

            // if section is not compelted, fill with empty lines
            if (lineIndex % 16 != 15)
            {
                int remained = 15 - lineIndex % 15;
                for (int i = 0; i < remained; i++)
                {
                    lineIndex++;
                    string emptyLineText = $"{addExtraZeros(lineIndex % 16)};;{lineIndex % 16}.\n";
                    outputText += emptyLineText;
                }
            }

            return outputText;
        }
        private string addExtraZeros(int x)
        {
            if(x < 10)
            {
                return "00" + x;
            } else if (x < 100)
            {
                return "0" + x;
            }
            return "" + x;
        }

        private void _ClearStatuses()
        {
            // CLEAR OSAI
            foreach(var pair in _OSAIInputTagStatusPairs)
            {
                _OSAIInputTagStatusPairs[pair.Key] = false;
            }
            foreach (var pair in _OSAIOutputTagStatusPairs)
            {
                _OSAIOutputTagStatusPairs[pair.Key] = false;
            }

            // CLEAR ISAC
            foreach (var pair in _ISACInputTagStatusPairs)
            {
                _ISACInputTagStatusPairs[pair.Key] = false;
            }
            foreach (var pair in _ISACOutputTagStatusPairs)
            {
                _ISACOutputTagStatusPairs[pair.Key] = false;
            }
        }

        private void InitializeSettings()
        {
            if (_typeIndex == 0)
            {
                InitializeSettingsArea(_OSAIInputTagNamePairs, _OSAIInputTagStatusPairs, input_settings_area, 0);
                InitializeSettingsArea(_OSAIOutputTagNamePairs, _OSAIOutputTagStatusPairs, output_settings_area, 1);
            } else
            {
                InitializeSettingsArea(_ISACInputTagNamePairs, _ISACInputTagStatusPairs, input_settings_area, 0);
                InitializeSettingsArea(_ISACOutputTagNamePairs, _ISACOutputTagStatusPairs, output_settings_area, 1);
            }

        }

       

        private void InitializeSettingsArea(Dictionary<string, string[]> pairs, Dictionary<string, bool> statusPairs, StackPanel settings_area, int type)
        {
            settings_area.Children.Clear();
            Label lbl = new Label { FontWeight = FontWeights.Bold, Margin = new Thickness(4)};
            lbl.Content = "INPUT SETTINGS";
            if (type == 1)
            {
                lbl.Content = "OUTPUT SETTINGS";
            }
            settings_area.Children.Add(lbl);
            StackPanel settings = new StackPanel();
            int index = 1;
            foreach (var pair in pairs)
            {
                DockPanel row = new DockPanel { VerticalAlignment = VerticalAlignment.Center, Height = 36 };

                CheckBox checkBox = new CheckBox { Height = 32, Width = 32, VerticalAlignment = VerticalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center };
                checkBox.Checked += (sender, e) => statusPairs[pair.Key] = true;
                checkBox.Unchecked += (sender, e) => statusPairs[pair.Key] = false;

                Label indexLabel = new Label { Content = index + ". ", Margin = new Thickness(12, 0, 4, 0), FontWeight = FontWeights.SemiBold, HorizontalAlignment = HorizontalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center, Width = 32, };
                Label label = new Label { FontWeight = FontWeights.SemiBold, FontSize = 16 };
                label.Content = pair.Value[0];

                if (index % 2 == 1)
                {
                    row.Background = Brushes.LightGray;
                }
                else
                {
                    row.Background = Brushes.LightSlateGray;
                }

                if(_langIndex == 1)
                {
                    label.Content = pair.Value[1];
                }

                label.Margin = new Thickness(4, 0, 0, 0);

                row.Children.Add(indexLabel);
                row.Children.Add(checkBox);
                row.Children.Add(label);

                settings.Children.Add(row);

                index++;
            }
            settings_area.Children.Add(settings);
        }

        private void _ReadExcel()
        {
            Params @params = new Params();
            Console.WriteLine(@params.ExcelFilePath);
            using (var workbook = new XLWorkbook(@params.ExcelFilePath))
            {
                IXLWorksheets sheets = workbook.Worksheets;
                
                for (int i = 0; i < sheets.Count; i++)
                {
                    IXLWorksheet ws = sheets.Worksheet(i + 1);
                    if(i == 0)
                    {
                        _ReadSheet(ws, _OSAIInputTagNamePairs, _OSAIInputTagStatusPairs);
                    } else if (i == 1)
                    {
                        _ReadSheet(ws, _OSAIOutputTagNamePairs, _OSAIOutputTagStatusPairs);
                    } else if (i == 2)
                    {
                        _ReadSheet(ws, _ISACInputTagNamePairs, _ISACInputTagStatusPairs);
                    } else if (i == 3)
                    {
                        _ReadSheet(ws, _ISACOutputTagNamePairs, _ISACOutputTagStatusPairs);
                    }
                }

                InitializeSettings();
            }
        }

        private void _ReadSheet(IXLWorksheet ws, Dictionary<string, string[]> tagNamePairs, Dictionary<string, bool> tagStatusPairs)
        {
            
            foreach(var row in ws.RowsUsed())
            {
                int rowIndex = row.RowNumber();
                if (rowIndex <= 1) continue;
                String tag = row.Cell(1).Value.ToString();
                String name = row.Cell(2).Value.ToString();
                String turkishName = row.Cell(3).Value.ToString();

                try
                {

                    tagNamePairs.Add(tag, [name, turkishName]);
                    tagStatusPairs.Add(tag, false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }
        }
    }
}
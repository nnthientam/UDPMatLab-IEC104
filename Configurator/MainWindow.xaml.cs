﻿using SimulinkIEC104;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Configurator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        Dictionary<string, Dictionary<string, string>> _columnsDictionary = new Dictionary<string, Dictionary<string, string>>
        {
            { "_dgSendUDPparams", new Dictionary<string, string> {{ "ID", "ID" }, { "Name", "Название" },{ "DataType", "Тип" }}},
            { "_dgReceiveUDPparams", new Dictionary<string, string> {{ "ID", "ID" }, { "Name", "Название" },{ "DataType", "Тип" }}},
            { "_dgReceiveIEC104params", new Dictionary<string, string> {{ "UDPparameterIDs", "Параметры Simulink" }, { "IOA", "Адрес 104" }}},
            { "_dgSendIEC104params", new Dictionary<string, string> {{ "UDPParameterID", "Параметр Simulink" }, { "IOA", "Адрес 104" }}}
        };


        private static string _configFileName = "settings.xml";
        private static Settings _settings;
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(Settings));
                using (FileStream fs = new FileStream(_configFileName, FileMode.Open))
                {
                    _settings = (Settings)formatter.Deserialize(fs);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка десериализации " + ex.Message);
            }

            DataContext= _settings;
             
        }

        private void _save_Button_Click(object sender, RoutedEventArgs e)
        {

            XmlSerializer formatter = new XmlSerializer(typeof(Settings));

            try
            {
                using (FileStream fs = new FileStream(_configFileName, FileMode.Open))
                {
                    formatter.Serialize(fs, _settings);
                }
                MessageBox.Show("Сохранено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении: " + ex.Message);
            }
        }

        private void _addUDPDestination_Button_Click(object sender, RoutedEventArgs e)
        {
            _settings.UDPDestinations.Add(new Destination("Новое"));
        }

        private void _deleteUDPDestination_Button_Click(object sender, RoutedEventArgs e)
        {
            _settings.UDPDestinations.Remove((Destination)_lbUDPDestinations.SelectedItem);
        }

        private void _addIEC104DestinationButton_Click(object sender, RoutedEventArgs e)
        {
            IEC104Destination newDest;
            var result = MessageBox.Show("Создать клиент? Да - создастся клиент, Нет - создастся сервер", "Клиент или сервер МЭК104?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                newDest = new IEC104Connection("Новый клиент");
            } 
            else
            {
                newDest = new IEC104Server("Новый сервер");
            }

            _settings.IEC104Destinations.Add(newDest);
            _cbIEC104Destinations.SelectedItem = newDest;
        }

        private void _deleteIEC104DestinationButton_Click(object sender, RoutedEventArgs e)
        {
            _settings.IEC104Destinations.Remove((IEC104Destination)_cbIEC104Destinations.SelectedItem);
        }



        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            _settings.IEC104Destinations.ResetBindings();
            int index = _cbIEC104Destinations.SelectedIndex;
            _cbIEC104Destinations.SelectedIndex = -1;
            _cbIEC104Destinations.SelectedIndex = index;
        }



        private void _cbIEC104Destinations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_cbIEC104Destinations.SelectedItem == null) return;

            if (_cbIEC104Destinations.SelectedItem.GetType() == typeof(IEC104Connection))
            {
                _tbIPadsress.Visibility = Visibility.Visible;                
            }
            else
            {
                _tbIPadsress.Visibility = Visibility.Hidden;                
            }
        }

        private void _addCAButton_Click(object sender, RoutedEventArgs e)
        {
            ((IEC104Destination)_cbIEC104Destinations.SelectedItem).CommonAdreses.Add(new IEC104CommonAddress("Новый CA"));
        }

        private void _removeCAButton_Click(object sender, RoutedEventArgs e)
        {
            
            ((IEC104Destination)_cbIEC104Destinations.SelectedItem).CommonAdreses.Remove(((IEC104CommonAddress)_lbIEC104CommonAddresses.SelectedItem));
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (_columnsDictionary.ContainsKey( ((Control)sender).Name ))
            {
                if (_columnsDictionary[((Control)sender).Name].ContainsKey((string)e.Column.Header))
                {
                    e.Column.Header = _columnsDictionary[((Control)sender).Name][(string)e.Column.Header];
                }
                else
                {
                    e.Cancel = true;
                }
            }
            
        }




    }
}

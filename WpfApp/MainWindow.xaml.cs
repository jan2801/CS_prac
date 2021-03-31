using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassLibrary;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private V3MainCollection MainCollection { get; set; } = new V3MainCollection();
        public MainWindow()
        {

            InitializeComponent();

            //V3MainCollection MainCollection = new V3MainCollection();
            this.DataContext = MainCollection;
            lisBox_Main.ItemsSource = MainCollection;

        }



        private void DataOnGrid_filter(object sender, FilterEventArgs args)
        {
            var i = args.Item;
            if (i.GetType() == typeof(V3DataOnGrid))
                args.Accepted = true;
            else
                args.Accepted = false;
        } 

        private void NewClick(object sender, RoutedEventArgs e)
        {
            MainCollection = new V3MainCollection();
            DataContext = MainCollection;
            lisBox_Main.ItemsSource = (IEnumerable<V3Data>)DataContext;
            MessageBox.Show("New v3 main collection was created");
        }

        public void SaveChanges()
        {
            SaveFileDialog dlg = new SaveFileDialog();

            dlg.Filter = "(*.txt) | *.txt | All files(*.*) | *.*";
            dlg.FilterIndex = 2;

            if ((dlg.ShowDialog() == true) && (MainCollection != null))
                MainCollection.Save(dlg.FileName);
        }

        private bool Changes_Detected()
        {
            if ((MainCollection != null) && (MainCollection.IsChanged))
                return true;
            return false;
        }

        private void OpenClick(object sender, RoutedEventArgs e)
        {
            if (Changes_Detected())
            {
                MessageBox.Show("Changes are not saved");
                if (MessageBox.Show("Do you want to save changes?", " ", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    SaveChanges();


                    MessageBox.Show("Changes were saved");
                }

            }

            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "(*.txt) | *.txt | All files(*.*) | *.*";
            dlg.FilterIndex = 2;

            if (dlg.ShowDialog() == true)
            {
                MainCollection = new V3MainCollection();
                MainCollection.Load(dlg.FileName);
                //code here
            }
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            SaveChanges();
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            SaveChanges();
            

            MessageBox.Show("Main Window is closed");
        }

        private void AddDefaultsClick(object sender, RoutedEventArgs e)
        {
            MainCollection.AddDefaults();
        }

        private void AddDefaultClickV3DataCollection(object sender, RoutedEventArgs e)
        {
            V3DataCollection default_collection = new V3DataCollection("one default collection", new DateTime(2008, 5, 1, 8, 30, 52));
            default_collection.InitRandom(10, 8, 8, 0, 10);
            MessageBox.Show("Default data collection was created");
            MainCollection.Add(default_collection);

        }


        private void DataCollection_filter(object sender, FilterEventArgs args)
        {
            var i = args.Item;
            if (i.GetType() == typeof(V3DataCollection))
                args.Accepted = true;
            else
                args.Accepted = false;
        }
    
       
        

        private void AddDefaultClickV3DataOnGrid(object sender, RoutedEventArgs e)
        {
            V3DataOnGrid default_data_on_grid = new V3DataOnGrid("default data on grid", DateTime.Now, new Grid1D(20, 6), new Grid1D(20, 6));
            default_data_on_grid.InitRandom(0, 10);
            MessageBox.Show("Default data on grid was created");
            MainCollection.Add(default_data_on_grid);
        }

        private void AddV3DataOnGridFromFile(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();

            dlg.Filter = "(*.txt) | *.txt | All files(*.*) | *.*";
            dlg.FilterIndex = 2;

            if ((bool)dlg.ShowDialog())
            {
                V3DataOnGrid new_grid = new V3DataOnGrid(dlg.FileName);
                MainCollection.Add(new_grid);
            }
            else
            {
                MessageBox.Show("Please type file name");
            }
        }

        

        private void RemoveElement(object sender, RoutedEventArgs e)
        {
            if (lisBox_Main.SelectedItem is V3Data selected)
            {
                MessageBox.Show("Item will be removed");
                MainCollection.Remove(selected.measure, selected.date);
            }
        }

        


        
    }
}

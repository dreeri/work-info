using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Globalization;
using System.IO;

namespace TunnitApplikaatio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            textBox4.Text = Properties.Settings.Default.Useri;
        }

       
        private void button2_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string saveVar = Properties.Settings.Default.Useri;
                string dateVar = datePicker1.Text;
                Directory.CreateDirectory("C:/Wrap/");
                XmlTextWriter textWriter = new XmlTextWriter("C:/Wrap/" + saveVar + dateVar + ".xml", null);
                textWriter.WriteStartDocument();

                textWriter.WriteStartElement("General");
                textWriter.WriteStartElement("Date");
                textWriter.WriteString(datePicker1.Text);
                textWriter.WriteEndElement();
                textWriter.WriteStartElement("StartTime");
                textWriter.WriteString(textBox1.Text);
                textWriter.WriteEndElement();
                textWriter.WriteStartElement("EndTime");
                textWriter.WriteString(textBox2.Text);
                textWriter.WriteEndElement();
                textWriter.WriteStartElement("Infos");
                textWriter.WriteString(textBox3.Text);
                textWriter.WriteEndElement();
                textWriter.WriteEndDocument();
                textWriter.Close();

                //Saving user hours
                
                string startTime = textBox1.Text;
                string endTime = textBox2.Text;
                double basic = TimeSpan.Parse(textBox10.Text).TotalMinutes;
                int divider = 60;

                //Weird boolean statement for checkbox.
                int swits = 1;
                if (checkBox1.IsChecked == true)
                {
                    swits++;
                }

                double doubledStart = TimeSpan.Parse(startTime).TotalMinutes;
                double doubledEnd = TimeSpan.Parse(endTime).TotalMinutes;

                double subtractedTime = doubledEnd - doubledStart;
                
               
               
                double calcTime = subtractedTime * swits - basic;

                double oldTime = Properties.Settings.Default.TotalTime;
                double summarizedFinal = calcTime + oldTime;
                Properties.Settings.Default.TotalTime = summarizedFinal;
                double jeah = Properties.Settings.Default.TotalTime;
                double hour = jeah / divider;
                double minutes = jeah % divider;
                string gasd = minutes.ToString();
                int hourInt = (int)hour;
                string hoursConverted = hourInt.ToString();
                textBox9.Text = hoursConverted +":"+ gasd;

                //Uncomment to save bruttotime
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                global::System.Windows.MessageBox.Show("ErrorX" + ex.Source);
                Environment.Exit(1);
            }
            
           
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string saveVar = Properties.Settings.Default.Useri;
                string dateVar = datePicker2.Text;
                string readVar = "";
                XmlTextReader textReader = new XmlTextReader("C:/Wrap/" + saveVar + dateVar + ".xml");
                textReader.Read();
                while (textReader.Read())
                {
                    if (textReader.Name.Equals("Date"))
                    {
                        string conv = textReader.ReadElementString();
                        string dateS = "Date: ";
                        readVar += dateS + conv;
                    }
                    if (textReader.Name.Equals("StartTime"))
                    {
                        string conv = textReader.ReadElementString();
                        string startS = " Tulo: ";
                        readVar += startS + conv;
                    }
                    if (textReader.Name.Equals("EndTime"))
                    {
                        string conv = textReader.ReadElementString();
                        string endS = " Lähtö: ";
                        readVar += endS + conv;
                    }
                    if (textReader.Name.Equals("Infos"))
                    {
                        string conv = textReader.ReadElementString();
                        string infoS = " Info: ";
                        readVar += infoS + conv;
                    }
                    
                    textReader.MoveToElement();
                }
                string brakekString = "---------";
                listBox1.Items.Add(readVar);
                listBox1.Items.Add(brakekString);
                
            }
            catch (Exception ex)
            {
                global::System.Windows.MessageBox.Show(ex.Source); ;
                Environment.Exit(1);
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            string astring = textBox4.Text.ToString();
            Properties.Settings.Default.Useri = astring;
            Properties.Settings.Default.TotalTime = 0;
            
            Properties.Settings.Default.Save();
           
        }

   

     

        
    }
}

using System;
using System.Collections.Generic;
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
using System.Xml;
using System.IO;
using System.Web;
namespace weatherApp
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string Temperature;
        string Condition;
        string Humidity;
        string Windspeed;
        string Town;
        string Region;
        string Country;
        string Code;
        string Local;
        string woeid;
        string LastUpdate;
        string[] next_day = new string[10];
        string[] next_cond = new string[10];
        string[] next_condt = new string[10];
        string[] next_high = new string[10];
        string[] next_low = new string[10];

        public MainWindow()
        {
            InitializeComponent();
            woeid = "721244";
            getWeather();
            label1.Content = Temperature;
            label2.Content = Town + ", " + Region;
            label6.Content = Condition;
            label7.Content = Humidity;
            label8.Content = Windspeed;
            label10.Content = string.Format("\u00B0") + "F";
            label12.Content = next_day[1];
            label15.Content = next_day[2];
            label17.Content = next_day[3];
            label19.Content = next_day[4];
            label20.Content = Country;
            label13.Content = next_condt[1];
            label14.Content = next_condt[2];
            label16.Content = next_condt[3];
            label18.Content = next_condt[4];

            label21.Content = next_high[1] + string.Format("\u00B0") + "F";
            label24.Content = next_high[2] + string.Format("\u00B0") + "F";
            label26.Content = next_high[3] + string.Format("\u00B0") + "F";
            label28.Content = next_high[4] + string.Format("\u00B0") + "F";
            label22.Content = next_low[1] + string.Format("\u00B0") + "F";
            label23.Content = next_low[2] + string.Format("\u00B0") + "F";
            label25.Content = next_low[3] + string.Format("\u00B0") + "F";
            label27.Content = next_low[4] + string.Format("\u00B0") + "F";
           

            comboBox1.Items.Add("Rome");
            comboBox1.Items.Add("Milan");
            comboBox1.Items.Add("Turin");
            comboBox1.Items.Add("Florence");
            comboBox1.Items.Add("Naples");
            comboBox1.Items.Add("Imola");
            comboBox1.Items.Add("Palermo");
            comboBox1.Items.Add("New York");
            comboBox1.Items.Add("Los Angeles");
            comboBox1.Items.Add("London");
            comboBox1.Items.Add("Paris");

        }
      
        private void getWeather()
        {
            String st = String.Format(@"http://weather.yahooapis.com/forecastrss?w=");
            st = st + woeid;
            XmlDocument wData = new XmlDocument();
            wData.Load(st);
            XmlNamespaceManager manager = new XmlNamespaceManager(wData.NameTable);
            manager.AddNamespace("yweather", @"http://xml.weather.yahoo.com/ns/rss/1.0");
            XmlNode channel = wData.SelectSingleNode("rss").SelectSingleNode("channel");
            Temperature = channel.SelectSingleNode("item").SelectSingleNode("yweather:condition", manager).Attributes["temp"].Value;
            Condition = channel.SelectSingleNode("item").SelectSingleNode("yweather:condition", manager).Attributes["text"].Value;
            Code = channel.SelectSingleNode("item").SelectSingleNode("yweather:condition", manager).Attributes["code"].Value;
            Humidity = channel.SelectSingleNode("yweather:atmosphere", manager).Attributes["humidity"].Value;
            Windspeed = channel.SelectSingleNode("yweather:wind", manager).Attributes["speed"].Value;
            Town = channel.SelectSingleNode("yweather:location", manager).Attributes["city"].Value;
            Region = channel.SelectSingleNode("yweather:location", manager).Attributes["region"].Value;
            Country = channel.SelectSingleNode("yweather:location", manager).Attributes["country"].Value;
            Local = channel.SelectSingleNode("item").SelectSingleNode("title", manager).InnerXml;
            LastUpdate = channel.SelectSingleNode("item").SelectSingleNode("pubDate", manager).InnerXml;
            XmlNodeList forecast = channel.SelectSingleNode("item").SelectNodes("yweather:forecast", manager);
            setIcon();
            for (int i = 0; i < forecast.Count; i++)
            {
                next_day[i] = forecast[i].Attributes["day"].Value;
                next_cond[i] = forecast[i].Attributes["code"].Value;
                next_condt[i] = forecast[i].Attributes["text"].Value;
                next_high[i] = forecast[i].Attributes["high"].Value;
                next_low[i] = forecast[i].Attributes["low"].Value;
            }

            label11.Content = "Last Updated on : " + LastUpdate;
            double x = (Double.Parse(Temperature) - 32) * 5.0 / 9.0;
            x = (int)x;
            label9.Content = x.ToString() + " " + string.Format("\u00B0") + "C";
        }
      
      

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            
            switch (comboBox1.Text)
            {
                case "Rome": woeid = "721943"; break;
                case "Milan": woeid = "718345"; break;
                case "Turin": woeid = "725003"; break;
                case "Florence": woeid = "715496"; break;
                case "Naples": woeid = "719258"; break;
                case "Imola": woeid = "716645"; break;
                case "Palermo": woeid = "719846"; break;
                case "Los Angeles": woeid = "2442047"; break;
                case "London": woeid = "44418"; break;
                case "Paris": woeid = "615702"; break;
                case "New York": woeid = "2459115"; break;
                default: woeid = comboBox1.Text; break;
            }
            getWeather();
            label1.Content = Temperature;
            label2.Content = Town + ", " + Region;
            label6.Content = Condition;
            label7.Content = Humidity;
            label8.Content = Windspeed;
            label10.Content = string.Format("\u00B0") + "F";
            label12.Content = next_day[1];
            label15.Content = next_day[2];
            label17.Content = next_day[3];
            label19.Content = next_day[4];
            label20.Content = Country;
            label13.Content = next_condt[1];
            label14.Content = next_condt[2];
            label16.Content = next_condt[3];
            label18.Content = next_condt[4];
        }

        private void button2_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void setIcon()
        {
            if (Code.Equals("3200"))
            {
                image.Source = new BitmapImage(new Uri(@"/Pics/na.png", UriKind.Relative));
             
            }
            else
            {
               
                image.Source = new BitmapImage(new Uri(@"/Pics/" + Code + ".png", UriKind.Relative));
              
            }
        }
    }
  
    
}

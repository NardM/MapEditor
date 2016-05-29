using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using WindowsFormsApplication3.Properties;
using YandexAPI.Maps;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        private bool _f = true;
        private double _longitude ;
        private double _lititude;
        private double _stepSize = 0.005F;
        private readonly int _width = 650;
        private readonly int _height = 450;
        private string _flags = "";
        private string _line = "";


        public Form1()
        {
            InitializeComponent();

        }

        public void CreateFlags(List<V> v)
        {
            foreach (var vn in v)
            {
                _flags += vn.Longitude.ToString(CultureInfo.InvariantCulture).Replace(',', '.') + "," + vn.Lititude.ToString(CultureInfo.InvariantCulture).Replace(',', '.') + ",pmors" + vn.v + "~";
            }
            _flags = _flags.Substring(0, _flags.Length - 1);
        }

        public void CreateLine(List<int> path, List<V> v)
        {
            _line = "&pl=";
            foreach (var x in path)
            {
                _line += v[x - 1].Longitude.ToString(CultureInfo.CurrentCulture).Replace(',', '.') + "," + v[x - 1].Lititude.ToString(CultureInfo.InvariantCulture).Replace(',', '.') + ",";
            }
            _line = _line.Substring(0, _line.Length - 1);
        }

        public void Solve()
        {
            _flags = null;
            _line = null;
                
            var v =
                FileManeger.BuildGraph(
                    @"designation.txt",
                    @"graph.txt");
            CreateFlags(v);
            try
            {
                var v1 = int.Parse(textBox6.Text);
                var v2 = int.Parse(textBox1.Text);
                CreateLine(new Dijkstra(v).SerchPath(v1, v2), v);
            }
            catch (Exception)
            {
                // ignored
            }

            var geoCode = new GeoCode();

            var str = geoCode.SearchObject("Ульяновск");

            var s = geoCode.GetPoint(str);

            var lenghtWidth = s.Split(',');
            if (_f)
            {
                _longitude = double.Parse(lenghtWidth[0].Replace('.', ','));
                _lititude = double.Parse(lenghtWidth[1].Replace('.', ','));
                _f = false;
            }
            var longitudeString = _longitude.ToString(CultureInfo.InvariantCulture).Replace(',', '.');
            var lititudeString = _lititude.ToString(CultureInfo.InvariantCulture).Replace(',', '.');
            var image = @"https://static-maps.yandex.ru/1.x/?l="
                           + "map" + "&ll=" + longitudeString
                           + "," + lititudeString + "&z=" + int.Parse(comboBox1.Text)
                           + "&size=" + _width + "," + _height
                           + "&pt=" + _flags + _line;
            pictureBox1.Image = geoCode.DownloadMapImage(image);
            textBox2.Text = _longitude.ToString(CultureInfo.InvariantCulture);
            textBox3.Text = _lititude.ToString(CultureInfo.InvariantCulture);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _f = true;
            Solve();
           
        }

        private void Point(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Очистить(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox6.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = Resources.Form1_button2_Click__17;
            pictureBox1.Image = null;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Solve();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Up(object sender, EventArgs e)
        {
            _lititude += _stepSize;
            Solve();
        }

        private void Right(object sender, EventArgs e)
        {
            _longitude +=  _stepSize;
            Solve();
        }

        private void Left(object sender, EventArgs e)
        {
            _longitude -= _stepSize;
            Solve();
        }

        private void Down(object sender, EventArgs e)
        {
            _lititude -=  _stepSize;
            Solve();
        }

       

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {
            var str = textBox4.Text.Replace(".", ",");
            _stepSize = double.Parse(str);
        }

      

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

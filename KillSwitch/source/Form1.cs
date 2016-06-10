using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace KillSwitch
{
    public partial class Form1 : Form
    {
        List<string> lstFiles = new List<string>();
        string filePath = @"C:\test.xml";
        public Form1()
        {
            InitializeComponent();
            lstFiles = GetConfiguration(filePath);
            dataGridView1.DataSource = lstFiles.Select(x => new { FileName = x }).ToList();
        }

        private void SetConfiguration(List<string> lstConfig, string filePath)
        {
            XmlSerializer _serializer = new XmlSerializer(typeof(List<string>));
            using (var _stream = File.OpenWrite(filePath))
            {
                _serializer.Serialize(_stream, lstConfig);
            }
        }

        private List<string> GetConfiguration(string filePath)
        {
            List<string> _lstConfig;
            XmlSerializer _serializer = new XmlSerializer(typeof(List<string>));
            using (var _stream = File.OpenRead(filePath))
            {
                _lstConfig = (List<string>)_serializer.Deserialize(_stream);
                return _lstConfig;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog _fileDialog = new OpenFileDialog();
            _fileDialog.Multiselect = true;
            if (_fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = _fileDialog.FileName;
                if (!lstFiles.Contains(textBox1.Text))
                {
                    lstFiles.Add(textBox1.Text);
                }
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = lstFiles.Select(x => new { FileName = x }).ToList();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SetConfiguration(lstFiles, filePath);
        }
    }
}

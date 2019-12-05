using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Windows.Forms;

namespace TesteHardcore
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string[]> dados = new List<string[]>();
            SpedDAO FiscalDao = new SpedDAO();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string[] line = File.ReadAllLines(openFileDialog1.FileName);
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i].StartsWith("|") && line[i].EndsWith("|"))
                        {
                            string[] separacao = line[i].Split('|');
                            dados.Add(separacao);
                            if (separacao[1] == "0005")
                            {
                                MessageBox.Show("PisConfis");

                            }
                            else
                            {
                                FiscalDao.Inserir(separacao, openFileDialog1);
                            }
                        }

                    }
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" + $"Details:\n\n{ex.StackTrace}");
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebApplication1;

namespace WebScraping
{
    public partial class Form1 : Form
    {
        public WebScraping ws;
        public string activity;
        public string ville;
        public WebService1 webservice;
        

        public Form1()
        {
            InitializeComponent();
            ws = new WebScraping();
            this.webservice = new WebService1();
   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var url = "";

            if (textBox1.Text == "")
            {
                MessageBox.Show("Le champ correspondant au secteur d'activité ne peut pas être vide", "Erreur dans la saisie");
            }
            else if (textBox2.Text == "" && textBox3.Text == "")
            {
                MessageBox.Show("Le champ correspondant à la ville ou au code postal doit être remplis au choix");
            }
            else
            {

                if (radioButton1.Checked)
                {
                    if (textBox2.Text == "")
                    {
                        url = "https://www.pagesjaunes.fr/annuaire/departement/" + textBox3.Text.Substring(0, 2) + "/" + textBox1.Text + "";
                    }
                    else
                    {
                        url = "https://www.pagesjaunes.fr/annuaire/chercherlespros?quoiqui=" + textBox1.Text + "&ou=" + textBox2.Text + "&univers=pagesjaunes&idOu=&acOuSollicitee=1&nbPropositionOuTop=5&nbPropositionOuHisto=0&ouSaisi=" + textBox2.Text + "&acQuiQuoiSollicitee=0&nbPropositionQuiQuoiTop=0&nbPropositionQuiQuoiHisto=0&nbPropositionQuiQuoiGeo=0&quiQuoiSaisi=" + textBox1.Text + "";
                    }

                    ws.ScrapeData(url, textBox1.Text);
                    dataGridView1.DataSource = ws.Scraper;

                    dataGridView1.Columns[2].HeaderCell.Value = "Code Postal";

                }
                else if (radioButton2.Checked)
                {
                    if(textBox2.Text == "")
                    {
                        dataGridView1.DataSource = webservice.GetDataByZip(textBox3.Text, textBox1.Text);
                    }
                    else if(textBox3.Text == "")
                    {
                        dataGridView1.DataSource = webservice.GetDataByCity(textBox2.Text.ToUpper(), textBox1.Text);
                    }
                    else if(textBox2.Text != "" && textBox3.Text != "")
                    {
                        dataGridView1.DataSource = webservice.FilterData(textBox1.Text, textBox2.Text.ToUpper(), textBox3.Text);
                    }

                    dataGridView1.Columns[0].Visible = false;

                    dataGridView1.Columns[1].HeaderCell.Value = "Entreprise";
                    dataGridView1.Columns[2].HeaderCell.Value = "Adresse";
                    dataGridView1.Columns[3].HeaderCell.Value = "Code Postal";
                    dataGridView1.Columns[4].HeaderCell.Value = "Ville";


                    dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    dataGridView1.Columns[4].HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
                }
                else
                {
                    MessageBox.Show("Une des checkbox doit être cochée");
                }
            }

            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11);

            dataGridView1.Columns[0].HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
            dataGridView1.Columns[1].HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
            dataGridView1.Columns[2].HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
            dataGridView1.Columns[3].HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);



            label1.AutoSize = false;
            dataGridView1.Update();
            dataGridView1.Refresh();



            

        }
    }
}

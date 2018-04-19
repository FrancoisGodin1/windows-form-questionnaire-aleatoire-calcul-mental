using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestScholaNova
{
    public partial class Fresultat : Form
    {
        private Fquestionnaire f;
        public Fresultat(int score, string temps,Fquestionnaire f)
        {
            InitializeComponent();
            this.f = f;
            lblResult.Text = $@"Votre score : {score} / 10";
            lblTime.Text = "Votre temps : " + temps;
            this.Show();
            btnExit.Click += BtnExit_Click;
            btnReload.Click += BtnReload_Click;
        }

        private void BtnReload_Click(object sender, EventArgs e)
        {
            this.Close();
            f = new Fquestionnaire();
            f.Show();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

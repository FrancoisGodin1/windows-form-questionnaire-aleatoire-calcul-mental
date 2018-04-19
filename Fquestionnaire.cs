using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace TestScholaNova
{
    public partial class Fquestionnaire : Form
    {
        private List<string> _questionsAlreadyAsked = new List<string>();
        private static int _score;
        private static int _tentative;
        private double _resultat;
        private Stopwatch watch = new Stopwatch();

        public Fquestionnaire()
        {
            InitializeComponent();
            tbProposition.MaxLength = 3;
            _tentative = 0;
            _score = 0;
            btnStart.Click += BtnStart_Click;
            btnValider.Click += new EventHandler(BtnValider_Click);
            tbProposition.KeyPress += new KeyPressEventHandler(TbProposition_KeyPress);
            timerQuestionnaire.Tick += TimerQuestionnaire_Tick;
            gbQuestion.Hide();
        }

        private void TimerQuestionnaire_Tick(object sender, EventArgs e)
        {
            TimeSpan time = watch.Elapsed;
            lbTimeValue.Text = time.ToString(@"mm\:ss\:ff");
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            btnStart.Hide();
            lblEnonceText.Hide();
            GetRandomOperation();
            gbQuestion.Show();
            timerQuestionnaire.Start();
            watch.Start();
        }

        private void TbProposition_KeyPress(object sender, KeyPressEventArgs e)
        {
            //controle de la saisie du champ de proposition
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) &&(e.KeyChar != '-');
        }

        private void BtnValider_Click(object sender, EventArgs e)
        {
            _tentative = _tentative + 1;
            if (tbProposition.Text == _resultat.ToString())
                _score = _score + 1;
            if (_tentative >= 10)
            {
                TimeSpan time = watch.Elapsed;
                //questionnaire fini on affiche le resultat dans la fenetre resultat
                btnValider.Enabled = false;
                this.Hide();
                Fresultat fresultat = new Fresultat(_score,time.ToString(@"mm\:ss\:ff"),this);
            }
            else
                GetRandomOperation();
            tbProposition.Clear();
        }

        private void GetRandomOperation()
        { 
            Question randomQuestion = new Question();
            while (_questionsAlreadyAsked.Contains(randomQuestion.ToString())) // si la question est déjà posée on en réinstancie une 
                randomQuestion = new Question();

            _resultat = randomQuestion.GetResultat();

            _questionsAlreadyAsked.Add(randomQuestion.ToString());
            lblOperation.Text = randomQuestion.ToString();
        }

    }
}
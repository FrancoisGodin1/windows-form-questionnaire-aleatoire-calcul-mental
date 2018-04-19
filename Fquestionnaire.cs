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
            btnValider.Click += BtnValider_Click;
            tbProposition.KeyPress += TbProposition_KeyPress;
            timerQuestionnaire.Tick += TimerQuestionnaire_Tick;
            gbQuestion.Hide(); // je cache dans un 1er temps le groupBox question
        }

        private void TimerQuestionnaire_Tick(object sender, EventArgs e)
        {
            TimeSpan time = watch.Elapsed;
            lbTimeValue.Text = time.ToString(@"mm\:ss\:ff");
        }

        /// <summary>
        /// affiche la 1ere question et demarre le chrono
        /// </summary>
        private void BtnStart_Click(object sender, EventArgs e)
        {
            btnStart.Hide(); 
            lblEnonceText.Hide();
            GetRandomOperation();
            gbQuestion.Show(); // affichage question 
            timerQuestionnaire.Start();
            watch.Start();
        }
        /// <summary>
        /// filtre la saisie du textBox et acepte seulement les chiffres et tirets
        /// </summary>
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
                TimeSpan time = watch.Elapsed; //recupere le temps final 
                btnValider.Enabled = false;
                this.Hide(); // et on cache la current window
                Fresultat fresultat = new Fresultat(_score,time.ToString(@"mm\:ss\:ff"),this);
            }
            else
                GetRandomOperation();
            tbProposition.Clear();
        }
        /// <summary>
        /// initialise un objet question
        /// controle si la question a déja été posée
        /// set le label question 
        /// recupere le resultat de la q
        /// </summary>
        private void GetRandomOperation()
        { 
            Question randomQuestion = new Question();

            // si la question est déjà posée on en réinstancie une
            while (_questionsAlreadyAsked.Contains(randomQuestion.ToString()))  
                randomQuestion = new Question();

            _resultat = randomQuestion.GetResultat(); // on set le resultat

            _questionsAlreadyAsked.Add(randomQuestion.ToString()); //add la question a la liste des questions déjà posées
            lblOperation.Text = randomQuestion.ToString(); // set la question dans le label question
        }

    }
}
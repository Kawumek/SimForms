using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimForms
{
    public partial class MainForm : Form
    {
        private void SaveGame()
        {
            Properties.Settings save = new Properties.Settings
            {
                health = HealthProgress.Value,
                hunger = HungerProgress.Value,
                money = Convert.ToInt32(MoneyBox.Text),
                day = Convert.ToInt32(DayBox.Text),
                time = TimeBoxValue()
            };
            save.Save();
        }
        private void LoadGame()
        {
            Properties.Settings loadsaves = new Properties.Settings();
            HealthProgress.Value = loadsaves.health;
            HungerProgress.Value = loadsaves.hunger;
            MoneyBox.Text = loadsaves.money.ToString();
            DayBox.Text = loadsaves.day.ToString();
            TimeBoxValue(loadsaves.time);
        }

        private int TimeBoxValue()
        {
            if (TimeBox.Text == "Ночь") return 0;
            else if (TimeBox.Text == "Утро") return 1;
            else if (TimeBox.Text == "День") return 2;
            else if (TimeBox.Text == "Вечер") return 3;
            else return 0;
        }
        private void TimeBoxValue(int type)
        {
            switch (type)
            {
                case 0:
                    TimeBox.Text = "Ночь";
                    break;
                case 1:
                    TimeBox.Text = "Утро";
                    break;
                case 2:
                    TimeBox.Text = "День";
                    break;
                case 3:
                    TimeBox.Text = "Вечер";
                    break;
            }
        }

        public MainForm()
        {
            InitializeComponent();
            LoadGame();
        }

        private void HouseDButton_Click(object sender, EventArgs e)
        {

        }

        private void HouseCButton_Click(object sender, EventArgs e)
        {

        }

        private void VehiclesButton_Click(object sender, EventArgs e)
        {

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveGame();
        }

        private void SupermarketCButton_Click(object sender, EventArgs e)
        {

        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            var close = MessageBox.Show(null, "Весь Ваш прогресс будет удален, если вы не сохранились.\nВы уверены, что хотите закрыть программу?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if(close == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void NextDayButton_Click(object sender, EventArgs e)
        {
            if (TimeBoxValue() != 3) TimeBoxValue(TimeBoxValue() + 1);
            else
            {
                TimeBoxValue(0);
                DayBox.Text = (Convert.ToInt32(DayBox.Text) + 1).ToString();
            }
            SaveGame();
        }

    }
}

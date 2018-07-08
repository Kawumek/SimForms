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
            Properties.stats save = new Properties.stats
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
            Properties.stats loadsaves = new Properties.stats();
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

        private bool GetDisease()
        {
            Random random = new Random();
            if (HealthProgress.Value > 75)
            {
                if (random.NextDouble() <= 0.05) return true;
            }
            else if (HealthProgress.Value > 50)
            {
                if (random.NextDouble() <= 0.1) return true;
            }
            else if (HealthProgress.Value > 25)
            {
                if (random.NextDouble() <= 0.25) return true;
            }
            else
            {
                if (random.NextDouble() <= 0.5) return true;
            }
            return false;
        }

        public MainForm()
        {
            InitializeComponent();
            LoadGame();
            Properties.Settings prestart = new Properties.Settings();
            if (prestart.infoBox)
            {
                var result = MessageBox.Show(null, "Хотите ли вы, чтобы весь Ваш прогресс сохранялся автоматически после каждого дня?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.No) prestart.autosave = true;
                prestart.infoBox = false;
                prestart.Save();
            }
            this.Text = "SimForms: v" + prestart.version;
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
            MessageBox.Show(null, "Весь Ваш прогресс был успешно сохранён!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SupermarketCButton_Click(object sender, EventArgs e)
        {

        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            var close = MessageBox.Show(null, "Весь Ваш прогресс будет удален, если вы не сохранились.\nВы уверены, что хотите закрыть программу?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (close == DialogResult.Yes) this.Close();
        }

        private void NextDayButton_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            HealthProgress.Value -= random.Next(3);
            if (GetDisease()) MessageBox.Show(null, "Из-за недостаточной активности имунной системы Вы простудились, теперь Ваш иммунитет будет слабеть намного быстрее, также Вам необоходимо покупать лекарства для лечения в течении 5 дней.", "Болезнь!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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

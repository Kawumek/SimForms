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
        #region Service
        /// <param name="chance">Range: 0.0 to 1.0</param>
        private bool Try(double chance)
        {
            var random = new Random();
            if (random.NextDouble() <= chance) return true;
            else return false;
        }
        
        private int RandomInt()
        {
            Random random = new Random();
            return random.Next();
        }
        private int RandomInt(int max)
        {
            Random random = new Random();
            return random.Next(max);
        }
        private int RandomInt(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        private double RandomDouble()
        {
            Random random = new Random();
            return random.NextDouble();
        }
        #endregion

        #region SaveGames Manager
        private int homeid;
        private void SaveGame()
        {
            Properties.stats stats = new Properties.stats
            {
                health = HealthProgress.Value,
                hunger = FoodProgress.Value,
                money = Convert.ToInt32(MoneyBox.Text),
                day = Convert.ToInt32(DayBox.Text),
                time = TimeBoxValue(),
                homeid = homeid
            };
            stats.Save();
        }
        private void LoadGame()
        {
            Properties.stats loadsaves = new Properties.stats();
            HealthProgress.Value = loadsaves.health;
            FoodProgress.Value = loadsaves.hunger;
            MoneyBox.Text = loadsaves.money.ToString();
            DayBox.Text = loadsaves.day.ToString();
            TimeBoxValue(loadsaves.time);

            homeid = loadsaves.homeid;
        }
        private void SetDefaults()
        {
            Properties.stats stats = new Properties.stats
            {
                health = 80,
                hunger = 70,
                money = 20.00,
                day = 1,
                time = 0
            };
            stats.Save();
        }
        #endregion

        #region TimeBox
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
        #endregion

        #region Other Stuff
        private void GetDisease()
        {
            Random random = new Random();
            if (HealthProgress.Value > 75)
            {

                if (Try(0.01)) MessageBox.Show(null, "Из-за недостаточной активности имунной системы Вы простудились, теперь Ваш иммунитет будет слабеть намного быстрее, также Вам необоходимо покупать лекарства для лечения в течении 5 дней.", "Болезнь!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (HealthProgress.Value > 50)
            {
                if (Try(0.03)) MessageBox.Show(null, "Из-за недостаточной активности имунной системы Вы простудились, теперь Ваш иммунитет будет слабеть намного быстрее, также Вам необоходимо покупать лекарства для лечения в течении 5 дней.", "Болезнь!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (HealthProgress.Value > 25)
            {
                if (Try(0.1)) MessageBox.Show(null, "Из-за недостаточной активности имунной системы Вы простудились, теперь Ваш иммунитет будет слабеть намного быстрее, также Вам необоходимо покупать лекарства для лечения в течении 5 дней.", "Болезнь!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (Try(0.3)) MessageBox.Show(null, "Из-за недостаточной активности имунной системы Вы простудились, теперь Ваш иммунитет будет слабеть намного быстрее, также Вам необоходимо покупать лекарства для лечения в течении 5 дней.", "Болезнь!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void HomeBonus()
        {
            if (homeid == 0 && Try(0.2))
            {
                HealthProgress.Value -= RandomInt(2, 5);
                Properties.stats stats = new Properties.stats();
                MessageBox.Show(null, "Из-за отсутствия дома вы потеряли " + (stats.health - HealthProgress.Value) + " ед. иммунитета", "Плохие условия!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (homeid==1 && Try(0.05))
            {
                HealthProgress.Value -= RandomInt(1, 3);
                Properties.stats stats = new Properties.stats();
                MessageBox.Show(null, "Из-за плохих условий проживания вы потеряли " + (stats.health - HealthProgress.Value) + " ед. иммунитета", "Плохие условия!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

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

        #region Clicks
        private void HouseDButton_Click(object sender, EventArgs e)
        {

        }

        private void HouseCButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(null, "", "Дом эконом класса");
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
            if (TimeBoxValue() != 3) TimeBoxValue(TimeBoxValue() + 1);
            else
            {
                HealthProgress.Value -= RandomInt(0,2);
                if (FoodProgress.Value >= 80) HealthProgress.Value += RandomInt(2, 5);
                else if (FoodProgress.Value >= 60) HealthProgress.Value += RandomInt(0, 2);
                GetDisease();
                HomeBonus();
                TimeBoxValue(0);
                DayBox.Text = (Convert.ToInt32(DayBox.Text) + 1).ToString();
                var stats = new Properties.stats();
                MessageBox.Show(null, "Настал " + DayBox.Text + " день.\nВаш иммунитет изменился на " + (HealthProgress.Value - stats.health) + " ед.\nВаш уровень сытости изменился на " + (FoodProgress.Value - stats.hunger) + " ед.", "Следующий день!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SaveGame();
            }
        }

        private void newProfileItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(null, "Вы потеряете весь свой прогресс!\nВы уверены, что хотите начать заного?", "Внимание!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK) SetDefaults();
            LoadGame();
        }
        #endregion
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;

namespace MemoGame
{
    public class DifficultyForm : Form
    {
        public DifficultyForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Выбор сложности";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightYellow;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Заголовок
            Label title = new Label();
            title.Text = "Выберите сложность";
            title.Font = new Font("Arial", 22, FontStyle.Bold);
            title.ForeColor = Color.DarkOrange;
            title.Size = new Size(400, 50);
            title.Location = new Point(100, 40);
            title.TextAlign = ContentAlignment.MiddleCenter;

            // Описание сложностей
            Label descEasy = new Label();
            descEasy.Text = "4x4 карточки\n8 пар";
            descEasy.Font = new Font("Arial", 12);
            descEasy.Size = new Size(200, 50);
            descEasy.Location = new Point(300, 130);

            Label descMedium = new Label();
            descMedium.Text = "4x5 карточек\n10 пар";
            descMedium.Font = new Font("Arial", 12);
            descMedium.Size = new Size(200, 50);
            descMedium.Location = new Point(300, 220);

            Label descHard = new Label();
            descHard.Text = "5x6 карточек\n15 пар";
            descHard.Font = new Font("Arial", 12);
            descHard.Size = new Size(200, 50);
            descHard.Location = new Point(300, 310);

            // Кнопки сложности
            Button btnEasy = new Button();
            btnEasy.Text = " Легко";
            btnEasy.Font = new Font("Arial", 14, FontStyle.Bold);
            btnEasy.Size = new Size(200, 70);
            btnEasy.Location = new Point(50, 120);
            btnEasy.BackColor = Color.LightGreen;
            btnEasy.Click += (s, e) => StartGame(GameMode.Easy);

            Button btnMedium = new Button();
            btnMedium.Text = " Средне";
            btnMedium.Font = new Font("Arial", 14, FontStyle.Bold);
            btnMedium.Size = new Size(200, 70);
            btnMedium.Location = new Point(50, 210);
            btnMedium.BackColor = Color.Gold;
            btnMedium.Click += (s, e) => StartGame(GameMode.Medium);

            Button btnHard = new Button();
            btnHard.Text = " Сложно";
            btnHard.Font = new Font("Arial", 14, FontStyle.Bold);
            btnHard.Size = new Size(200, 70);
            btnHard.Location = new Point(50, 300);
            btnHard.BackColor = Color.LightCoral;
            btnHard.Click += (s, e) => StartGame(GameMode.Hard);

            this.Controls.AddRange(new Control[] {
                title, btnEasy, btnMedium, btnHard, descEasy, descMedium, descHard
            });
        }

        private void StartGame(GameMode difficulty)
        {
            MemoGameForm gameForm = new MemoGameForm(difficulty);
            gameForm.Show();
            this.Close();
        }
    }
}

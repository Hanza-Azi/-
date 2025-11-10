using System;
using System.Drawing;
using System.Windows.Forms;

namespace MemoGame
{
    public class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Мемо Игра - Главное меню";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;            
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            try
            {
                // Загружаем ваше фоновое изображение"
                this.BackgroundImage = Image.FromFile(@"C:\Users\lidzi\Downloads\skay2.jpg");
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (Exception)
            {
                  // Если изображение не найдено, используем градиентный фон
                this.BackColor = Color.Black;
            }
            // Полупрозрачная панель для кнопок
            Panel buttonPanel = new Panel();
            buttonPanel.BackColor = Color.FromArgb(200, 255, 255, 255); // полупрозрачный белый
            buttonPanel.Size = new Size(400, 350);
            buttonPanel.Location = new Point(200, 125);
            buttonPanel.Padding = new Padding(20);

            // Заголовок
            Label title = new Label();
            title.Text = "МЕМО ИГРА"; 
            title.Font = new Font("Arial", 32, FontStyle.Bold);
            title.ForeColor = Color.DarkBlue;
            title.Size = new Size(400, 60);
            title.Location = new Point(200, 80);
            title.TextAlign = ContentAlignment.MiddleCenter;

            // Кнопка "Быстрая игра"
            Button btnQuickGame = new Button();
            btnQuickGame.Text = " Быстрая игра";
            btnQuickGame.Font = new Font("Arial", 16, FontStyle.Bold);
            btnQuickGame.Size = new Size(300, 60);
            btnQuickGame.Location = new Point(250, 180);
            btnQuickGame.BackColor = Color.Gold;
            btnQuickGame.Click += (s, e) => StartGame(GameMode.Quick);

            // Кнопка "Выбор сложности"
            Button btnDifficulty = new Button();
            btnDifficulty.Text = " Выбор сложности";
            btnDifficulty.Font = new Font("Arial", 16, FontStyle.Bold);
            btnDifficulty.Size = new Size(300, 60);
            btnDifficulty.Location = new Point(250, 260);
            btnDifficulty.BackColor = Color.LightGreen;
            btnDifficulty.Click += (s, e) => ShowDifficultyMenu();

            // Кнопка "Выход"
            Button btnExit = new Button();
            btnExit.Text = " Выход";
            btnExit.Font = new Font("Arial", 16, FontStyle.Bold);
            btnExit.Size = new Size(300, 60);
            btnExit.Location = new Point(250, 340);
            btnExit.BackColor = Color.LightCoral;
            btnExit.Click += (s, e) => Application.Exit();

            this.Controls.AddRange(new Control[] { title, btnQuickGame, btnDifficulty, btnExit });
        }

        private void StartGame(GameMode mode)
        {
            MemoGameForm gameForm = new MemoGameForm(mode);
            gameForm.Show();
            this.Hide();

            gameForm.FormClosed += (s, e) => this.Show();
        }

        private void ShowDifficultyMenu()
        {
            DifficultyForm difficultyForm = new DifficultyForm();
            difficultyForm.ShowDialog();
        }
    }

    public enum GameMode
    {
        Quick,
        Easy,
        Medium,
        Hard
    }
}
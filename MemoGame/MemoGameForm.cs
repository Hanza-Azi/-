using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MemoGame
{
    public class MemoGameForm : Form
    {
        private List<Button> cards = new List<Button>();
        private Button firstCard = null;
        private Button secondCard = null;
        private int pairsFound = 0;
        private int totalPairs = 8;
        private int movesCount = 0;
        private int timeElapsed = 0;
        private Timer gameTimer;
        private Label lblMoves, lblTime, lblPairs, lblDifficulty;
        private GameMode currentMode;

        public MemoGameForm(GameMode mode)
        {
            currentMode = mode;
            InitializeComponent();
            StartNewGame();
        }

        private void InitializeComponent()
        {
            this.Text = "Мемо Игра";
            this.Size = GetFormSize();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            CreateControls();
        }

        private Size GetFormSize()
        {
            if (currentMode == GameMode.Easy)
                return new Size(800, 800);
            else if (currentMode == GameMode.Medium)
                return new Size(900, 800);
            else if (currentMode == GameMode.Hard)
                return new Size(1000, 850);
            else
                return new Size(800, 800);
        }

        private void CreateControls()
        {
            // Панель информации
            Panel infoPanel = new Panel();
            infoPanel.Size = new Size(this.ClientSize.Width, 100);
            infoPanel.Location = new Point(0, 0);
            infoPanel.BackColor = Color.LightGray;

            // Метка сложности
            lblDifficulty = new Label();
            lblDifficulty.Text = GetDifficultyText();
            lblDifficulty.Font = new Font("Arial", 14, FontStyle.Bold);
            lblDifficulty.ForeColor = Color.DarkBlue;
            lblDifficulty.Size = new Size(200, 25);
            lblDifficulty.Location = new Point(30, 20);

            // Счетчик ходов
            lblMoves = new Label();
            lblMoves.Text = "Ходы: 0";
            lblMoves.Font = new Font("Arial", 12, FontStyle.Bold);
            lblMoves.Size = new Size(120, 25);
            lblMoves.Location = new Point(30, 55);

            // Таймер
            lblTime = new Label();
            lblTime.Text = "Время: 00:00";
            lblTime.Font = new Font("Arial", 12, FontStyle.Bold);
            lblTime.Size = new Size(120, 25);
            lblTime.Location = new Point(170, 55);

            // Найдено пар
            lblPairs = new Label();
            lblPairs.Text = $"Пары: 0/{totalPairs}";
            lblPairs.Font = new Font("Arial", 12, FontStyle.Bold);
            lblPairs.Size = new Size(120, 25);
            lblPairs.Location = new Point(310, 55);

            // Кнопка меню
            Button btnMenu = new Button();
            btnMenu.Text = " Меню";
            btnMenu.Font = new Font("Arial", 12, FontStyle.Bold);
            btnMenu.Size = new Size(100, 35);
            btnMenu.Location = new Point(this.ClientSize.Width - 120, 35);
            btnMenu.Click += (s, e) => this.Close();

            infoPanel.Controls.AddRange(new Control[] { 
                lblDifficulty, lblMoves, lblTime, lblPairs, btnMenu 
            });
            this.Controls.Add(infoPanel);

            // Таймер игры
            gameTimer = new Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;
        }

        private string GetDifficultyText()
        {
            if (currentMode == GameMode.Quick)
                return "Быстрая игра";
            else if (currentMode == GameMode.Easy)
                return " Легко";
            else if (currentMode == GameMode.Medium)
                return " Средне";
            else if (currentMode == GameMode.Hard)
                return " Сложно";
            else
                return "Быстрая игра";
        }

        private void StartNewGame()
        {
            // Настройка в зависимости от сложности
            switch (currentMode)
            {
                case GameMode.Quick:
                case GameMode.Easy:
                    totalPairs = 8; // 4x4
                    break;
                case GameMode.Medium:
                    totalPairs = 10; // 4x5
                    break;
                case GameMode.Hard:
                    totalPairs = 15; // 5x6
                    break;
            }

            // Очищаем старые карточки
            foreach (Button card in cards)
            {
                this.Controls.Remove(card);
            }
            cards.Clear();

            CreateCards();
            pairsFound = 0;
            movesCount = 0;
            timeElapsed = 0;
            UpdateStats();
            gameTimer.Start();
        }

        private void CreateCards()
        {
            // Создаем значения карточек
            List<int> values = new List<int>();
            for (int i = 1; i <= totalPairs; i++)
            {
                values.Add(i);
                values.Add(i);
            }

            // Перемешиваем
            ShuffleValues(values);

            // Настройки сетки в зависимости от сложности
            int cols = 4;
            if (currentMode == GameMode.Medium)
                cols = 5;
            else if (currentMode == GameMode.Hard)
                cols = 6;

            int rows = (totalPairs * 2) / cols;

            int cardSize =100;
            int margin = 15;

            // РАССЧИТЫВАЕМ ЦЕНТР
            int totalWidth = cols * cardSize + (cols - 1) * margin;
            int totalHeight = rows * cardSize + (rows - 1) * margin;

            int startX = (this.ClientSize.Width - totalWidth) / 2;  // Центр по горизонтали
            int startY = 120;  // Отступ сверху (под панелью информации)

            // Создаем карточки
            for (int i = 0; i < values.Count; i++)
            {
                Button card = new Button();
                card.Size = new Size(cardSize, cardSize);
                card.Location = new Point(
                    startX + (i % cols) * (cardSize + margin),
                    startY + (i / cols) * (cardSize + margin)
                );
                card.Text = "?";
                card.Tag = values[i];
                card.BackColor = GetCardColor();
                card.Font = new Font("Arial", 16, FontStyle.Bold);
                card.ForeColor = Color.Black;
                card.Click += Card_Click;

                cards.Add(card);
                this.Controls.Add(card);
            }
        }

        private Color GetCardColor()
        {
            if(currentMode == GameMode.Easy)
                  return Color.LightBlue;
             else if (currentMode == GameMode.Medium)
                return Color.LightGreen;
            else if (currentMode == GameMode.Hard)
                return Color.LightCoral;
            else
                return Color.LightBlue;
        }

        private void ShuffleValues(List<int> values)
        {
            Random rand = new Random();
            for (int i = values.Count - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                int temp = values[i];
                values[i] = values[j];
                values[j] = temp;
            }
        }

        private void Card_Click(object sender, EventArgs e)
        {
            Button card = (Button)sender;
            
            if (card.Text != "?") return;
            
            card.Text = card.Tag.ToString();
            card.BackColor = Color.White;

            if (firstCard == null)
            {
                firstCard = card;
            }
            else
            {
                secondCard = card;
                movesCount++;
                CheckForMatch();
            }
        }

        private void CheckForMatch()
        {
            foreach (Button card in cards) card.Enabled = false;

            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += (s, e) =>
            {
                timer.Stop();

                if (firstCard.Tag.ToString() == secondCard.Tag.ToString())
                {
                    firstCard.BackColor = Color.Gold;
                    secondCard.BackColor = Color.Gold;
                    firstCard.Enabled = false;
                    secondCard.Enabled = false;
                    pairsFound++;

                    if (pairsFound == totalPairs)
                    {
                        GameWon();
                    }
                }
                else
                {
                    firstCard.Text = "?";
                    secondCard.Text = "?";
                    firstCard.BackColor = GetCardColor();
                    secondCard.BackColor = GetCardColor();
                }

                firstCard = null;
                secondCard = null;
                UpdateStats();
                
                foreach (Button card in cards)
                    if (card.Text == "?") card.Enabled = true;
            };
            
            timer.Start();
        }

        private void GameWon()
        {
            gameTimer.Stop();
            string message = $"🎉 Поздравляю! Вы выиграли! 🎉\n\n" +
                           $"Сложность: {GetDifficultyText()}\n" +
                           $"Ходы: {movesCount}\n" +
                           $"Время: {FormatTime(timeElapsed)}";
            
            MessageBox.Show(message, "Победа!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateStats()
        {
            lblMoves.Text = $"Ходы: {movesCount}";
            lblTime.Text = $"Время: {FormatTime(timeElapsed)}";
            lblPairs.Text = $"Пары: {pairsFound}/{totalPairs}";
        }

        private string FormatTime(int seconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            return $"{time.Minutes:00}:{time.Seconds:00}";
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            timeElapsed++;
            UpdateStats();
        }
    }
}
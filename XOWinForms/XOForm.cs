using XOSimpleToolkit.ClassicXO.Constants;
using XOSimpleToolkit.Common.Enums;
using XOSimpleToolkit.Common.Models;
using XOWinForms.UIElements;
using XOWinForms.UIInterfaces;

namespace XOWinForms
{
    /// <summary>
    /// Основная форма
    /// </summary>
    public partial class XOForm : Form, IXOView
    {
        public event Action<int, int>? ClickEvent;
        public event Action? EasyDifficultyClick;
        public event Action? NormalDifficultyClick;
        public event Action? HardDifficultyClick;
        public event Action? StartClick;
        public event Action? HumanFirstEvent;
        public event Action? ComputerFirstEvent;

        private CellPictureBox[,]? _pictureField;

        /// <summary>
        /// Конструктор
        /// </summary>
        public XOForm()
        {
            InitializeComponent();
            _background = new PictureBox() { Width = 500, Height = 700, Location = new Point(0, 0), SizeMode = PictureBoxSizeMode.StretchImage, Image = Properties.Resources.background };
            _close = new PictureBox() { Width = 40, Height = 40, Location = new Point(455, 10), BackColor = Color.Transparent, Image = Properties.Resources.close };
            _minimize = new PictureBox() { Width = 40, Height = 40, Location = new Point(410, 10), BackColor = Color.Transparent, Image = Properties.Resources.minimize };
            _direction = new PictureBox() { Width = 40, Height = 40, Location = new Point(430, 550), BackColor = Color.Transparent, Image = Properties.Resources.direction1 };
            Controls.Add(_background);
            LoadImages();
            LoadMotionFrames();
            _start!.Enabled = false;
        }

        /// <summary>
        /// Создание игрового поля
        /// </summary>
        private void CreateField()
        {
            _field = new PictureBox() { Width = 240, Height = 240, Location = new Point(100, 150), BackColor = Color.Transparent, SizeMode = PictureBoxSizeMode.StretchImage };
            _background!.Controls.Add(_field);
            _pictureField = new CellPictureBox[3, 3];

            var x = 0;
            var y = 0;

            for (int i = 0; i < FieldConstants.FieldWidth; i++)
            {
                for (int j = 0; j < FieldConstants.FieldHeight; j++)
                {
                    _pictureField[i, j] = new CellPictureBox(i, j);
                    _pictureField[i, j].Size = new Size(70, 70);
                    _pictureField[i, j].Location = new Point(x, y);
                    _pictureField[i, j].Click += CellPictureBox_Click!;
                    _pictureField[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    _field.Controls.Add(_pictureField[i, j]);
                    x += 85;
                }
                x = 0;
                y += 85;
            }
        }

        /// <summary>
        /// Блокировка поля
        /// </summary>
        private void BlockField()
        {
            foreach (var item in _pictureField!)
            {
                item.Enabled = false;
            }
        }

        /// <summary>
        /// Разблокировка поля
        /// </summary>
        private void UnblockField()
        {
            foreach (var item in _pictureField!)
            {
                item.Enabled = true;
            }
        }

        /// <summary>
        /// Клик по полю
        /// </summary>
        /// <param name="sender">Объект инициатор</param>
        /// <param name="e">Аргументы события</param>
        private void CellPictureBox_Click(object? sender, EventArgs e)
        {
            var btn = sender as CellPictureBox;
            OnClickEvent(btn!.X, btn!.Y);
        }

        /// <summary>
        /// Отрисовка хода компьютера
        /// </summary>
        /// <param name="moveInformation">Данные хода</param>
        public void ComputerMove(MoveInformation moveInformation)
        {
            BlockField();
            ComputerDrawAsync(_pictureField![moveInformation.X, moveInformation.Y], moveInformation.WhoWasFirst);
            UnblockField();
        }

        /// <summary>
        /// Отрисовка хода человека
        /// </summary>
        /// <param name="moveInformation">Данные хода</param>
        public void HumanMove(MoveInformation moveInformation)
        {
            HumanDrawAsync(_pictureField![moveInformation.X, moveInformation.Y], moveInformation.WhoWasFirst);
        }

        /// <summary>
        /// Отрисовка конца игры
        /// </summary>
        public void GameIsOver()
        {
            InvokePanelNobodyWin();
            _background!.Controls.Add(_again);
        }

        /// <summary>
        /// Отрисовка хода человека
        /// </summary>
        /// <param name="cellPictureBox">Клетка</param>
        /// <param name="first">Чей ход был первым</param>
        private async void HumanDrawAsync(CellPictureBox cellPictureBox, Turn first)
        {
            if (first == Turn.Human)
            {
                for (int i = 0; i < 12; i++)
                {
                    cellPictureBox.Image = _crossImages[i];
                    await Task.Delay(50);
                }
                return;
            }

            for (int i = 0; i < 12; i++)
            {
                cellPictureBox.Image = _circleImages[i];
                await Task.Delay(50);
            }
        }

        /// <summary>
        /// Отрисовка хода компьютера
        /// </summary>
        /// <param name="cellPictureBox">Клетка</param>
        /// <param name="first">Чей ход был первым</param>
        private async void ComputerDrawAsync(CellPictureBox cellPictureBox, Turn first)
        {
            await Task.Delay(1000);
            if (first == Turn.Human)
            {
                for (int i = 0; i < 12; i++)
                {
                    Invoke(new Action(() => cellPictureBox.Image = _circleImages[i]));
                    await Task.Delay(50);
                }
                return;
            }

            for (int i = 0; i < 12; i++)
            {
                Invoke(new Action(() => cellPictureBox.Image = _crossImages[i]));
                await Task.Delay(50);
            }
        }

        /// <summary>
        /// Отрисовка окна победы для человека
        /// </summary>
        /// <param name="victoryInformation">Информация о победе</param>
        public void HumanVictory(VictoryInformation victoryInformation)
        {
            Task.Run(async () =>
            {
                await Task.Delay(1000);
                if (victoryInformation.WhoWasFirst == Turn.Human)
                {
                    foreach (var cell in victoryInformation.VictoryCombinationCells)
                    {
                        _pictureField![cell.X, cell.Y].Image = Properties.Resources.crosswin;
                        await Task.Delay(300);
                    }
                }
                else
                {
                    foreach (var cell in victoryInformation.VictoryCombinationCells)
                    {
                        _pictureField![cell.X, cell.Y].Image = Properties.Resources.circlewin;
                        await Task.Delay(300);
                    }
                }
            });
            InvokePanelHumanWin();
            _background!.Controls.Add(_again);
        }

        /// <summary>
        /// Отрисовка окна победы для компьютера
        /// </summary>
        /// <param name="victoryInformation">Информация о победе</param>
        public void ComputerVictory(VictoryInformation victoryInformation)
        {
            Task.Run(async () =>
            {
                await Task.Delay(2000);
                if (victoryInformation.WhoWasFirst == Turn.Computer)
                {
                    foreach (var cell in victoryInformation.VictoryCombinationCells)
                    {
                        _pictureField![cell.X, cell.Y].Image = Properties.Resources.crosswin;
                        await Task.Delay(300);
                    }
                }
                else
                {
                    foreach (var cell in victoryInformation.VictoryCombinationCells)
                    {
                        _pictureField![cell.X, cell.Y].Image = Properties.Resources.circlewin;
                        await Task.Delay(300);
                    }
                }
            });
            InvokePanelComputerWin();
            _background!.Controls.Add(_again);
        }

        /// <summary>
        /// Клик по кнопке Старт
        /// </summary>
        /// <param name="sender">Объект инициатор</param>
        /// <param name="e">Аргументы события</param>
        private void Start_Click(object? sender, EventArgs e)
        {
            _easy!.Dispose();
            _normal!.Dispose();
            _hard!.Dispose();
            _start!.Dispose();
            _mainScreen!.Dispose();

            PrepareField();
        }

        /// <summary>
        /// Клик по кнопке Сложный уровень сложности
        /// </summary>
        /// <param name="sender">Объект инициатор</param>
        /// <param name="e">Аргументы события</param>
        private void HardClick(object? sender, EventArgs e)
        {
            OnHardDifficultyClick();
            _isHard = true;
            _isNormal = false;
            _isEasy = false;
            _hard!.Image = Properties.Resources.hard_pressed;
            _normal!.Image = Properties.Resources.normal;
            _easy!.Image = Properties.Resources.easy;

            _start!.Image = Properties.Resources.start;
            _start.Enabled = true;
        }

        /// <summary>
        /// Клик по кнопке Нормальный уровень сложности
        /// </summary>
        /// <param name="sender">Объект инициатор</param>
        /// <param name="e">Аргументы события</param>
        private void Normal_Click(object? sender, EventArgs e)
        {
            OnNormalDifficultyClick();
            _isHard = false;
            _isNormal = true;
            _isEasy = false;
            _hard!.Image = Properties.Resources.hard;
            _normal!.Image = Properties.Resources.normal_pressed;
            _easy!.Image = Properties.Resources.easy;

            _start!.Image = Properties.Resources.start;
            _start.Enabled = true;
        }

        /// <summary>
        /// Клик по кнопке Легкий уровень сложности
        /// </summary>
        /// <param name="sender">Объект инициатор</param>
        /// <param name="e">Аргументы события</param>
        private void Easy_Click(object? sender, EventArgs e)
        {
            OnEasyDifficultyClick();
            _isHard = false;
            _isNormal = false;
            _isEasy = true;
            _hard!.Image = Properties.Resources.hard;
            _normal!.Image = Properties.Resources.normal;
            _easy!.Image = Properties.Resources.easy_pressed;

            _start!.Image = Properties.Resources.start;
            _start.Enabled = true;
        }

        protected virtual void OnClickEvent(int x, int y) 
        {
            var raiseEvent = ClickEvent;
            if (raiseEvent != null)
            {
                raiseEvent(x, y);
            }
        }

        protected virtual void OnEasyDifficultyClick()
        {
            var raiseEvent = EasyDifficultyClick;
            if (raiseEvent != null)
            {
                raiseEvent();
            }
        }

        protected virtual void OnNormalDifficultyClick() 
        {
            var raiseEvent = NormalDifficultyClick;
            if (raiseEvent != null)
            {
                raiseEvent();
            }
        }

        protected virtual void OnHardDifficultyClick()
        {
            var raiseEvent = HardDifficultyClick;
            if (raiseEvent != null)
            {
                raiseEvent();
            }
        }
    }
}

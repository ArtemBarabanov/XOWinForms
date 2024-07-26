using System.Resources;
using XOSimpleToolkit.ClassicXO.Constants;

namespace XOWinForms
{
    /// <summary>
    /// Основная форма [Graphics]
    /// </summary>
    partial class XOForm
    {
        #region Кадры анимации

        private Image[] _crossImages = new Image[12];
        private Image[] _circleImages = new Image[12];
        private Image[] _fieldImages = new Image[16];
        private Image[] _horizontalWinImages = new Image[6];
        private Image[] _compWinImages = new Image[26];
        private Image[] _manWinImages = new Image[24];
        private Image[] _nobodyWinImages = new Image[20];
        private Image[] _whoFirstImages = new Image[14];

        #endregion

        #region Изображение

        private PictureBox? _background;
        private PictureBox? _field;
        private PictureBox? _close;
        private PictureBox? _minimize;
        private PictureBox? _win;
        private PictureBox? _easy;
        private PictureBox? _normal;
        private PictureBox? _hard;
        private PictureBox? _start;
        private PictureBox? _whoFirst;
        private PictureBox? _mainScreen;
        private PictureBox? _panelUp;
        private PictureBox? _panelDown;
        private PictureBox? _direction;
        private PictureBox? _again;

        #endregion

        private int mouseX;
        private int mouseY;
        private bool isMouseMoving;

        private bool _isEasy;
        private bool _isNormal;
        private bool _isHard;

        /// <summary>
        /// Загрузка картинок
        /// </summary>
        private void LoadImages()
        {
            FormBorderStyle = FormBorderStyle.None;

            _easy = new PictureBox() { Width = 200, Height = 80, Location = new Point(80, 400), SizeMode = PictureBoxSizeMode.StretchImage, BackColor = Color.Transparent, Image = Properties.Resources.easy };
            _normal = new PictureBox() { Width = 200, Height = 80, Location = new Point(80, 480), SizeMode = PictureBoxSizeMode.StretchImage, BackColor = Color.Transparent, Image = Properties.Resources.normal };
            _hard = new PictureBox() { Width = 200, Height = 80, Location = new Point(80, 560), SizeMode = PictureBoxSizeMode.StretchImage, BackColor = Color.Transparent, Image = Properties.Resources.hard };
            _start = new PictureBox() { Width = 80, Height = 80, Location = new Point(290, 480), SizeMode = PictureBoxSizeMode.StretchImage, BackColor = Color.Transparent, Image = Properties.Resources.start_disabled };
            _mainScreen = new PictureBox() { Width = 400, Height = 300, Location = new Point(10, 40), SizeMode = PictureBoxSizeMode.StretchImage, BackColor = Color.Transparent, Image = Properties.Resources.main_screen };
            _whoFirst = new PictureBox() { Width = 70, Height = 70, Location = new Point(210, 230), BackColor = Color.Transparent, SizeMode = PictureBoxSizeMode.StretchImage, Image = Properties.Resources.frame_simple };
            _again = new PictureBox() { Width = 80, Height = 80, Location = new Point(410, 400), SizeMode = PictureBoxSizeMode.StretchImage, BackColor = Color.Transparent, Image = Properties.Resources.again };

            _background!.Controls.Add(_easy);
            _background.Controls.Add(_normal);
            _background.Controls.Add(_hard);
            _background.Controls.Add(_start);
            _background.Controls.Add(_whoFirst);
            _background.Controls.Add(_mainScreen);
            _background.Controls.Add(_close);
            _background.Controls.Add(_minimize);
            _background.Controls.Add(_direction);

            AddHandlers();
        }

        /// <summary>
        /// Загрузка анимационных кадров
        /// </summary>
        private void LoadMotionFrames()
        {
            var imageResources = new ResourceManager(typeof(Properties.Resources));
            for (int i = 0; i < 12; i++)
            {
                _crossImages[i] = (Image)imageResources.GetObject($"frameX{i}")!;
                _circleImages[i] = (Image)imageResources.GetObject($"frameCircle{i}")!;
            }

            for (int i = 0; i < 16; i++)
            {

                _fieldImages[i] = (Image)imageResources.GetObject($"fieldFrame{i}")!;
            }

            for (int i = 0; i < 14; i++)
            {
                _whoFirstImages[i] = (Image)imageResources.GetObject($"frameChoice{i}")!;
            }

            for (int i = 0; i < 26; i++)
            {
                _compWinImages[i] = (Image)imageResources.GetObject($"compV{i}")!;
            }

            for (int i = 0; i < 24; i++)
            {
                _manWinImages[i] = (Image)imageResources.GetObject($"manV{i}")!;
            }

            for (int i = 0; i < 20; i++)
            {
                _nobodyWinImages[i] = (Image)imageResources.GetObject($"noV{i}")!;
            }
        }

        /// <summary>
        /// Добавление обработчиков событий
        /// </summary>
        private void AddHandlers()
        {
            _start!.Click += Start_Click!;
            _start.MouseEnter += StartMouseEnter!;
            _start.MouseLeave += StartMouseLeave!;
            _easy!.Click += Easy_Click!;
            _normal!.Click += Normal_Click!;
            _hard!.Click += HardClick!;
            _easy.MouseEnter += EasyMouseEnter!;
            _easy.MouseLeave += Easy_MouseLeave!;
            _normal.MouseEnter += Normal_MouseEnter!;
            _normal.MouseLeave += Normal_MouseLeave!;
            _hard.MouseEnter += Hard_MouseEnter!;
            _hard.MouseLeave += Hard_MouseLeave!;
            _minimize!.Click += MinimizeClick!;
            _close!.Click += CloseClick!;
            _again!.Click += AgainClick!;
            _again.MouseEnter += AgainMouseEnter!;
            _again.MouseLeave += AgainMouseLeave!;
            _minimize.MouseEnter += MinimizeMouseEnter!;
            _minimize.MouseLeave += MinimizeMouseLeave!;
            _close.MouseEnter += CloseMouseEnter!;
            _close.MouseLeave += CloseMouseLeave!;
            _direction!.MouseDown += DirectionMouseDown!;
            _direction.MouseUp += DirectionMouseUp!;
            _direction.MouseMove += DirectionMouseMove!;
            _direction.MouseEnter += DirectionMouseEnter!;
            _direction.MouseLeave += DirectionMouseLeave!;
        }

        /// <summary>
        /// Обработка наведения курсора мыши на элемент Старт (курсор покидает элемент)
        /// </summary>
        /// <param name="sender">Объект инициатор</param>
        /// <param name="e">Аргументы события</param>
        private void StartMouseLeave(object? sender, EventArgs e)
        {
            _start!.Image = Properties.Resources.start;
        }

        /// <summary>
        /// Обработка наведения курсора мыши на элемент Старт (курсор заходит на элемент)
        /// </summary>
        /// <param name="sender">Объект инициатор</param>
        /// <param name="e">Аргументы события</param>
        private void StartMouseEnter(object? sender, EventArgs e)
        {
            _start!.Image = Properties.Resources.start_hover;
        }

        /// <summary>
        /// Обработка наведения курсора мыши на элемент Закрытие окна (курсор покидает элемент)
        /// </summary>
        /// <param name="sender">Объект инициатор</param>
        /// <param name="e">Аргументы события</param>
        private void CloseMouseLeave(object? sender, EventArgs e)
        {
            _close!.Image = Properties.Resources.close;
        }

        /// <summary>
        /// Обработка наведения курсора мыши на элемент Закрытие окна (курсор заходит на элемент)
        /// </summary>
        /// <param name="sender">Объект инициатор</param>
        /// <param name="e">Аргументы события</param>
        private void CloseMouseEnter(object? sender, EventArgs e)
        {
            _close!.Image = Properties.Resources.close_hover;
        }

        /// <summary>
        /// Обработка наведения курсора мыши на элемент Сворачивание окна (курсор покидает элемент)
        /// </summary>
        /// <param name="sender">Объект инициатор</param>
        /// <param name="e">Аргументы события</param>
        private void MinimizeMouseLeave(object? sender, EventArgs e)
        {
            _minimize!.Image = Properties.Resources.minimize;
        }

        /// <summary>
        /// Обработка наведения курсора мыши на элемент Сворачивание окна (курсор заходит на элемент)
        /// </summary>
        /// <param name="sender">Объект инициатор</param>
        /// <param name="e">Аргументы события</param>
        private void MinimizeMouseEnter(object? sender, EventArgs e)
        {
            _minimize!.Image = Properties.Resources.minimize_hover;
        }

        /// <summary>
        /// Клик по кнопке Еще
        /// </summary>
        /// <param name="sender">Объект инициатор</param>
        /// <param name="e">Аргументы события</param>
        private void AgainClick(object? sender, EventArgs e)
        {
            _again!.Dispose();

            _win!.Dispose();
            LoadImages();
            _isEasy = false;
            _isNormal = false;
            _isHard = false;
            _start!.Enabled = false;

            for (int i = 0; i < FieldConstants.FieldWidth; i++)
            {
                for (int j = 0; j < FieldConstants.FieldHeight; j++)
                {
                    _pictureField![i, j].Dispose();
                    _pictureField[i, j].Click -= CellPictureBox_Click;
                }
            }
            _field!.Dispose();
        }

        /// <summary>
        /// Обработка наведения курсора мыши на элемент Еще (курсор покидает элемент)
        /// </summary>
        /// <param name="sender">Объект инициатор</param>
        /// <param name="e">Аргументы события</param>
        private void AgainMouseLeave(object? sender, EventArgs e)
        {
            _again!.Image = Properties.Resources.again;
        }

        /// <summary>
        /// Обработка наведения курсора мыши на элемент Еще (курсор заходит на элемент)
        /// </summary>
        /// <param name="sender">Объект инициатор</param>
        /// <param name="e">Аргументы события</param>
        private void AgainMouseEnter(object? sender, EventArgs e)
        {
            _again!.Image = Properties.Resources.again_hover;
        }

        /// <summary>
        /// Событие перемещения окна при зажатой левой кнопке мыши
        /// </summary>
        /// <param name="sender">Объект инициатор</param>
        /// <param name="e">Аргументы события</param>
        private void DirectionMouseMove(object? sender, MouseEventArgs e)
        {
            if (isMouseMoving)
            {
                Left = Left + (Cursor.Position.X - mouseX);
                Top = Top + (Cursor.Position.Y - mouseY);

                mouseY = Cursor.Position.Y;
                mouseX = Cursor.Position.X;
            }
        }

        /// <summary>
        /// Событие отпускания клавиши мыши при перемещении окна
        /// </summary>
        /// <param name="sender">Объект инициатор</param>
        /// <param name="e">Аргументы события</param>
        private void DirectionMouseUp(object? sender, MouseEventArgs e)
        {
            isMouseMoving = false;
        }

        /// <summary>
        /// Событие нажатия клавиши мыши при перемещении окна
        /// </summary>
        /// <param name="sender">Объект инициатор</param>
        /// <param name="e">Аргументы события</param>
        private void DirectionMouseDown(object? sender, MouseEventArgs e)
        {
            mouseX = Cursor.Position.X;
            mouseY = Cursor.Position.Y;
            isMouseMoving = true;
        }

        /// <summary>
        /// Клик по кнопке Сворачивание формы
        /// </summary>
        /// <param name="sender">Объект инициатор</param>
        /// <param name="e">Аргументы события</param>
        private void MinimizeClick(object? sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// Клик по кнопке Закрытие формы
        /// </summary>
        /// <param name="sender">Объект инициатор</param>
        /// <param name="e">Аргументы события</param>
        private void CloseClick(object? sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработка наведения курсора мыши на элемент Перемещение окна (курсор покидает элемент)
        /// </summary>
        /// <param name="sender">Объект инициатор</param>
        /// <param name="e">Аргументы события</param>
        private void DirectionMouseLeave(object? sender, EventArgs e)
        {
            _direction!.Image = Properties.Resources.direction1;
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Обработка наведения курсора мыши на элемент Перемещение окна (курсор заходит на элемент)
        /// </summary>
        /// <param name="sender">Объект инициатор</param>
        /// <param name="e">Аргументы события</param>
        private void DirectionMouseEnter(object? sender, EventArgs e)
        {
            _direction!.Image = Properties.Resources.direction1_hover;
            Cursor = Cursors.Hand;
        }

        /// <summary>
        /// Обработка наведения курсора мыши на элемент Сложная сложность (курсор покидает элемент)
        /// </summary>
        /// <param name="sender">Объект инициатор</param>
        /// <param name="e">Аргументы события</param>
        private void Hard_MouseLeave(object? sender, EventArgs e)
        {
            if (_isHard)
            {
                _hard!.Image = Properties.Resources.hard_pressed;
                return;
            }

            _hard!.Image = Properties.Resources.hard;
        }

        /// <summary>
        /// Обработка наведения курсора мыши на элемент Сложная сложность (курсор заходит на элемент)
        /// </summary>
        /// <param name="sender">Объект инициатор</param>
        /// <param name="e">Аргументы события</param>
        private void Hard_MouseEnter(object? sender, EventArgs e)
        {
            if (!_isHard)
            {
                _hard!.Image = Properties.Resources.hard_hover;
            }
        }

        /// <summary>
        /// Обработка наведения курсора мыши на элемент Нормальная сложность (курсор покидает элемент)
        /// </summary>
        /// <param name="sender">Объект инициатор</param>
        /// <param name="e">Аргументы события</param>
        private void Normal_MouseLeave(object? sender, EventArgs e)
        {
            if (_isNormal)
            {
                _normal!.Image = Properties.Resources.normal_pressed;
                return;
            }

            _normal!.Image = Properties.Resources.normal;
        }

        /// <summary>
        /// Обработка наведения курсора мыши на элемент Нормальная сложность (курсор заходит на элемент)
        /// </summary>
        /// <param name="sender">Объект инициатор</param>
        /// <param name="e">Аргументы события</param>
        private void Normal_MouseEnter(object? sender, EventArgs e)
        {
            if (!_isNormal)
            {
                _normal!.Image = Properties.Resources.normal_hover;
            }
        }

        /// <summary>
        /// Обработка наведения курсора мыши на элемент Легкая сложность (курсор покидает элемент)
        /// </summary>
        /// <param name="sender">Объект инициатор</param>
        /// <param name="e">Аргументы события</param>
        private void Easy_MouseLeave(object? sender, EventArgs e)
        {
            if (_isEasy)
            {
                _easy!.Image = Properties.Resources.easy_pressed;
                return;
            }

            _easy!.Image = Properties.Resources.easy;
        }

        /// <summary>
        /// Обработка наведения курсора мыши на элемент Легкая сложность (курсор заходит на элемент)
        /// </summary>
        /// <param name="sender">Объект инициатор</param>
        /// <param name="e">Аргументы события</param>
        private void EasyMouseEnter(object? sender, EventArgs e)
        {
            if (!_isEasy)
            {
                _easy!.Image = Properties.Resources.easy_hover;
            }
        }
    }
}
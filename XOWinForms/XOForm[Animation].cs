namespace XOWinForms
{
    /// <summary>
    /// Основная форма [Animation]
    /// </summary>
    partial class XOForm
    {
        /// <summary>
        /// Подготовка поля выбора, чей ход первый
        /// </summary>
        private async void PrepareField()
        {
            await ChangeXPositionAsync();
            await WhoFirstAsync();
            await FieldDrawAsync();
        }

        /// <summary>
        /// Перемещение панели выбора, чей ход первый
        /// </summary>
        private async Task ChangeXPositionAsync()
        {
            for (int i = 0; i < 7; i++)
            {
                int x = _whoFirst!.Location.X;
                int y = _whoFirst.Location.Y;
                _whoFirst.Location = new Point(x - 8, y - 5);
                _whoFirst.Height = _whoFirst.Height + 5;
                _whoFirst.Width = _whoFirst.Width + 5;
                await Task.Delay(50);
            }
        }

        /// <summary>
        /// Анимация выбора, кто будет ходить первым
        /// </summary>
        private async Task WhoFirstAsync()
        {
            int count = new Random().Next(1, 10);
            InvokeUpPanel();

            int j = 0;

            while (j != count)
            {
                if (j != count)
                {
                    for (int i = 0; i < 14; i++)
                    {
                        _whoFirst!.Image = _whoFirstImages[i];
                        await Task.Delay(50);
                    }
                    ++j;
                }
                if (j != count)
                {
                    for (int i = 13; i >= 0; i--)
                    {
                        _whoFirst!.Image = _whoFirstImages[i];
                        await Task.Delay(50);
                    }
                    ++j;
                }
            }

            if (count % 2 == 0)
            {
                OnHumanFirstEvent();
                InvokePanelHumanFirst();
            }
            else
            {
                OnComputerFirstEvent();
                InvokePanelComputerFirst();
            }
        }

        /// <summary>
        /// Анимация отрисовки игрового поля
        /// </summary>
        private async Task FieldDrawAsync()
        {
            await Task.Delay(3000);
            _whoFirst!.Dispose();
            _panelUp!.Dispose();
            _panelDown!.Dispose();
            CreateField();

            for (int i = 0; i < 16; i++)
            {
                _field!.Image = _fieldImages[i];
                await Task.Delay(70);
            }
            OnStartClick();
        }

        /// <summary>
        /// Отбражение панели Ты играешь...
        /// </summary>
        private void InvokeUpPanel()
        {
            _panelUp = new PictureBox() { Width = 300, Height = 80, Location = new Point(50, 30), SizeMode = PictureBoxSizeMode.StretchImage, BackColor = Color.Transparent, Image = Properties.Resources.panelup };
            _background!.Controls.Add(_panelUp);
        }

        /// <summary>
        /// Отображение панели Человек ходит первым
        /// </summary>
        private void InvokePanelHumanFirst()
        {
            _panelDown = new PictureBox() { Width = 300, Height = 300, Location = new Point(50, 300), SizeMode = PictureBoxSizeMode.StretchImage, BackColor = Color.Transparent, Image = Properties.Resources.paneldown };
            _background!.Controls.Add(_panelDown);
        }

        /// <summary>
        /// Отображение панели Компьютер ходит первым
        /// </summary>
        private void InvokePanelComputerFirst()
        {
            _panelDown = new PictureBox() { Width = 300, Height = 300, Location = new Point(50, 300), SizeMode = PictureBoxSizeMode.StretchImage, BackColor = Color.Transparent, Image = Properties.Resources.paneldown2 };
            _background!.Controls.Add(_panelDown);
        }

        /// <summary>
        /// Анимация панели Компьютер победил
        /// </summary>
        private void InvokePanelComputerWin()
        {
            _win = new PictureBox() { Width = 250, Height = 150, Location = new Point(100, 400), BackColor = Color.Transparent };
            _background!.Controls.Add(_win);

            Task.Run(async () =>
            {
                for (int i = 0; i < 26; i++)
                {
                    _win.Image = _compWinImages[i];
                    await Task.Delay(40);
                }
            });
        }

        /// <summary>
        /// Анимация панели Человек победил
        /// </summary>
        private void InvokePanelHumanWin()
        {
            _win = new PictureBox() { Width = 250, Height = 150, Location = new Point(100, 400), BackColor = Color.Transparent };
            _background!.Controls.Add(_win);

            Task.Run(async () =>
            {
                for (int i = 0; i < 26; i++)
                {
                    _win.Image = _manWinImages[i];
                    await Task.Delay(40);
                }
            });
        }

        /// <summary>
        /// Анимация панели Ничья
        /// </summary>
        private void InvokePanelNobodyWin()
        {
            _win = new PictureBox() { Width = 250, Height = 150, Location = new Point(100, 400), BackColor = Color.Transparent };
            _background!.Controls.Add(_win);

            Task.Run(async () =>
            {
                for (int i = 0; i < 26; i++)
                {
                    _win.Image = _nobodyWinImages[i];
                    await Task.Delay(40);
                }
            });
        }

        protected virtual void OnStartClick()
        {
            var raiseEvent = StartClick;
            if (raiseEvent != null)
            {
                raiseEvent();
            }
        }

        protected virtual void OnHumanFirstEvent()
        {
            var raiseEvent = HumanFirstEvent;
            if (raiseEvent != null)
            {
                raiseEvent();
            }
        }

        protected virtual void OnComputerFirstEvent()
        {
            var raiseEvent = ComputerFirstEvent;
            if (raiseEvent != null)
            {
                raiseEvent();
            }
        }
    }
}
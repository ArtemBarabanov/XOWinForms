using XOSimpleToolkit.Common.Enums;
using XOSimpleToolkit.Common.Interfaces;
using XOSimpleToolkit.Common.Models;
using XOWinForms.UIInterfaces;

namespace XOWinForms.Presenter
{
    /// <summary>
    /// Презентер формы
    /// </summary>
    sealed class XOPresenter
    {
        private readonly IXOView _xoView;
        private readonly IXOSession _xoSession;
        private Difficulty _difficulty;
        private Turn _whoIsFirst;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="xoView">Интерфейс View</param>
        /// <param name="xoSession">Интефрейс игровой сессии</param>
        public XOPresenter(IXOView xoView, IXOSession xoSession)
        {
            _xoView = xoView;
            xoView.ClickEvent += FieldClickEvent!;
            xoView.HardDifficultyClick += MenuHighDifficultyClick!;
            xoView.NormalDifficultyClick += MenuNormalDifficultyClick!;
            xoView.EasyDifficultyClick += MenuLowDifficultyClick!;
            xoView.StartClick += MenuStartClick!;
            xoView.HumanFirstEvent += ViewHumanFirstEvent!;
            xoView.ComputerFirstEvent += ViewComputerFirstEvent!;

            _xoSession = xoSession;
            _xoSession.HumanMoveEvent += HumanMove;
            _xoSession.ComputerMoveEvent += ComputerMove;
            _xoSession.ComputerVictoryEvent += ComputerVictory;
            _xoSession.HumanVictoryEvent += HumanVictory;
            _xoSession.DrawVictoryEvent += DrawVictory;
        }

        /// <summary>
        /// Ход человека
        /// </summary>
        /// <param name="moveInformation">Информация о ходе</param>
        public void HumanMove(MoveInformation moveInformation)
        {
            _xoView.HumanMove(moveInformation);
        }

        /// <summary>
        /// Ход компьютера
        /// </summary>
        /// <param name="moveInformation">Информация о ходе</param>
        public void ComputerMove(MoveInformation moveInformation)
        {
            _xoView.ComputerMove(moveInformation);
        }

        /// <summary>
        /// Ничья
        /// </summary>
        public void DrawVictory()
        {
            _xoView.GameIsOver();
        }

        /// <summary>
        /// Победа человека
        /// </summary>
        /// <param name="victoryInformation">Информация о победе</param>
        public void HumanVictory(VictoryInformation victoryInformation)
        {
            _xoView.HumanVictory(victoryInformation);
        }

        /// <summary>
        /// Победа компьютера
        /// </summary>
        /// <param name="victoryInformation">Информация о победе</param>
        public void ComputerVictory(VictoryInformation victoryInformation)
        {
            _xoView.ComputerVictory(victoryInformation);
        }

        /// <summary>
        /// Компьютер ходит первым
        /// </summary>
        private void ViewComputerFirstEvent()
        {
            _whoIsFirst = Turn.Computer;
        }

        /// <summary>
        /// Человек ходит первым
        /// </summary>
        private void ViewHumanFirstEvent()
        {
            _whoIsFirst = Turn.Human;
        }

        /// <summary>
        /// Начало игры
        /// </summary>
        private void MenuStartClick()
        {
            _xoSession.Start(_difficulty, _whoIsFirst);
        }

        /// <summary>
        /// Выбор легкого уровня сложности
        /// </summary>
        private void MenuLowDifficultyClick()
        {
            _difficulty = Difficulty.Easy;
        }

        /// <summary>
        /// Выбор нормального уровня сложности
        /// </summary>
        private void MenuNormalDifficultyClick()
        {
            _difficulty = Difficulty.Normal;
        }

        /// <summary>
        /// Выбор тяжелого уровня сложности
        /// </summary>
        private void MenuHighDifficultyClick()
        {
            _difficulty = Difficulty.Hard;
        }

        /// <summary>
        /// Клик по клетке поля
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        private void FieldClickEvent(int x, int y)
        {
            _xoSession.Move(x, y);
        }
    }
}

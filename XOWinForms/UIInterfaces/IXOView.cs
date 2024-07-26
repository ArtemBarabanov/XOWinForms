using XOSimpleToolkit.Common.Models;

namespace XOWinForms.UIInterfaces
{
    /// <summary>
    /// Интерфейс View, определяющий контракт игры
    /// </summary>
    public interface IXOView : IXOMenu
    {
        /// <summary>
        /// Клик по полю
        /// </summary>
        event Action<int, int> ClickEvent;

        /// <summary>
        /// Ход компьютера
        /// </summary>
        /// <param name="moveInformation">Информация о ходе</param>
        void ComputerMove(MoveInformation moveInformation);

        /// <summary>
        /// Ход человека
        /// </summary>
        /// <param name="moveInformation">Информация о ходе</param>
        void HumanMove(MoveInformation moveInformation);

        /// <summary>
        /// Победа человека
        /// </summary>
        /// <param name="victoryInformation">Информация о победе</param>
        void HumanVictory(VictoryInformation victoryInformation);

        /// <summary>
        /// Победа компьютера
        /// </summary>
        /// <param name="victoryInformation">Информация о победе</param>
        void ComputerVictory(VictoryInformation victoryInformation);

        /// <summary>
        /// Игра окончена
        /// </summary>
        void GameIsOver();
    }
}

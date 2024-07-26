namespace XOWinForms.UIInterfaces
{
    /// <summary>
    /// Интерфейс View, определяющий контракт взаимодействия с меню
    /// </summary>
    public interface IXOMenu
    {
        /// <summary>
        /// Выбор лекого уровня сложности
        /// </summary>
        event Action EasyDifficultyClick;

        /// <summary>
        /// Выбор среднего уровня сложности
        /// </summary>
        event Action NormalDifficultyClick;

        /// <summary>
        /// Выбор тяжелого уровня сложности
        /// </summary>
        event Action HardDifficultyClick;

        /// <summary>
        /// Начало игры
        /// </summary>
        event Action StartClick;

        /// <summary>
        /// Игрок ходит первым
        /// </summary>
        event Action HumanFirstEvent;

        /// <summary>
        /// Компьютер ходит первым
        /// </summary>
        event Action ComputerFirstEvent;
    }
}

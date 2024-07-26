namespace XOWinForms.UIElements
{
    /// <summary>
    /// Элемнт - клетка игрового поля
    /// </summary>
    public class CellPictureBox : PictureBox
    {
        /// <summary>
        /// Координата X
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Координата Y
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        public CellPictureBox(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}

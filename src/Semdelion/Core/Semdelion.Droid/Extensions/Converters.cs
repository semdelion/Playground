namespace Semdelion.Droid.Extensions
{
    using Android.Content;

    /// <summary>
    ///     Конвертер величин.
    /// </summary>
    public static class Converters
    {
        /// <summary>
        ///     Конвертировать DP в PX.
        /// </summary>
        /// <param name="context">Контекст.</param>
        /// <param name="dp">Значение в DP.</param>
        /// <returns>Значение в PX.</returns>
        public static float DpToPx(Context context, float dp)
        {
            return dp * context.Resources.DisplayMetrics.Density;
        }

        /// <summary>
        ///     Конвертировать PX в DP.
        /// </summary>
        /// <param name="context">Контекст.</param>
        /// <param name="px">Значение в PX.</param>
        /// <returns>Значение в DP.</returns>
        public static float PxToDp(Context context, float px)
        {
            return px / context.Resources.DisplayMetrics.Density;
        }

        /// <summary>
        ///     Конвертировать SP в PX.
        /// </summary>
        /// <param name="context">Контекст.</param>
        /// <param name="sp">Значение в SP.</param>
        /// <returns>Значение в DP.</returns>
        public static float SpToPx(Context context, float sp)
        {
            return sp * context.Resources.DisplayMetrics.ScaledDensity;
        }

        /// <summary>
        ///     Конвертировать PX в SP.
        /// </summary>
        /// <param name="context">Контекст.</param>
        /// <param name="px">Значение в PX.</param>
        /// <returns>Значение в SP.</returns>
        public static float PxToSp(Context context, float px)
        {
            return px / context.Resources.DisplayMetrics.ScaledDensity;
        }
    }
}
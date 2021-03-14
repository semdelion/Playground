using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Logging;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Playground.Core.ViewModels.Settings.CellElements;

namespace Playground.Droid.Adapters
{
    public class LogsAdapter : MvxRecyclerAdapter
    {
        private Color _warningColor = new Color(255, 204, 0);
        private Color _errorColor = new Color(255, 51, 51);
        private Color _fatalColor = new Color(255, 51, 51);
        private Color _mainColor = new Color();

        public LogsAdapter(IMvxAndroidBindingContext bindingContext, Context context) : base(bindingContext)
        {
            using var typedValue = new TypedValue();
            if (context.Theme.ResolveAttribute(Droid.Resource.Attribute.semdelion_text_color_caption, typedValue, true))
                _mainColor = new Color(typedValue.Data);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            base.OnBindViewHolder(holder, position);
            var logCellElement = (LogCellElement)GetItem(position);
            if (logCellElement != null)
            {
                var logTextView = holder.ItemView.FindViewById<TextView>(Droid.Resource.Id.logs_text_view);

                if (logCellElement.LogLevel == MvxLogLevel.Warn)
                    logTextView.SetTextColor(_warningColor);
                else if (logCellElement.LogLevel == MvxLogLevel.Error)
                    logTextView.SetTextColor(_errorColor);
                else if (logCellElement.LogLevel == MvxLogLevel.Fatal)
                    logTextView.SetTextColor(_fatalColor);
                else
                    logTextView.SetTextColor(_mainColor);
            }
        }
    }
}
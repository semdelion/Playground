namespace Semdelion.iOS.Custom
{
    using MvvmCross.Commands;
    using UIKit;

    public class EmptyDataSet
    {
        public IMvxCommand RefreshCommand;
        public UIView ContentView;

        public EmptyDataSet(UIView content, IMvxCommand command)
        {
            ContentView = content;
            RefreshCommand = command;
        }
    }
}

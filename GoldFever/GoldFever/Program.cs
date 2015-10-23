using GoldFever.UI.Views;
using GoldFever.UI.Views.Generic;
using System;

namespace GoldFever
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var view = new OptionListView("Hello, world!", "Lorem ipsum dolor sit amet.");
            view.Items.AddRange(new OptionListViewItem[]
            {
                new OptionListViewItem("1"),
                new OptionListViewItem("2"),
                new OptionListViewItem("3"),
                new OptionListViewItem("4"),
                new OptionListViewItem("5"),
                new OptionListViewItem("6"),
                new OptionListViewItem("7"),
                new OptionListViewItem("8"),
                new OptionListViewItem("9"),
                new OptionListViewItem("10"),
            });

            view.Show();
        }
    }
}

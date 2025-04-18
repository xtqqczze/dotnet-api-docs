﻿//<SnippetMainWindowLogic>
using System;
using System.ComponentModel;
using System.Windows;

namespace IEditableCollectionViewAddItemExample;

public partial class Window1 : Window
{
    public Window1() => InitializeComponent();

    void Button_Click(object sender, RoutedEventArgs e)
    {
        IEditableCollectionViewAddNewItem viewToAddDisparateItems =
            catalogList.Items;

        if (!viewToAddDisparateItems.CanAddNewItem)
        {
            _ = MessageBox.Show("You cannot add items to the list.");
            return;
        }

        // Create a window that prompts the user to enter a new
        // item to sell.
        AddItemWindow win = new();

        // Create an item, depending on which RadioButton is selected.
        // Radio buttons correspond to book, cd, dvd, or other.
        LibraryItem newItem = (bool)book.IsChecked
            ? new Book("Enter the book title", "Enter an Author", "Enter a Genre",
                "Enter a call number", DateTime.Now + new TimeSpan(21, 0, 0, 0))
            : (bool)cd.IsChecked
                ? new MusicCD("Enter the Album", "Enter the artist", 0, "CD.******",
                            DateTime.Now + new TimeSpan(14, 0, 0, 0))
                : (bool)dvd.IsChecked
                            ? new MovieDVD("Enter the movie title", "Enter the director",
                                        "Enter the genre", new TimeSpan(), "DVD.******",
                                        DateTime.Now + new TimeSpan(7, 0, 0, 0))
                            : new LibraryItem("Enter the title", "Enter the call number",
                                            DateTime.Now + new TimeSpan(14, 0, 0, 0));

        // Add the new item to the collection by calling
        // IEditableCollectionViewAddNewItem.AddNewItem.
        // Set the DataContext of the AddItemWindow to the
        // returned item.
        win.DataContext = viewToAddDisparateItems.AddNewItem(newItem);

        // If the user submits the new item, commit the new
        // object to the collection.  If the user cancels 
        // adding the new item, discard the new item.
        if ((bool)win.ShowDialog())
        {
            viewToAddDisparateItems.CommitNew();
        }
        else
        {
            viewToAddDisparateItems.CancelNew();
        }
    }
}
//</SnippetMainWindowLogic>

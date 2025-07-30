using Avalonia.Controls;
using Avalonia.Platform.Storage;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace AvaloniaIconTool.Views;

public partial class MainView : UserControl
{
    internal ObservableCollection<LocalImagePathAndInfo> LocalImagesList { get; set; }
    [DllImport("AvaloniaIconToolDLL.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern int WriteIconFile(int argc, string[] args);

    public MainView()
    {
        DataContext = this;
        LocalImagesList = new ObservableCollection<LocalImagePathAndInfo>();
        //LocalImagesList.Add(new LocalImagePathAndInfo("test", "bmp"));
        //LocalImagesList.Add(new LocalImagePathAndInfo("JustAVeryLongStringToBeHereForTestingPurposes_JustAVeryLongStringToBeHereForTestingPurposes_JustAVeryLongStringToBeHereForTestingPurposes_JustAVeryLongStringToBeHereForTestingPurposes", "png"));

        InitializeComponent();

        listView.ItemsSource = LocalImagesList;
    }

    private async void btnAddPicture_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(this);

        // Start async operation to open the dialog.
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Text File",
            AllowMultiple = false,
            //FileTypeFilter = new[] { FilePickerFileTypes.ImagePng }
        });

        if (files.Count >= 1)
        {
            string firstFile = files[0].Path.ToString();

            LocalImagesList.Add(new LocalImagePathAndInfo(firstFile,
                        firstFile.Substring(firstFile.Length - 3)));
        }
    }
    private async void btnSavePicture_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(this);

        // Start async operation to open the dialog.
        var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Save Text File"
        });
        
        if (file is not null)
        {
            //get rid of "file:///"
            string realLocation = file.Path.ToString().Substring(8);

            //Step 1 : Re-do a console c++ command line arguments list
            List<string> paramsList = new List<string>();

            paramsList.Add(System.Environment.ProcessPath);
            paramsList.Add("pack");
            paramsList.Add(realLocation);

            foreach (var item in LocalImagesList)
                paramsList.Add(item.ImagePath.Substring(8));

            //Step 2 : Export using C++
            int result = WriteIconFile(paramsList.Count, paramsList.ToArray());

        }
    }
}
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FlowSphere.ViewModels;

namespace FlowSphere.Views;

public partial class ProjectDetailsView : UserControl
{
    public ProjectDetailsView()
    {
        // ProjectDetailsViewModel vm = new ProjectDetailsViewModel();
        // DataContext = vm;
        InitializeComponent();
    }

    private void Task_MouseMove(object sender, MouseEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            var border = sender as Border;
            if (border?.DataContext is TaskModel task)
            {
                var fromList = border.Tag?.ToString();
                var data = new DataObject();
                data.SetData("Task", task);
                data.SetData("FromList", fromList);
                DragDrop.DoDragDrop(border, data, DragDropEffects.Move);
            }
        }
    }

    private void Column_Drop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent("Task") && e.Data.GetDataPresent("FromList"))
        {
            var task = e.Data.GetData("Task") as TaskModel;
            var fromList = e.Data.GetData("FromList") as string;

            var border = sender as Border;
            var toList = border?.Tag?.ToString();

            if (task != null && fromList != null && toList != null && fromList != toList)
            {
                if (DataContext is ProjectDetailsViewModel vm)
                {
                    vm.MoveTask(task, fromList, toList);
                }
            }
        }
    }
}
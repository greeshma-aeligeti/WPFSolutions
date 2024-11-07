using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSira.Models;

namespace WPFSira.ViewModels
{
    public class TaskDetailsViewModel
    {
        public MyTask SelectedTask { get; set; }
        public ObservableCollection<TaskProperty> TaskProperties { get; set; }

        public TaskDetailsViewModel(MyTask task)
        {
            SelectedTask = task;
            TaskProperties = new ObservableCollection<TaskProperty>
    {
        new TaskProperty { Label = "Title", Value = SelectedTask.Title },
        new TaskProperty { Label = "Description", Value = SelectedTask.Description },
        new TaskProperty { Label = "Type", Value = SelectedTask.Type },
        new TaskProperty { Label = "Start Date", Value = SelectedTask.StartDate.ToString("d") },
        new TaskProperty { Label = "End Date", Value = SelectedTask.EndDate.ToString("d") },
        new TaskProperty { Label = "Story Point", Value = SelectedTask.StoryPoint.ToString() }
    };
        }
    }
}

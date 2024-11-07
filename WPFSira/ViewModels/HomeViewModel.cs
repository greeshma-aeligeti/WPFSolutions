using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSira.Models;

namespace WPFSira.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<MyTask> _tasks;
        private ObservableCollection<MyTask> _filteredTasks;
        private string _searchText;
        public MyTask SelectedTask { get; set; }
        public ObservableCollection<MyTask> Tasks
        {
            get => _filteredTasks;
            set
            {
                _filteredTasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                FilterTasks(); // Filter the tasks when search text changes
            }
        }

        public HomeViewModel()
        {
            _tasks = new ObservableCollection<MyTask>
            {
                new MyTask { Id = 1, Title = "first Task", Description = "Description 1", Type = "Feature", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(3), StoryPoint = 5 },
                new MyTask { Id = 2, Title = "second Task ", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", Type = "Bug", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(2), StoryPoint = 3 },
                new MyTask { Id = 3, Title = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin ", Description = "Description 3", Type = "Improvement", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(4), StoryPoint = 8 },
            };

            _filteredTasks = new ObservableCollection<MyTask>(_tasks);
        }

        private void FilterTasks()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Tasks = new ObservableCollection<MyTask>(_tasks);
            }
            else
            {
                Tasks = new ObservableCollection<MyTask>(
                    _tasks.Where(task => task.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


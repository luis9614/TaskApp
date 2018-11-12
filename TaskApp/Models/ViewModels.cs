using System;
using System.Collections.Generic;

namespace TaskApp.Models
{
    public class TaskView
    {
        public List<IUserTasks> AvailiableTasks
        {
            get;
            set;
        }
        public TaskInvoker CurrentTasks
        {
            get;
            set;
        }
        public DummySystem System
        {
            get;
            set;
        }

        public TaskView(List<IUserTasks> userTasks, TaskInvoker taskInvoker, DummySystem System)
        {
            this.AvailiableTasks = userTasks;
            this.CurrentTasks = taskInvoker;
            this.System = System;
        }

    }
}

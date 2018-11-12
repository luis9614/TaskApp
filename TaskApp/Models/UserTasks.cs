using System;
using System.Collections.Generic;

namespace TaskApp.Models
{
    public class TaskInvoker
    {
        public List<IUserTasks> Tasks
        {
            get;
            set;
        }
        public TaskInvoker()
        {
            this.Tasks = new List<IUserTasks>();
        }
        public void AddTask(IUserTasks Task){
            this.Tasks.Add(Task);
        }

        private Boolean IsAnyActive()
        {
            int cont = 0;
            foreach (var task in Tasks)
            {
                if (task.IsExecuting) cont++;
            }
            return cont != 0;
        }
        private void CheckForCompletedTasks()
        {
            if(Tasks.Count > 0){
                if (DateTime.Now.Subtract(Tasks[0].Duration) >= Tasks[0].StartedExecuting)
                {
                    Tasks.RemoveAt(0);
                }
            }

        }
        public void ExecuteTasks()
        {
            CheckForCompletedTasks();
            if(Tasks.Count > 0 && !IsAnyActive()){
                Tasks[0].Execute();

            }
        }
    }
    public abstract class IUserTasks
    {
        public string TaskName
        {
            get;
            set;
        }
        public TimeSpan Duration
        {
            get;
            set;
        }
        public Boolean IsExecuting
        {
            get;
            set;
        }
        public DateTime StartedExecuting{
            get;
            set;
        }

        // Receiver Class
        protected DummySystem System
        {
            get;
            set;
        }

        public IUserTasks(DummySystem System)
        {
            this.System = System;
        }

        public abstract void Execute();
        public abstract void Undo();
    }

    public class DefragmentHD : IUserTasks
    {
        public DefragmentHD(DummySystem System) : base(System)
        {
            this.TaskName = " Defragmenting HDD";
            this.Duration = TimeSpan.FromSeconds(5);
            this.IsExecuting = false;

        }

        public override void Execute()
        {
            this.StartedExecuting = DateTime.Now;
            this.IsExecuting = true;
            if(this.System.NeedsDefrag){
                this.System.NeedsDefrag = false;
            }else{
                this.Duration = TimeSpan.Zero;
            }
        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }
    }

    public class DiskCleanUp : IUserTasks
    {
        public DiskCleanUp(DummySystem System) : base(System)
        {
            this.TaskName = " Cleaning UP";
            this.Duration = TimeSpan.FromSeconds(10);
            this.IsExecuting = false;
        }

        public override void Execute()
        {
            this.StartedExecuting = DateTime.Now;
            this.IsExecuting = true;
            System.AvailiableSpace = (int)(Convert.ToDouble(System.AvailiableSpace) * 0.9);
        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }
    }
}

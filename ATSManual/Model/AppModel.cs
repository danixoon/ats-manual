using ATSManual.Forms;
using ATSManual.Storing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSManual.Database;

namespace ATSManual.Model
{

    public class AppModel : CacheResource
    {
        public enum AppTaskStatus
        {
            Executing,
            Succces,
            Stopped,
            Error
        }

        public class AppTask
        {
            public string label;
            public AppTaskStatus status;
            public string error = null;
            public AppTask(string label, AppTaskStatus status)
            {
                this.label = label;
                this.status = status;
            }
        }

        public bool IsTaskExecuting(string key)
        {
            return tasks.ContainsKey(key);
        }

        public Dictionary<string, AppTask> tasks = new Dictionary<string, AppTask>();
        public Task RunTask(string key, string label, Task targetTask)
        {
            return RunTask(key, label, Task.Run<object>(async () => { await targetTask; return null; }));
        }
        public async Task<T> RunTask<T>(string key, string label, Task<T> targetTask)
        {
            T result = default(T);
            var task = new AppTask(label, AppTaskStatus.Stopped);
            tasks.Add(key, task);
            try
            {
                App.Mutate((store) =>
                {
                    Mutate();
                    task.status = AppTaskStatus.Executing;
                });
                result = await targetTask;
                App.Mutate((store) =>
                {
                    Mutate();
                    task.status = AppTaskStatus.Succces;
                });

            }
            catch (Exception ex)
            {
                Logging.Logger.Log($"Ошибка выполнения задачи {key}: {ex.Message}", Logging.Logger.MessageType.Error);
                App.Mutate((store) =>
                {
                    Mutate();
                    task.status = AppTaskStatus.Error;
                    task.error = ex.Message;
                });
                //throw ex;
            }
            finally
            {
                App.Mutate((store) =>
                {
                    Mutate();
                    tasks.Remove(key);
                });
            }
            return result;
        }
    }


}

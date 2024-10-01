using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOStudy_Logic.Service.Sentmail
{
    using Quartz;
    using Quartz.Impl;
    using System;
    using System.Threading.Tasks;

    public class TaskScheduler
    {
        private readonly IServiceProvider _serviceProvider;

        // Constructor nhận IServiceProvider
        public TaskScheduler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        // Phương thức Start để khởi chạy công việc
        public async Task Start()
        {
            // Lấy scheduler từ Quartz
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            // Tạo Job bằng cách sử dụng IServiceProvider để lấy các dịch vụ được đăng ký trong DI
            var job = JobBuilder.Create<TaskReminderJob>()
                .WithIdentity("taskReminderJob", "group1")
                .Build();

            // Tạo trigger để thực hiện công việc mỗi giờ
            var trigger = TriggerBuilder.Create()
                .WithIdentity("taskReminderTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(1)  // Chạy mỗi giờ
                    .RepeatForever())
                .Build();

            // Lên lịch công việc
            await scheduler.ScheduleJob(job, trigger);
        }
    }

}

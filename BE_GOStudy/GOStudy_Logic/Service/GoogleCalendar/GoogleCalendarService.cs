using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOStudy_Logic.Service.GoogleCalendar
{
    public class GoogleCalendarService
    {
        //public async Task AddEventToGoogleCalendar(string accessToken, TaskModel taskModel)
        //{
        //    var credential = GoogleCredential.FromAccessToken(accessToken);
        //    var service = new CalendarService(new BaseClientService.Initializer()
        //    {
        //        HttpClientInitializer = credential,
        //        ApplicationName = "GO Study"
        //    });

        //    var calendarEvent = new Event
        //    {
        //        Summary = taskModel.Title,
        //        Description = taskModel.Description,
        //        Start = new EventDateTime
        //        {
        //            DateTime = taskModel.StartTime,
        //            TimeZone = "America/Los_Angeles", // Cần đổi thành múi giờ của bạn
        //        },
        //        End = new EventDateTime
        //        {
        //            DateTime = taskModel.EndTime,
        //            TimeZone = "America/Los_Angeles",
        //        },
        //    };

        //    var request = service.Events.Insert(calendarEvent, "primary");
        //    await request.ExecuteAsync();
        //}
    }
}

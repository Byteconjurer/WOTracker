using WOTracker.Models;
using WOTracker.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace WOTracker.Utilities;

    internal class WODateListGenerator
    {
        private List<object> _workoutDays;
        private WOTrackerDbContext _context;


        public WODateListGenerator( )
        {
            _workoutDays = new List<object>();
            _context = new WOTrackerDbContext();
        }

    /* Algorithm that generates a list of DateTimes and WorkoutDays. If no WorkoutDays are found in the database, 
     * the list will contain DateTimes for the current week. If WorkoutDays are found, the list will contain a mix of 
     * DateTimes and WorkoutDays ranging from the monday on the same week as the first WorkoutDay date to the sunday
     * of the same week as the last WorkoutDay date. */

    internal async Task<List<object>> GenerateDatesList(DateTime startDate)
    {
        // Clear the list to ensure that it is empty
        _workoutDays.Clear();

        // Ensure that the start date is the first day of the week
        while (startDate.DayOfWeek != DayOfWeek.Sunday)
        {
            startDate = startDate.AddDays(1);
        }


        // Fetch WorkoutDays from the database
        List<WorkoutDay> workoutDaysFromDb = await _context.WorkoutDays
            .Include(wd => wd.Groups)
            .ToListAsync();

        foreach (var wd in workoutDaysFromDb)
        {
            Console.WriteLine(wd.Date);
        }

        // Add DateTimes for the current week if there are no WorkoutDays in the database
        if (workoutDaysFromDb.Count == 0)
        {
            for (DateTime date = startDate; date != startDate.AddDays(-7); date = date.AddDays(-1))
            {
                _workoutDays.Add(date);
            }

            return _workoutDays;
        }

        // Create a dictionary for quick lookup of WorkoutDays by date
        Dictionary<DateTime, WorkoutDay?> workoutDayLookup = workoutDaysFromDb
            .OfType<WorkoutDay>()
            .OrderByDescending(day => day.Date)
            .ToDictionary(day => day.Date, day => (WorkoutDay?)day);
 
        DateTime endDate = workoutDayLookup.LastOrDefault().Value.Date;


        // Ensure that the end date is the last day of the week
        while (endDate.DayOfWeek != DayOfWeek.Monday)
        {
            endDate = endDate.AddDays(-1);
        }

        // Generate the list of WorkoutDays for the specified date range
        for (DateTime date = startDate; date >= endDate; date = date.AddDays(-1))
        {
            _workoutDays.Add(workoutDayLookup.ContainsKey(date) ? workoutDayLookup[date] : date);
        }

        return _workoutDays;
    }


    // Add a week of DateTimes to the list
    internal List<object> AddWeekToList(List<object> workoutDays)
    {
        DateTime startDate = DateTime.Today;
        var lastItem = workoutDays.LastOrDefault();
        
        if (lastItem is WorkoutDay wd)
        {
            startDate = wd.Date.AddDays(-1);
        }
        else if (lastItem is DateTime dt)
        {
            startDate = dt.AddDays(-1);
        }

        for (int i = 0; i < 7; i++)
        {
            DateTime currentDate = startDate.AddDays(-i);
            workoutDays.Add(currentDate);
        }

        return workoutDays;
    }


}


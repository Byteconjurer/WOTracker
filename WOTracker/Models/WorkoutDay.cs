using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOTracker.Models
{
    public class WorkoutDay
    {
        [Key]
        public DateTime Date { get; set; }
        public string? Comment { get; set; }
        public List<MuscleGroup> Groups { get; set; }

        public WorkoutDay(DateTime date, string? comment, List<MuscleGroup> groups)
        {
            Date = date;
            Comment = comment;
            Groups = groups;
        }
        public WorkoutDay() 
        {
            Date = DateTime.Now;
            Comment= "";
            Groups = new List<MuscleGroup>();
        }
    }

    public class MuscleGroup
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Musclegroups Musclegroup { get; set; }
        public List<WorkoutDay> Days { get; set; }

        public MuscleGroup()
        {
            Musclegroup = Musclegroups.Abs;
            Days = new List<WorkoutDay>();
        }
   

    }


    public enum Musclegroups
    {  
        Abs,
        Arms,
        Back,
        Backside,
        Cardio,
        Chest,
        Core,
        Fullbody,
        Frontside,
        Legs,
        Shoulders,
    }
}

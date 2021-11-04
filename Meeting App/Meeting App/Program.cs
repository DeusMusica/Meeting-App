using System;
using System.Collections.Generic;
using System.IO;

namespace Meeting_App
{
    public class Meeting
    {
        private string title;
        private string location;
        private DateTime startDateTime;
        private DateTime endDateTime;

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
            }
        }

        public string Location
        {
            get
            {
                return this.location;
            }
            set
            {
                this.location = value;
            }
        }

        public DateTime StartDateTime
        {
            get
            {
                return this.startDateTime;
            }
            set
            {
                this.startDateTime = value;
            }
        }

        public DateTime EndDateTime
        {
            get
            {
                return this.endDateTime;
            }
            set
            {
                this.endDateTime = value;
            }
        }
        public Meeting(string title, string location, DateTime startTime, DateTime endtime)
        {
            Title = title;
            Location = location;
            StartDateTime = startTime;
            EndDateTime = endtime;
        }

        public Meeting()
        {

        }
        //public override string ToString()
        //{
        //    return string.Format("{0} at {1} on {2} from {3} to {4}", Title, Location, StartDateTime.ToString("MM/dd"), StartDateTime.ToString("HH:mm"), EndDateTime.ToString("HH:mm"));
        //}

    }
    
    

    
    class Program
    {
        public static Meeting AddEvent(List<Meeting> calendar)
        {

            Meeting newMeeting = new Meeting();
            bool hasConflict = false;
            do
            {
                Console.WriteLine("Enter the event Title:");
                newMeeting.Title = Console.ReadLine();
                Console.WriteLine("Enter the event location");
                newMeeting.Location = Console.ReadLine();
                Console.WriteLine("Enter the start date and time: MM/DD/YYYY HH:MM");
                newMeeting.StartDateTime = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Enter the end date and time: MM/DD/YYYY HH:MM");
                newMeeting.EndDateTime = DateTime.Parse(Console.ReadLine());
                foreach (Meeting eachMeeting in calendar)
                {
                    
                    if (eachMeeting.StartDateTime >= newMeeting.StartDateTime && eachMeeting.EndDateTime >= newMeeting.EndDateTime)
                    {                        
                        Console.WriteLine("This is a conflicting meeting!");
                        hasConflict = true;
                    }

                }

                if (hasConflict)
                {                    
                    Console.WriteLine("Would you like to add meeting with conflict? Y/N");
                    string userinput1 = Console.ReadLine();
                    if (userinput1 == "Y")
                    { 
                        hasConflict = false;
                    }
                }

            } while (hasConflict);

            return newMeeting;
            
        }
        public static void CalendarPrint(List<Meeting> calendar)
        {
            DateTime date;
            Console.WriteLine("Choose a date: MM/DD/YYYY");
            string userInput = Console.ReadLine();
            date = DateTime.Parse(userInput);
            for (date = date.AddHours(8); date.Hour <= 17; date = date.AddMinutes(30))
            {
                Console.Write(date.ToString());
                foreach (Meeting meeting in calendar)
                {
                    if (meeting.StartDateTime <= date && meeting.EndDateTime > date)
                    {
                        Console.Write(" | " + meeting.Title + " at " + meeting.Location);
                    }
                }
                Console.Write("\n");
            }

        }
    
        public static void CalendarMenu(List<Meeting> calendar)
        {
            bool run = true;
            
            do
            {
                Console.WriteLine("Enter Menu Option:\n1. Add event\n2. Remove event\n3. Display calendar\n4. Save Calendar\n5. Return to previous menu");
                string menuInput = Console.ReadLine();
                switch (menuInput)
                {
                    case "1":                        
                        calendar.Add(AddEvent(calendar));
                        break;
                    case "2":
                        //write method to remove event
                        break;
                    case "3":
                        CalendarPrint(calendar);
                        //write method to display calendar
                        break;
                    case "4":
                        //write method to save calender
                        break;
                    case "5":
                        run = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
            while (run == true);
        }
        static void Main(string[] args)
        {
            bool run = true;
            List<Meeting> currentCalendar = new List<Meeting>();
            List<Meeting> calendar = new List<Meeting>();
            do
            {
                Console.WriteLine("Welcome to Meeting App!" + "\n");
                Console.Write("What would you like to do?\n1. Load calendar from file\n2. Create new calendar\n0. Exit\n");
                string mainMenuChoice = Console.ReadLine();
                try
                {
                    {
                        switch (mainMenuChoice)
                        {
                            case "1": 
                                Console.WriteLine("Enter the file location");
                                string fileLocation = Console.ReadLine();
                                //method or code to read file and save to variable
                                string[] calenderArray = File.ReadAllLines(@fileLocation);
                                //method for menu
                                //method for saving location?
                                break;
                            case "2":
                                Console.WriteLine("Enter save location:");
                                string saveLocation = Console.ReadLine();
                                //Method name for new menu
                                CalendarMenu(currentCalendar);
                                //method for saving location
                                break;
                            case "0":
                                run = false;
                                break;
                            default:
                                Console.WriteLine("Invalid Input");
                                break;
                        }


                    }
                }

                
                //this is a place holder for exceptions
                catch (Exception e)
                {
                    Console.WriteLine($"An error occured. Message={e.Message} StackTrace={e.StackTrace}");
                }


            }
            while (run == true);

        }
    }
}

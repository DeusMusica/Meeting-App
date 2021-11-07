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
        public static Meeting? RemoveEvent(List<Meeting> calendar)
        {
            Meeting returnOFTheJedi = new Meeting();
            Console.WriteLine("Enter the date of event you want removed:");
            DateTime dateInput = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter the event Title you want removed:");
            string titleInput = Console.ReadLine();
            int index = -1;


            foreach (Meeting meeting in calendar)
            {
                if (meeting.StartDateTime.Day == dateInput.Day && meeting.Title == titleInput)
                {
                    index = calendar.IndexOf(meeting);
                }

            }
            if (index == -1)
            {
                return null;
            }
            else
            {
                return calendar[index];
            }

        }

        public static Meeting AddEvent(List<Meeting> calendar)
        {

            Meeting newMeeting = new Meeting();
            bool hasConflict = false;
            do
            {
                //add input validation for datetime
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
                        Console.WriteLine("This is a conflicting meeting!\n");
                        hasConflict = true;

                    }
                    else
                    {

                        hasConflict = false;
                    }

                }

                if (hasConflict == true)
                {
                    Console.WriteLine("Would you like to add conflicting meeting? Y/N\n");
                    string userinput1 = Console.ReadLine();
                    if (userinput1.ToUpper() == "Y")
                    {
                        hasConflict = false;

                    }
                    else
                    {
                        hasConflict = true;
                    }
                }


            } while (hasConflict == true);

            return newMeeting;

        }
        public static void CalendarPrint(List<Meeting> calendar)
        {
            //input validation
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

        public static void CalendarMenu(List<Meeting> calendar, string location, string fileName)
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
                        Meeting removedMeet = RemoveEvent(calendar);
                        if (removedMeet == null)
                        {
                            Console.WriteLine("No event removed\n");
                            break;
                        }
                        else
                        {
                            calendar.Remove(removedMeet);
                        }
                        //write method to remove event
                        break;
                    case "3":
                        CalendarPrint(calendar);
                        break;
                    case "4":
                        //write method to save calender
                        CalendarSave(calendar, location, fileName);
                        break;
                    case "5":
                        run = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input\n");
                        break;
                }
            }
            while (run == true);
        }

        public static void CalendarSave(List<Meeting> calendar, string location, string fileName)
        {
            
            string path = location + @fileName;
            string saveString = "";
            foreach (Meeting meeting in calendar)
            {
                saveString += (meeting.Title + "," + meeting.Location + "," + meeting.StartDateTime.ToString() + "," + meeting.EndDateTime.ToString() + "\n");
            }

            File.WriteAllText(path, saveString);
            Console.WriteLine("\nSaved Successfully\n");
        }

        public static List<Meeting> CalendarLoad(string fileLocation, string fileName)
        {
            List<Meeting> readEvents = new List<Meeting>();
            string path = fileLocation + @fileName;
            string[] calenderArray = File.ReadAllLines(path);

            foreach (string fileLine in calenderArray)
            {
                string[] fileLineData = fileLine.Split(",");
                string title = fileLineData[0];
                string location = fileLineData[1];
                DateTime startDateTime = DateTime.Parse(fileLineData[2]);
                DateTime endDateTime = DateTime.Parse(fileLineData[3]);

                Meeting readEvent = new Meeting(title, location, startDateTime, endDateTime);
                readEvents.Add(readEvent);

            }
            return readEvents;
        }
        static void Main(string[] args)
        {
            bool run = true;
            List<Meeting> returnedCalendar = new List<Meeting>();
            List<Meeting> currentCalendar = new List<Meeting>();
            List<Meeting> calendar = new List<Meeting>();
            do
            {
                //don't need the + with \n
                Console.WriteLine("Welcome to Meeting App!\n");
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
                                Console.WriteLine("Give file name:");

                                string loadFileNameInput = Console.ReadLine();
                                string loadFileName = "\\" + loadFileNameInput + ".dat";                               
                                currentCalendar = CalendarLoad(fileLocation, loadFileName);
                                CalendarMenu(currentCalendar, fileLocation, loadFileName);
                                break;
                            case "2":

                                //default location 
                                //string saveLocation = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                                //Console.WriteLine("File location is:" + saveLocation);
                                
                                //asking location
                                Console.WriteLine("Enter save location:");
                                string saveLocation = Console.ReadLine();

                                Console.WriteLine("Give file name:");
                                string saveFileNameInput = Console.ReadLine();
                                string saveFileName = "\\" + saveFileNameInput + ".dat";
                                
                                CalendarMenu(currentCalendar, saveLocation, saveFileName);
                                //method for saving location

                                break;
                            case "0":
                                run = false;
                                break;
                            default:
                                Console.WriteLine("Invalid Input\n");
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

using System.Collections.Generic;
using UnityEngine;

public sealed class Constants
{
    public class Prefs {
        public static string currentID = "currentID";
        public static string currentProof = "currentProof";
        public static string proofExpireDate = "proofExpireDate";
        public static string masterVolume = "masterVolume";
        public static string musicCapVolume = "musicCapVolume";
        public static string musicFade = "musicFadeVolume";
        public static string sfxCapVolume = "sfxCapVolume";
        public static string screenModeKey = "screenModeKey";
        public static string selectedTheme = "selectedTheme";
        public static string reMatch = "reMatch";
    }

    public class GameSettings{
        public static float transitionAnimationTime = 0.5f;
        public static int attackSkipID = -1;
        public static int defenseSkipID = -2;
        public static float opponentPlayCardDelay = 2.0f;
        public static float playerPlayCardDelay = 1f;
    }

    public class Colors
    {
        public static Color idleUiColor = new Color(1f, 0f, 0f, 1f);
        public static Color selectedUiColor = new Color(0.55f, 1f, 0.4f, 1f);
    }

    public class Tips
    {
        public static List<string> tips = new List<string>()
        {
            "Patients can be asymptomatic",
            "Strange behaviours can indicate neurological deceases.",
            "Tip n 3",
            "Tip n 4"
        };
    }

    public class Patients
    {
        public static List<string> names = new List<string>()
        {
            "Ava Smith",
            "Liam Johnson",
            "Emma Williams",
            "Noah Brown",
            "Olivia Jones",
            "Mason Garcia",
            "Sophia Miller",
            "Ethan Davis",
            "Isabella Rodriguez",
            "Lucas Martinez",
            "Mia Hernandez",
            "Jackson Lopez",
            "Amelia Gonzalez",
            "Aiden Wilson",
            "Harper Anderson",
            "Logan Thomas",
            "Charlotte Taylor",
            "Benjamin Moore",
            "Ella Jackson",
            "James Martin",
            "Abigail Lee",
            "Sebastian Perez",
            "Avery Thompson",
            "Elijah White",
            "Scarlett Harris",
            "Alexander Sanchez",
            "Grace Clark",
            "Jacob Ramirez",
            "Chloe Lewis",
            "Evelyn Robinson",
            "Henry Walker",
            "Aria Young",
            "Daniel King",
            "Layla Wright",
            "Matthew Scott",
            "Lily Green",
            "David Adams",
            "Ellie Baker",
            "Joseph Nelson",
            "Riley Carter",
            "Samuel Mitchell",
            "Zoe Campbell",
            "Andrew Roberts",
            "Nora Phillips",
            "Joshua Turner",
            "Hannah Parker",
            "Gabriel Evans",
            "Lillian Edwards",
            "William Collins",
            "Hazel Stewart",
            "Michael Morris",
            "Eleanor Murphy",
            "Christopher Rivera",
            "Penelope Cook",
            "Ryan Rogers",
            "Sophie Morgan",
            "John Reed",
            "Aubrey Bell",
            "Owen Cooper",
            "Victoria Richardson",
            "Nathan Bailey",
            "Madison Cox",
            "Caleb Howard",
            "Natalie Ward",
            "Dylan Torres",
            "Camila Peterson",
            "Luke Gray",
            "Aurora Ramirez",
            "Adam James",
            "Brooklyn Washington",
            "Isaac Brooks",
            "Paisley Bennett",
            "Julian Sanders",
            "Savannah Perry",
            "Grayson Foster",
            "Stella Powell",
            "Levi Long",
            "Allison Russell",
            "Hunter Patterson",
            "Violet Griffin",
            "Christian Jenkins",
            "Alice Webb",
            "Jonathan Myers",
            "Clara Haynes",
            "Isaiah Boyd",
            "Bella Alexander",
            "Charles Kelly",
            "Samantha Gonzales",
            "Thomas Ross",
            "Lucy Bryant",
            "Josiah Hamilton",
            "Audrey Russell",
            "Hudson Griffin",
        };
    }
}

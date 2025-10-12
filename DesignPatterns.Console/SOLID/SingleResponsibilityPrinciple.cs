namespace DesignPatterns.SOLID
{
    // The Single Responsibility Principle (SRP) states that a class should have only one reason to change —
    // in other words, it should have only one well-defined responsibility.

    #region The Problem

    /// <summary>
    /// The Journal class represents a simple diary where you can add and remove entries.
    /// 
    /// In this case, the Journal class is only responsible for **managing entries**.
    /// It is NOT responsible for saving/loading data (persistence).
    /// </summary>
    public class Journal
    {
        private readonly List<string> entries = new List<string>();
        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");
            return count;
        }

        public void RemoveEntry(int index)
        {
            if (index >= 0 && index < entries.Count)
            {
                entries.RemoveAt(index);
            }
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }

        /*
         * Violating SRP Example (Do NOT do this):
         * 
         * If you add methods like:
         * 
         * public void Save(string filename) {}
         * public static Journal Load(string filename) {}
         * public void Load(Uri uri) {}
         * 
         * You create a class that is responsible for both:
         * - Managing journal entries
         * - Handling data persistence (file I/O, networking, etc.)
         * 
         * This is bad because now the class could change for **two reasons**:
         * 1. If the way journal entries are managed changes.
         * 2. If the way data is saved/loaded changes.
         * 
         * => This breaks the Single Responsibility Principle.
         */
    }
    #endregion

    #region The Solution

    /// <summary>
    /// The Persistence class handles saving objects to files.
    /// 
    /// This keeps the Journal class focused on entry management,
    /// and delegates persistence concerns to a separate class.
    /// </summary>
    public class Persistence
    {
        public void SaveToFile(Journal journal, string filename, bool overwrite = false)
        {
            if (overwrite || !File.Exists(filename))
            {
                File.WriteAllText(filename, journal.ToString());
            }
        }
    }

    #endregion

    #region SRP_Example

    /// <summary>
    /// This static class runs the SRP example and shows the proper separation of concerns
    /// between `Journal` (entry management) and `Persistence` (data storage).
    /// </summary>
    public static class SRP_Example
    {
        public static void Run()
        {
            var j = new Journal();
            j.AddEntry("I cried today.");
            j.AddEntry("I ate a bug.");

            Console.WriteLine("Journal entries:");
            Console.WriteLine(j);

            var p = new Persistence();
            var filename = Path.Combine(Environment.CurrentDirectory, "journal.txt");

            p.SaveToFile(j, filename, overwrite: true);
            Console.WriteLine($"\nJournal saved to {filename}");
        }
    }

    #endregion
}

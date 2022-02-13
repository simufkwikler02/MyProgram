public static class FileCabinetService
{
    private static readonly List<FileCabinetRecord> List = new List<FileCabinetRecord>();

    public static int CreateRecord(string firstName, string lastName, DateTime dateOfBirth, short property1, decimal property2, char property3)
    {
        var record = new FileCabinetRecord
        {
            Id = List.Count + 1,
            FirstName = firstName,
            LastName = lastName,
            DateOfBirth = dateOfBirth,
            Property1 = property1,
            Property2 = property2,
            Property3 = property3,
        };

        List.Add(record);

        return record.Id;
    }

    public static FileCabinetRecord[] GetRecords()
    {
        if (List.Count == 0)
        {
            return Array.Empty<FileCabinetRecord>();
        }
        else
        {
            var records = new FileCabinetRecord[List.Count];
            foreach (var record in List)
            {
                records[record.Id - 1] = record;
            }

            return records;
        }
    }

    public static int GetStat()
    {
        return List.Count;
    }
}
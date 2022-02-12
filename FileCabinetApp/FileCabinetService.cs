public static class FileCabinetService
{
    private static readonly List<FileCabinetRecord> List = new List<FileCabinetRecord>();

    public static int CreateRecord(string firstName, string lastName, DateTime dateOfBirth)
    {
        var record = new FileCabinetRecord
        {
            Id = List.Count + 1,
            FirstName = firstName,
            LastName = lastName,
            DateOfBirth = dateOfBirth,
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